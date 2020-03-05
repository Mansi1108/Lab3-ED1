using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Helpers;

namespace Lab2_ED1.Controllers
{
    public class PedidoController : Controller
    {
        // GET: Pedido
        public ActionResult Index()
        {
            TempData["nombre"] = Storage.Instance.miCliente.Nombre;
            double total = 0;
            foreach (var item in Storage.Instance.miPedido)
            {
                total += item.Precio * item.Existencia;
            }
            TempData["total"] = "$ "+ total;
            return View(Storage.Instance.miPedido);
        }

        public ActionResult ConfirmarP(string tag)
        {
            Storage.Instance.miPedido.Clear();
            return RedirectToAction("Index", "Medicamento");
        }

    }
}