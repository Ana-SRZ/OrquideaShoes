using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrquideaShoes.Modelos
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }  // "Admin" o "Vendedor"
    }
}
