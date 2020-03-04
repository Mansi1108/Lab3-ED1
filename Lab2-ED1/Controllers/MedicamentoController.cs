using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Helpers;
using Lab2_ED1.Models;
using PagedList;

namespace Lab2_ED1.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult Index(int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(Storage.Instance.miArbolMedicamentos.ObtenerLista().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Index(int? page, FormCollection collection)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 25;
            int pageNumber = (page ?? 1);

            var name = collection["search"];
            return View(MedicamentoModel.Filter(name).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Edit(int id)
        {
            var std = Storage.Instance.misMedicamentosExt.Where(s => s.id == id).FirstOrDefault();
            return View(std);
        }

        //
        // POST: /Car/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                int pedido = int.Parse(collection["Existencia"]);
                int exist = Storage.Instance.misMedicamentosExt[id - 1].Existencia;
                bool agregar = true;

                if (exist == 0)
                {
                    ViewBag.Message = "No se encuentra en existencia este producto";
                    agregar = false;
                }
                else if (pedido > Storage.Instance.misMedicamentosExt[id - 1].Existencia)
                {
                    ViewBag.Message = "Solo se agregaron: " + exist + " a la orden.";
                    pedido = exist;
                    exist = 0;
                }
                else
                {
                    if (pedido == exist)
                    {
                        exist = 0;
                    }
                    ViewBag.Message = pedido + " " + '"' + Storage.Instance.misMedicamentosExt[id - 1].Nombre + '"' + " agregados a la orden.";
                    Storage.Instance.misMedicamentosExt[id - 1].Existencia -= pedido;
                }

                if (exist == 0)
                {
                    Storage.Instance.misMedicamentosExt[id - 1].Existencia = 0;
                    Storage.Instance.miArbolMedicamentos.Remove(Storage.Instance.misMedicamentosExt[id - 1]);
                }
                if (agregar)
                {
                    var nuevoPedido = new MedicamentoExtModel
                    {
                        Nombre = Storage.Instance.misMedicamentosExt[id - 1].Nombre,
                        Precio = Storage.Instance.misMedicamentosExt[id - 1].Precio,
                        Existencia = pedido
                    };
                    Storage.Instance.miPedido.Add(nuevoPedido);
                }

                return View(Storage.Instance.misMedicamentosExt[id - 1]);
            }
            catch
            {
                return View();
            }
        }

    }
}