using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLibrary
{
    public class Nodo<T> where T : IComparable<T>
    {
        public Nodo<T> Izquierdo { get; set; }
        public Nodo<T> Derecho { get; set; }
        public T Valor { get; set; }
    }
}
