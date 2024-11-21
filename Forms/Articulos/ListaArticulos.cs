using OrquideaShoes.Logica;
using OrquideaShoes.Modelos;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OrquideaShoes
{
    public partial class ListaArticulos : Form
    {
        // Agregar variable para el tipo de filtro actual
        private string filtroActual = "TODOS";
        public ListaArticulos()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            ConfigurarFiltros(); // Nuevo método

            AlmacenDatos.CargarArticulosIniciales();
            CalcularEstadisticas();
            CargarDatos();
        }
   
        // Nuevo método para aplicar filtros
        private void AplicarFiltros(string textoBusqueda, string tipoFiltro)
        {
            var query = AlmacenDatos.Articulos.AsEnumerable();

            // Aplicar filtro de texto
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Where(a =>
                    a.Codigo.ToUpper().Contains(textoBusqueda.ToUpper()) ||
                    a.Descripcion.ToUpper().Contains(textoBusqueda.ToUpper()));
            }

            // Aplicar filtro de tipo
            switch (tipoFiltro)
            {
                case "AGOTADOS":
                    query = query.Where(a => a.Stock == 0);
                    break;
                case "STOCK BAJO":
                    query = query.Where(a => a.Stock > 0 && a.Stock <= 10);
                    break;
                case "CLASIFICACIÓN A":
                    query = query.Where(a => a.Clasificacion == "A");
                    break;
                case "CLASIFICACIÓN B":
                    query = query.Where(a => a.Clasificacion == "B");
                    break;
                case "CLASIFICACIÓN C":
                    query = query.Where(a => a.Clasificacion == "C");
                    break;
            }

            // Ordenar y mostrar resultados
            var resultados = query.OrderByDescending(a => a.TotalVendido).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = resultados;

            // Actualizar el título con el conteo
            ActualizarTituloConteo(resultados.Count);
        }

        // Nuevo método para actualizar el título con el conteo
        private void ActualizarTituloConteo(int cantidad)
        {
            string titulo = $"Lista de Artículos - {cantidad} ";

            switch (filtroActual)
            {
                case "AGOTADOS":
                    titulo += "productos agotados";
                    break;
                case "STOCK BAJO":
                    titulo += "productos con stock bajo";
                    break;
                case "CLASIFICACIÓN A":
                case "CLASIFICACIÓN B":
                case "CLASIFICACIÓN C":
                    titulo += $"productos clasificación {filtroActual.Last()}";
                    break;
                default:
                    titulo += "productos encontrados";
                    break;
            }

            this.Text = titulo;
        }

        private void ConfigurarFiltros()
        {
            // Panel para filtros
            Panel panelFiltros = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(255, 128, 255) // Color que combina con el botón
            };

            // TextBox para búsqueda con diseño mejorado
            TextBox txtBuscar = new TextBox
            {
                Location = new Point(20, 18),
                Size = new Size(200, 24),
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Text = "Buscar artículo...",
                BorderStyle = BorderStyle.FixedSingle
            };

            // ComboBox para filtros con diseño mejorado
            ComboBox cmbFiltros = new ComboBox
            {
                Location = new Point(240, 18),
                Size = new Size(150, 24),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Label para mostrar el conteo
            Label lblConteo = new Label
            {
                Location = new Point(410, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.White
            };

            // Agregar opciones al ComboBox
            cmbFiltros.Items.AddRange(new string[] {
        "TODOS",
        "AGOTADOS",
        "STOCK BAJO",
        "CLASIFICACIÓN A",
        "CLASIFICACIÓN B",
        "CLASIFICACIÓN C"
    });
            cmbFiltros.SelectedIndex = 0;

            // Eventos del TextBox
            txtBuscar.GotFocus += (s, e) =>
            {
                if (txtBuscar.Text == "Buscar artículo...")
                {
                    txtBuscar.Text = "";
                    txtBuscar.ForeColor = Color.Black;
                }
            };

            txtBuscar.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    txtBuscar.Text = "Buscar artículo...";
                    txtBuscar.ForeColor = Color.Gray;
                }
            };

            txtBuscar.TextChanged += (s, e) =>
            {
                if (txtBuscar.Text != "Buscar artículo...")
                {
                    AplicarFiltros(txtBuscar.Text, cmbFiltros.SelectedItem.ToString());
                }
            };

            // Evento del ComboBox
            cmbFiltros.SelectedIndexChanged += (s, e) =>
            {
                filtroActual = cmbFiltros.SelectedItem.ToString();
                string textoBusqueda = txtBuscar.Text == "Buscar artículo..." ? "" : txtBuscar.Text;
                AplicarFiltros(textoBusqueda, filtroActual);
            };

            // Agregar controles al panel
            panelFiltros.Controls.AddRange(new Control[] { txtBuscar, cmbFiltros, lblConteo });
            this.Controls.Add(panelFiltros);

            // Ajustar posición del panel principal
            panel1.Top = panelFiltros.Bottom + 5;
            panel1.Height = this.ClientSize.Height - panelFiltros.Height - agregarArticulo.Height - 40;
        }

        private void ConfigurarDataGridView()
        {
            // Configuración básica
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(255, 192, 255);
            dataGridView1.Dock = DockStyle.Fill;

            // Configurar columnas
            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
            {
        new DataGridViewTextBoxColumn
        {
            Name = "Numero",
            HeaderText = "No.",
            DataPropertyName = "Numero",
            Width = 50,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Descripcion",
            HeaderText = "Código del Artículo",
            DataPropertyName = "Descripcion",
            Width = 200
        },
        new DataGridViewTextBoxColumn
        {
            Name = "CantidadVendida",
            HeaderText = "Cantidad de Pares vendidos",
            DataPropertyName = "Stock",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 100
        },
        new DataGridViewTextBoxColumn
        {
            Name = "PVP",
            HeaderText = "PVP",
            DataPropertyName = "Precio",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 80
        },
        new DataGridViewTextBoxColumn
        {
            Name = "TotalVendido",
            HeaderText = "Total Vendido ($)",
            DataPropertyName = "TotalVendido",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 120
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Participacion",
            HeaderText = "% Participación",
            DataPropertyName = "Participacion",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "P2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 100
        },
        new DataGridViewTextBoxColumn
        {
            Name = "ParticipacionAcumulada",
            HeaderText = "Participación Acumulada",
            DataPropertyName = "ParticipacionAcumulada",
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "P2",
                Alignment = DataGridViewContentAlignment.MiddleRight
            },
            Width = 120
        },
        new DataGridViewTextBoxColumn
        {
            Name = "Clasificacion",
            HeaderText = "Clasificación",
            DataPropertyName = "Clasificacion",
            Width = 80,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            }
        }
            });

            // Estilo del encabezado
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 128, 255),
                ForeColor = Color.White,
                SelectionBackColor = Color.FromArgb(255, 128, 255),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                WrapMode = DataGridViewTriState.True,
                Padding = new Padding(5)
            };
            dataGridView1.ColumnHeadersHeight = 50;

            // Estilo de las filas
            dataGridView1.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(255, 192, 255),
                SelectionForeColor = Color.Black,
                Font = new Font("Segoe UI", 9),
                Padding = new Padding(4)
            };
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(255, 248, 255)
            };
        }

        private void CalcularEstadisticas()
        {
            try
            {
                decimal totalVentas = AlmacenDatos.Articulos.Sum(a => a.Precio * a.Stock);

                var articulosOrdenados = AlmacenDatos.Articulos
                    .OrderByDescending(a => a.Precio * a.Stock)
                    .ToList();

                for (int i = 0; i < articulosOrdenados.Count; i++)
                {
                    var articulo = articulosOrdenados[i];
                    articulo.Numero = i + 1;
                    articulo.TotalVendido = articulo.Precio * articulo.Stock;
                    articulo.Participacion = totalVentas > 0 ? articulo.TotalVendido / totalVentas : 0;
                }

                decimal acumulado = 0;
                foreach (var articulo in articulosOrdenados)
                {
                    acumulado += articulo.Participacion;
                    articulo.ParticipacionAcumulada = acumulado;

                    if(articulo.Clasificacion == null || articulo.Clasificacion.Length <= 0)
                    {
                        if (acumulado <= 0.80m)
                            articulo.Clasificacion = "A";
                        else if (acumulado <= 0.95m)
                            articulo.Clasificacion = "B";
                        else
                            articulo.Clasificacion = "C";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = AlmacenDatos.Articulos
                    .OrderByDescending(a => a.TotalVendido)
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                string clasificacion = row.Cells["Clasificacion"].Value?.ToString();

                switch (clasificacion)
                {
                    case "A":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 234, 234); // Rosa claro
                        break;
                    case "B":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 247, 255); // Azul claro
                        break;
                    case "C":
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 253, 234); // Amarillo claro
                        break;
                }
            }
        }

        // Mantener los métodos existentes
        private void ListaArticulos_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aquí puedes agregar la lógica para cuando se hace clic en una celda
            // Por ejemplo, abrir un formulario de edición
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var formAgregar = new AgregarNuevoArticulo();
                if (formAgregar.ShowDialog() == DialogResult.OK)
                {
                    CalcularEstadisticas();
                    CargarDatos();
                    MessageBox.Show("Artículo agregado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Aquí puedes agregar la lógica para el pictureBox
            // Por ejemplo, cerrar el formulario
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             var menu = new Opciones();
            this.Hide();
            menu.FormClosed += (s, args) => this.Close();
            menu.Show();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Desea eliminar" + e.RowIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("No se ha seleccionado ninguna fila.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int articuloId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var articuloAEliminar = AlmacenDatos.Articulos.FirstOrDefault(a => a.Id == articuloId);

            if (articuloAEliminar == null)
            {
                MessageBox.Show("No se encontró el artículo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var deleteConfirmation = MessageBox.Show(
                $"¿Deseas eliminar el artículo '{articuloAEliminar.Descripcion}'?",
                "Eliminar producto",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (deleteConfirmation == DialogResult.Yes)
            {
                AlmacenDatos.Articulos.Remove(articuloAEliminar);
                CargarDatos();
                MessageBox.Show("El artículo fue eliminado exitosamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}