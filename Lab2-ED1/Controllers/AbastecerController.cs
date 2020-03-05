using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Helpers;

namespace Lab2_ED1.Controllers
{
    public class AbastecerController : Controller
    {
        // GET: Abastecer
        public ActionResult Index()
        {
            foreach (var item in Storage.Instance.misMedicamentosExt)
            {
                if (item.Existencia ==0)
                {
                    Storage.Instance.miAsbastecer.Add(item);
                }
            }
            return View(Storage.Instance.miAsbastecer);
        }

        public ActionResult ReAbastecer(string tag)
        {
            Random rnd = new Random();

            foreach (var item in Storage.Instance.miAsbastecer)
            {
                var std = Storage.Instance.misMedicamentosExt.Where(s => s.id == item.id).FirstOrDefault();
                std.Existencia = rnd.Next(1, 15);
                Storage.Instance.miArbolMedicamentos.Add(std);
            }
            return RedirectToAction("Index");
        }
    }
}