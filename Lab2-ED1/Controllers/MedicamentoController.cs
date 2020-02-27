using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Helpers;
using Lab2_ED1.Models;

namespace Lab2_ED1.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        public ActionResult Index()
        {
            return View(Storage.Instance.miArbolMedicamentos.ObtenerLista());
        }

    }
}