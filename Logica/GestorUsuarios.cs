using OrquideaShoes.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrquideaShoes.Logica
{
    public class GestorUsuarios
    {
        public bool ValidarCredenciales(string username, string password)
        {
            var usuario = AlmacenDatos.Usuarios.FirstOrDefault(
                u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                     u.Password == password
            );

            if (usuario != null)
            {
                AlmacenDatos.UsuarioActual = usuario;
                return true;
            }

            return false;
        }

        public Usuario ObtenerUsuarioActual()
        {
            return AlmacenDatos.UsuarioActual;
        }

        public void CerrarSesion()
        {
            AlmacenDatos.UsuarioActual = null;
        }

        public bool EsAdmin()
        {
            return AlmacenDatos.UsuarioActual?.Rol == "Admin";
        }

        public void AgregarUsuario(Usuario usuario)
        {
            if (AlmacenDatos.Usuarios.Any(u => u.Username.Equals(usuario.Username, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("El nombre de usuario ya existe");

            AlmacenDatos.Usuarios.Add(usuario);
        }
    }
}
