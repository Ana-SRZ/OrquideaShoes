using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrquideaShoes.Logica;
using OrquideaShoes.Modelos;

namespace OrquideaShoes
{
    public partial class AgregarNuevoArticulo : Form
    {
        private Articulo _articulo;
        private bool _esEdicion = false;

        public AgregarNuevoArticulo()
        {
            InitializeComponent();
            _articulo = new Articulo();
        }

        public AgregarNuevoArticulo(Articulo articulo)
        {
            InitializeComponent();
            _articulo = articulo;
            _esEdicion = true;
            CargarDatosArticulo();
        }

        private void CargarDatosArticulo()
        {
            if (_articulo != null)
            {
                textBox1.Text = _articulo.Codigo;
                textBox2.Text = _articulo.Descripcion;
                textBox3.Text = _articulo.Precio.ToString();
                textBox4.Text = _articulo.Stock.ToString();
                // Deshabilitar código en modo edición
                textBox1.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Se mantiene el método existente
        }

        private void AgregarNuevoArticulo_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar el título según sea nuevo o edición
            this.Text = _esEdicion ? "Editar Artículo" : "Agregar Nuevo Artículo";

            // Configurar validaciones de los TextBox
            textBox1.MaxLength = 10; // Código
            textBox2.MaxLength = 100; // Descripción

            // Si es edición, deshabilitar código
            if (_esEdicion)
            {
                textBox1.Enabled = false;
            }
        }

        // Se mantienen los eventos existentes
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Evento del código
            if (textBox1.Text.Length > 0)
                textBox1.Text = textBox1.Text.ToUpper();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Evento de la descripción
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Validar que solo se ingresen números y un punto decimal
            if (textBox3.Text.Length > 0 && !decimal.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Solo se permiten números en el precio", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Validar que solo se ingresen números en el stock
            if (textBox4.Text.Length > 0 && !int.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Solo se permiten números enteros en el stock", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 1);
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Se mantiene el evento existente
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            // Se mantiene el evento existente
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            // Se mantiene el evento existente
        }

        // Nuevo método para el botón guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    // Actualizar o crear artículo
                    _articulo.Codigo = textBox1.Text.Trim();
                    _articulo.Descripcion = textBox2.Text.Trim();
                    _articulo.Precio = decimal.Parse(textBox3.Text);
                    _articulo.Stock = int.Parse(textBox4.Text);

                    if (!_esEdicion)
                    {
                        _articulo.Id = AlmacenDatos.Articulos.Count + 1;
                        AlmacenDatos.Articulos.Add(_articulo);
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
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios",
                    "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!_esEdicion && AlmacenDatos.Articulos.Any(a => a.Codigo == textBox1.Text.Trim()))
            {
                MessageBox.Show("Ya existe un artículo con este código",
                    "Código duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(textBox3.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(textBox4.Text, out int stock))
            {
                MessageBox.Show("El stock debe ser un número entero",
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // Nuevo método para el botón cancelar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cancelar la operación?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LimpiarCampos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }
    }
}