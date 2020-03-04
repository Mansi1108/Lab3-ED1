using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Helpers;

namespace Lab2_ED1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            Storage.Instance.miCliente.Nombre = collection["Nombre"];
            Storage.Instance.miCliente.Direccion = collection["Direccion"];
            Storage.Instance.miCliente.Nit = long.Parse(collection["Nit"]);
            return RedirectToAction("Index", "Pedido");
        }

    }
}