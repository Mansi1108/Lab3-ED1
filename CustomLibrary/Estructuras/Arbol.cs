using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLibrary.Interfaces;

namespace CustomLibrary.Estructuras
{
    public class Arbol<T> : NonLinearDataStructureBase<T> where T : IComparable<T>
    {
        private Nodo<T> Raiz = new Nodo<T>();
        private Nodo<T> temp = new Nodo<T>();
        private List<T> listaOrdenada = new List<T>();

        // Metodos publicos que reciven un Valor (Ejemplo: medicamento)
        public void Add(T value)
        {
            Insert(Raiz, value);
        }
        public T Remove(T deleted)
        {
            Nodo<T> busc = new Nodo<T>();
            busc = Get(Raiz, deleted);
            if (busc != null)
            {
                Delete(busc);
            }
            return deleted;
        }
        public T Buscar(T buscado)
        {
            Nodo<T> busc = new Nodo<T>();
            busc = Get(Raiz, buscado);
            if (busc == null)
            {
                // throw new System.InvalidOperationException("Valor no encontrado");
                return temp.Valor;
            }
            else
            {
                return busc.Valor;
            }
        }
        public List<T> ObtenerLista()
        {
            listaOrdenada.Clear();
            InOrder(Raiz);
            return listaOrdenada;
        }
        private bool Balanceado (Nodo<T> nodo)
        {
            if (Math.Abs(nodo.Charge)<= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // tres casos para eliminacion
        protected override void Delete(Nodo<T> nodo)
        {
            if (nodo.Izquierdo.Valor == null && nodo.Derecho.Valor == null) // Caso 1
            {
                nodo.Valor = nodo.Derecho.Valor;
            }
            else if (nodo.Derecho.Valor == null) // Caso 2
            {
                nodo.Valor = nodo.Izquierdo.Valor;
                nodo.Derecho = nodo.Izquierdo.Derecho;
                nodo.Izquierdo = nodo.Izquierdo.Izquierdo;
            }
            else // Caso 3
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    temp = Derecha(nodo.Izquierdo);
                }
                else
                {
                    temp = Derecha(nodo);
                }
                nodo.Valor = temp.Valor;
            }
        }
        
        // Metodo ayuda para el caso 3 de eliminacion
        private Nodo<T> Derecha(Nodo<T> nodo)
        {
            if (nodo.Derecho.Valor == null)
            {
                if (nodo.Izquierdo.Valor != null)
                {
                    return Derecha(nodo.Izquierdo);
                }
                else
                {
                    Nodo<T> temporal = new Nodo<T>();
                    temporal.Valor = nodo.Valor;
                    nodo.Valor = nodo.Derecho.Valor;
                    return temporal;
                }
            }
            else
            {
                return Derecha(nodo.Derecho);
            }
        }

        // Busqueda recursiva de un valor dentro del arbol
        protected override Nodo<T> Get(Nodo<T> nodo, T value)
        {
            if (value.CompareTo(nodo.Valor) == 0)
            {
                return nodo;
            }
            else if (value.CompareTo(nodo.Valor) == -1)
            {
                if (nodo.Izquierdo.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Izquierdo, value);
                }
            }
            else
            {
                if (nodo.Derecho.Valor == null)
                {
                    return null;
                }
                else
                {
                    return Get(nodo.Derecho, value);
                }
            }
        }

        // Metodo recursivo para incertar segun orden alfabetico
        protected override Nodo<T> Insert(Nodo<T> nodo, T value)
        {
            try
            {
                if (nodo.Valor == null)
                {
                    nodo.Valor = value;
                    nodo.Derecho = new Nodo<T>();
                    nodo.Izquierdo = new Nodo<T>();
                }
                else if (value.CompareTo(nodo.Valor) == -1)
                {
                    nodo.Izquierdo = Insert(nodo.Izquierdo, value);
                }
                else if (value.CompareTo(nodo.Valor) == 1)
                {
                    nodo.Derecho = Insert(nodo.Derecho, value);
                }
                return nodo;
            }
            catch
            {
                throw;
            }
        }

        // Recorre la lista en orden y agrega los valores a la listaOrdenada
        private void InOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                InOrder(nodo.Izquierdo);
                listaOrdenada.Add(nodo.Valor);
                InOrder(nodo.Derecho);
            }
        }

        private void RotDerecha (Nodo<T> nodo)
        {
            
        }
        private void RotIzquierda (Nodo<T> nodo)
        {
            
        }
        private void Rotacion (Nodo<T> nodo)
        {

        }
        
    }
}
