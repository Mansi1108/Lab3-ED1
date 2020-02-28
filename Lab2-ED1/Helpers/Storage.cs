using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomLibrary.Estructuras;
using Lab2_ED1.Models;

namespace Lab2_ED1.Helpers
{
    public class Storage
    {
        private static Storage _instance = null;
        public static Storage Instance
        {
            get
            {
                if (_instance == null) _instance = new Storage();
                return _instance;
            }
        }

        public List<MedicamentoExtModel> misMedicamentosExt= new List<MedicamentoExtModel>();
        public Arbol<MedicamentoModel> miArbolMedicamentos = new Arbol<MedicamentoModel>();
    }
}