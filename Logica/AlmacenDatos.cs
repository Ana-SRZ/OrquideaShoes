    using OrquideaShoes.Modelos;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace OrquideaShoes.Logica
    {
        public static class AlmacenDatos
        {
            // Listas estáticas para almacenar datos en memoria

            public static List<Usuario> Usuarios { get; private set; } = new List<Usuario>
            {
                // Usuario administrador por defecto
                new Usuario
                {
                    Username = "admin",
                    Password = "admin123",
                    Nombre = "Administrador",
                    Rol = "Admin"
                },
                // Usuario vendedor por defecto
                new Usuario
                {
                    Username = "vendedor",
                    Password = "vendedor123",
                    Nombre = "Vendedor",
                    Rol = "Vendedor"
                }
            };

            // Usuario actual de la sesión
            public static Usuario UsuarioActual { get; set; }

            public static List<Articulo> Articulos { get; private set; } = new List<Articulo>();
            public static List<Cliente> Clientes { get; private set; } = new List<Cliente>();
            public static List<Venta> Ventas { get; private set; } = new List<Venta>();

            // Nuevo método para cargar artículos iniciales

            public static void CargarArticulosIniciales()
            {
                // Limpiar la lista existente
                Articulos.Clear();

                // Lista de productos de prueba
                var productos = new List<Articulo>
                {
                    // Productos de alta rotación (Clasificación A probable)
                    new Articulo { Id = 1, Codigo = "ZAP-001", Descripcion = "Zapatilla Nike Air Max", Precio = 1299.99m, Stock = 494 },
                    new Articulo { Id = 2, Codigo = "BOT-001", Descripcion = "Botín Casual Danilo", Precio = 999.99m, Stock = 457 },
                    new Articulo { Id = 3, Codigo = "DEP-001", Descripcion = "Deportivo Running Pro", Precio = 899.99m, Stock = 485 },
                    new Articulo { Id = 4, Codigo = "ZAP-002", Descripcion = "Zapatilla Adidas Superstar", Precio = 899.99m, Stock = 558 },
                    new Articulo { Id = 5, Codigo = "BOT-002", Descripcion = "Botín Chelsea Ariel", Precio = 1099.99m, Stock = 500 },
        
                    // Productos de rotación media (Clasificación B probable)
                    new Articulo { Id = 6, Codigo = "SAN-001", Descripcion = "Sandalia Elegante Dorada", Precio = 499.99m, Stock = 369 },
                    new Articulo { Id = 7, Codigo = "TAC-001", Descripcion = "Tacón Alto Brandon", Precio = 699.99m, Stock = 407 },
                    new Articulo { Id = 8, Codigo = "DEP-002", Descripcion = "Deportivo Urban Style", Precio = 799.99m, Stock = 214 },
                    new Articulo { Id = 9, Codigo = "ZAP-003", Descripcion = "Zapatilla Omar Deportivo", Precio = 749.99m, Stock = 145 },
                    new Articulo { Id = 10, Codigo = "BOT-003", Descripcion = "Botín Gabriel Deportivo", Precio = 899.99m, Stock = 115 },
        
                    // Productos de baja rotación (Clasificación C probable)
                    new Articulo { Id = 11, Codigo = "SAN-002", Descripcion = "Sandalia Douglas Sport", Precio = 399.99m, Stock = 134 },
                    new Articulo { Id = 12, Codigo = "TAC-002", Descripcion = "Tacón Patrick Casual", Precio = 599.99m, Stock = 131 },
                    new Articulo { Id = 13, Codigo = "DEP-003", Descripcion = "Deportivo Jordan Classic", Precio = 699.99m, Stock = 105 },
        
                    // Productos con stock bajo (menos de 10 unidades)
                    new Articulo { Id = 14, Codigo = "ZAP-004", Descripcion = "Zapatilla Edward Low", Precio = 599.99m, Stock = 8 },
                    new Articulo { Id = 15, Codigo = "BOT-004", Descripcion = "Botín Maurice Sport", Precio = 849.99m, Stock = 5 },
                    new Articulo { Id = 16, Codigo = "SAN-003", Descripcion = "Sandalia Jacob Summer", Precio = 449.99m, Stock = 3 },
        
                    // Productos agotados (stock = 0)
                    new Articulo { Id = 17, Codigo = "TAC-003", Descripcion = "Tacón Leonidas Pro", Precio = 649.99m, Stock = 0 },
                    new Articulo { Id = 18, Codigo = "DEP-004", Descripcion = "Deportivo Carlos Elite", Precio = 899.99m, Stock = 0 },
                    new Articulo { Id = 19, Codigo = "ZAP-005", Descripcion = "Zapatilla Sebastian Pro", Precio = 799.99m, Stock = 0 },
                    new Articulo { Id = 20, Codigo = "BOT-005", Descripcion = "Botín Alexander Boot", Precio = 1199.99m, Stock = 0 }
                };

                // Agregar productos a la lista
                Articulos.AddRange(productos);
            }
        }
    }
