using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2_ED1.Models;
using Lab2_ED1.Helpers;
using System.Configuration;

namespace Lab2_ED1.Controllers
{
    public class SubirArchivoController : Controller
    {
        // GET: SubirArchivo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubirArchivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubirArchivo(HttpPostedFileBase file)
        {
            string _path = "";
            string _FileName = "";
            try
            {

                if (file.ContentLength > 0)
                {
                    _FileName = Path.GetFileName(file.FileName);
                    _path = Path.Combine(Server.MapPath("~/Archivos"), _FileName);
                    file.SaveAs(_path);
                    Console.WriteLine(_FileName + ", " + _path);
                }
                
                ViewBag.Message = "Archivo subido exitosamente!";
                using (TextFieldParser parser = new TextFieldParser(_path))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields[0] != "id")
                        {
                            var medicamento = new MedicamentoExtModel
                            {
                                id = int.Parse(fields[0]),
                                Nombre = fields[1],
                                Descripcion = fields[2],
                                CasaProd = fields[3],
                                Precio = double.Parse(fields[4].Substring(1,fields[4].Length-1)),
                                Existencia = int.Parse(fields[5]),
                            };
                            Storage.Instance.misMedicamentosExt.Add(medicamento);
                            Storage.Instance.miArbolMedicamentos.Add(medicamento);
                        }
                    }
                }
                return RedirectToAction("Index", "Medicamento");
            }
            catch
            {
                ViewBag.Message = "No se pudo subir el archivo";
                return View();
            }
        }
    }
}