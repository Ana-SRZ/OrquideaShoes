using OrquideaShoes.Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace OrquideaShoes.Logica
{
    namespace OrquideaShoes.Logica
    {
        public class GestorArticulos
        {
            public void AgregarArticulo(Articulo articulo)
            {
                // Validaciones
                if (string.IsNullOrEmpty(articulo.Codigo))
                    throw new ArgumentException("El código es requerido");

                if (AlmacenDatos.Articulos.Any(a => a.Codigo == articulo.Codigo))
                    throw new InvalidOperationException("Ya existe un artículo con este código");

                AlmacenDatos.Articulos.Add(articulo);
            }

            public List<Articulo> ObtenerTodos()
            {
                return AlmacenDatos.Articulos.ToList();
            }

            public Articulo BuscarPorCodigo(string codigo)
            {
                return AlmacenDatos.Articulos.FirstOrDefault(a => a.Codigo == codigo);
            }

            public void ActualizarStock(string codigo, int cantidad)
            {
                var articulo = BuscarPorCodigo(codigo);
                if (articulo != null)
                {
                    articulo.Stock -= cantidad;
                }
            }
        }
    }
}
