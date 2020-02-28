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
        public ActionResult Index()
        {
            return View(Storage.Instance.miArbolMedicamentos.ObtenerLista());
        }
        
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            var name = collection["search"];
            return View(MedicamentoModel.Filter(name));
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
                if (pedido > Storage.Instance.misMedicamentosExt[id - 1].Existencia)
                {

                }

                return View(Storage.Instance.misMedicamentosExt[id-1]);
            }
            catch
            {
                return View();
            }
        }

    }
}