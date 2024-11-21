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
    public partial class ListaRegistroDeVentas : Form
    {
        public ListaRegistroDeVentas()
        {
            InitializeComponent();
        }

        private void volver(object sender, EventArgs e)
        {
            var menu = new Opciones();
            this.Hide();
            menu.FormClosed += (s, args) => this.Close();
            menu.Show();

        }
    }
}
