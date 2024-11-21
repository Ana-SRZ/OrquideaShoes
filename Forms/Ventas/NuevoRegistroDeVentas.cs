using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OrquideaShoes.Logica;
using OrquideaShoes.Logica.OrquideaShoes.Logica;
using OrquideaShoes.Modelos; // Asegúrate de incluir el espacio de nombres donde están tus modelos

namespace OrquideaShoes
{
    public partial class NuevoRegistroDeVentas : Form
    {
        private Venta ventaActual;
        private bool _esEdicion = false;

        public NuevoRegistroDeVentas()
        {
            InitializeComponent();
            ventaActual = new Venta();

            AlmacenDatos.CargarArticulosIniciales();
            llenarListaDeArticulos();
        }

        private void llenarListaDeArticulos()
        {
            cmbArticulo.Items.Clear();

            foreach (var articulo in AlmacenDatos.Articulos)
            {
                cmbArticulo.Items.Add($"{articulo.Descripcion}");
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            // Validar que solo se ingresen números y un punto decimal
            if (txtCantidad.Text.Length > 0 && !decimal.TryParse(txtCantidad.Text, out _))
            {
                MessageBox.Show("Solo se permiten números en el precio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCantidad.Text = txtCantidad.Text.Substring(0, txtCantidad.Text.Length - 1);
                txtCantidad.SelectionStart = txtCantidad.Text.Length;
            }
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    ventaActual.Fecha = dateTimePicker1.Value;

                    var articuloSeleccionado = AlmacenDatos.Articulos[cmbArticulo.SelectedIndex];

                    var detalleVenta = new DetalleVenta
                    {
                        Articulo = articuloSeleccionado,
                        Cantidad = Convert.ToInt32(txtCantidad.Text),
                        PrecioUnitario = articuloSeleccionado.Precio,
                    };

                    ventaActual.Detalles = new List<DetalleVenta> { detalleVenta };

                    if (!_esEdicion)
                    {
                        ventaActual.NumeroFactura = AlmacenDatos.Ventas.Count + 1;
                        AlmacenDatos.Ventas.Add(ventaActual);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el artículo: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text) ||
            string.IsNullOrWhiteSpace(dateTimePicker1.Text) ||
            cmbArticulo.SelectedIndex == -1)
            {
                MessageBox.Show("Todos los campos son obligatorios",
                    "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }



            if (!decimal.TryParse(txtCantidad.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
