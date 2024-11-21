using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrquideaShoes.Modelos
{
    public class Articulo
    {
        public int Id {  get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Descripcion { get; set; }

        public int Numero { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PVP { get; set; }
        public decimal TotalVendido { get; set; }
        public decimal Participacion { get; set; }
        public decimal ParticipacionAcumulada { get; set; }
        public string Clasificacion { get; set; }
    }
}
