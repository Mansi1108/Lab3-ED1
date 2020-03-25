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
        public List<T> ObtenerListaPost()
        {
            listaOrdenada.Clear();
            PostOrder(Raiz);
            return listaOrdenada;
        }
        public List<T> ObtenerListaPre()
        {
            listaOrdenada.Clear();
            PreOrder(Raiz);
            return listaOrdenada;
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
            BalanceoPost(Raiz);
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
                    nodo = Balancear(nodo);
                }
                else if (value.CompareTo(nodo.Valor) == 1)
                {
                    nodo.Derecho = Insert(nodo.Derecho, value);
                    nodo = Balancear(nodo);
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
        private void PostOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                PostOrder(nodo.Izquierdo);
                PostOrder(nodo.Derecho);
                listaOrdenada.Add(nodo.Valor);
            }
        }
        private void PreOrder(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                listaOrdenada.Add(nodo.Valor);
                PreOrder(nodo.Izquierdo);
                PreOrder(nodo.Derecho);
            }
        }
        private void BalanceoPost(Nodo<T> nodo)
        {
            if (nodo.Valor != null)
            {
                BalanceoPost(nodo.Izquierdo);
                BalanceoPost(nodo.Derecho);
                Balancear(nodo);
            }
        }


        private int getHeight(Nodo<T> node)
        {
            if (node == null) return -1;
            var IzquierdoH = getHeight(node.Izquierdo);
            var rightH = getHeight(node.Derecho);
            return Math.Max(IzquierdoH, rightH) + 1;
        }

        private int FactorEquilibrio(Nodo<T> nodoActual)
        {
            int iz = getHeight(nodoActual.Izquierdo);
            int der = getHeight(nodoActual.Derecho);
            int FactorE = iz - der;
            return FactorE;
        }

        private Nodo<T> Balancear(Nodo<T> nodoActual)
        {
            int factorE = FactorEquilibrio(nodoActual);
            if (factorE > 1)
            {
                if (FactorEquilibrio(nodoActual.Izquierdo) > 0)
                {
                    nodoActual = RotacionDer(nodoActual);
                }
                else
                {
                    nodoActual = RotacionDobDer(nodoActual);
                }
            }
            else if (factorE < -1)
            {
                if (FactorEquilibrio(nodoActual.Derecho) > 0)
                {
                    nodoActual = RotacionDobIzq(nodoActual);
                }
                else
                {
                    nodoActual = RotacionIzq(nodoActual);
                }
            }
            return nodoActual;
        }

        private Nodo<T> RotacionIzq(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Derecho.Valor,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = temp.Izquierdo;
            temp.Izquierdo = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }
        private Nodo<T> RotacionDer(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = temp.Derecho;
            temp.Derecho = nodoActual;

            if (nodoActual.Valor.CompareTo(Raiz.Valor) == 0)
            {
                Raiz = temp;
            }
            return temp;
        }
        private Nodo<T> RotacionDobDer(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Izquierdo.Valor,
                Izquierdo = nodoActual.Izquierdo.Izquierdo,
                Derecho = nodoActual.Izquierdo.Derecho
            };
            nodoActual.Izquierdo = RotacionIzq(temp);
            return RotacionDer(nodoActual);
        }
        private Nodo<T> RotacionDobIzq(Nodo<T> nodoActual)
        {
            var temp = new Nodo<T>
            {
                Valor = nodoActual.Derecho.Valor,
                Izquierdo = nodoActual.Derecho.Izquierdo,
                Derecho = nodoActual.Derecho.Derecho
            };
            nodoActual.Derecho = RotacionDer(temp);
            return RotacionIzq(nodoActual);
        }

    }
}
