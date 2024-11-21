using OrquideaShoes.Logica;
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
            ConfigurarDataGridView();

            CargarDatos();
        }

        private void volver(object sender, EventArgs e)
        {
            var menu = new Opciones();
            this.Hide();
            menu.FormClosed += (s, args) => this.Close();
            menu.Show();

        }

        private void ConfigurarDataGridView()
        {
            // Configuración básica
            dgVentas.AutoGenerateColumns = false;
            dgVentas.AllowUserToAddRows = false;
            dgVentas.AllowUserToDeleteRows = false;
            dgVentas.ReadOnly = true;
            dgVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgVentas.MultiSelect = false;
            dgVentas.BackgroundColor = Color.White;
            dgVentas.BorderStyle = BorderStyle.None;
            dgVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgVentas.GridColor = Color.FromArgb(255, 192, 255);
            //dgVentas.Dock = DockStyle.Fill;

            // Configurar columnas
            dgVentas.Columns.AddRange(new DataGridViewColumn[]
            {
        new DataGridViewTextBoxColumn
        {
            Name = "Numero",
            HeaderText = "No.",
            DataPropertyName = "Numero",
            Width = 40,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        },
         new DataGridViewTextBoxColumn
        {
            Name = "Fecha",
            HeaderText = "Fecha",
            DataPropertyName = "Fecha",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 100
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Articulo",
            HeaderText = "Articulo",
            DataPropertyName = "Articulo",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft
            },
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Valor Unitario",
            HeaderText = "Valor unitario",
            DataPropertyName = "Valor",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 100
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Cantidad",
            HeaderText = "Cantidad",
            DataPropertyName = "Cantidad",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 100
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Subtotal",
            HeaderText = "Subtotal",
            DataPropertyName = "Subtotal",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 80
        },
            });

            // Estilo del encabezado
            dgVentas.EnableHeadersVisualStyles = false;
            dgVentas.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 128, 255),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(255, 128, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True,
                Padding = new Padding(5)
            };
            dgVentas.ColumnHeadersHeight = 50;

            // Estilo de las filas
            dgVentas.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(255, 192, 255),
                SelectionForeColor = Color.Black,
                Font = new Font("Segoe UI", 9),
                Padding = new Padding(4)
            };
            dgVentas.RowTemplate.Height = 35;
            dgVentas.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 248, 255)
            };
        }

        private void CargarDatos()
        {
            try
            {
                dgVentas.Rows.Clear();

                // Recorrer cada venta en la lista de ventas
                foreach (var venta in AlmacenDatos.Ventas)
                {
                    foreach (var detalle in venta.Detalles)
                    {
                        // Agregar una fila por cada detalle de venta
                        dgVentas.Rows.Add(
                            venta.NumeroFactura,                  // No.
                            venta.Fecha.ToString("dd/MM/yyyy"),   // Fecha
                            detalle.Articulo.Descripcion,             // Artículo
                            detalle.Articulo.Precio,
                            detalle.Cantidad,                    // Cantidad
                            detalle.Subtotal                     // Subtotal
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                var formAgregar = new NuevoRegistroDeVentas();
                if (formAgregar.ShowDialog() == DialogResult.OK)
                {
                    CargarDatos();
                    MessageBox.Show("Venta agregada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
