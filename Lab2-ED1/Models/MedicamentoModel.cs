using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lab2_ED1.Helpers;

namespace Lab2_ED1.Models
{
    public class MedicamentoModel : IComparable<MedicamentoModel>
    {
        public string Nombre { get; set; }
        public int id { get; set; }

        public int CompareTo(MedicamentoModel other)
        {
            if (other == null)
            {
                return 0;
            }
            else
            {
                return this.Nombre.CompareTo(other.Nombre);
            }
        }
    }
}