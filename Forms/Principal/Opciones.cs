using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrquideaShoes
{
    public partial class Opciones : Form
    {
        public Opciones()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var articulos = new ListaArticulos();

            articulos.Show();

            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ventas = new ListaRegistroDeVentas();

            ventas.Show();

            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var clientes = new DirectorioDeClientes();

            clientes.Show();

            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var factura = new NuevaFactura();

            factura.Show();

            this.Hide();
        }
    }
}
