using System;
using System.Linq;
using System.Windows.Forms;
using OrquideaShoes.Modelos; // Asegúrate de incluir el espacio de nombres donde están tus modelos

namespace OrquideaShoes
{
    public partial class NuevoRegistroDeVentas : Form
    {
        private Venta ventaActual;

        public NuevoRegistroDeVentas()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Inicializar la venta actual
            ventaActual = new Venta();

            // Configurar controles
            this.Text = "Nuevo Registro de Ventas";
            this.Size = new System.Drawing.Size(900, 600);

            // Etiquetas y cuadros de texto
            Label lblFactura = new Label() { Text = "Número de Factura:", Location = new System.Drawing.Point(20, 20) };
            TextBox txtFactura = new TextBox() { Location = new System.Drawing.Point(150, 20), Width = 200 };

            Label lblFecha = new Label() { Text = "Fecha:", Location = new System.Drawing.Point(20, 60) };
            DateTimePicker dtpFecha = new DateTimePicker() { Location = new System.Drawing.Point(150, 60), Width = 200 };

            Label lblCliente = new Label() { Text = "Cliente:", Location = new System.Drawing.Point(20, 100) };
            TextBox txtCliente = new TextBox() { Location = new System.Drawing.Point(150, 100), Width = 200 };

            // Controles para los detalles de venta
            Label lblArticulo = new Label() { Text = "Artículo:", Location = new System.Drawing.Point(20, 160) };
            TextBox txtArticulo = new TextBox() { Location = new System.Drawing.Point(150, 160), Width = 200 };

            Label lblCantidad = new Label() { Text = "Cantidad:", Location = new System.Drawing.Point(20, 200) };
            NumericUpDown numCantidad = new NumericUpDown() { Location = new System.Drawing.Point(150, 200), Width = 200, Minimum = 1 };

            Label lblPrecioUnitario = new Label() { Text = "Precio Unitario:", Location = new System.Drawing.Point(20, 240) };
            TextBox txtPrecioUnitario = new TextBox() { Location = new System.Drawing.Point(150, 240), Width = 200 };

            Button btnAgregarDetalle = new Button() { Text = "Agregar Detalle", Location = new System.Drawing.Point(20, 280) };

            // DataGridView para mostrar detalles de la venta
            DataGridView dgvDetalles = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 320),
                Size = new System.Drawing.Size(850, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvDetalles.Columns.Add("articulo", "Artículo");
            dgvDetalles.Columns.Add("cantidad", "Cantidad");
            dgvDetalles.Columns.Add("precioUnitario", "Precio Unitario");
            dgvDetalles.Columns.Add("subtotal", "Subtotal");

            // Botones para finalizar la venta
            Button btnFinalizarVenta = new Button() { Text = "Finalizar Venta", Location = new System.Drawing.Point(20, 540) };

            // Agregar controles al formulario
            this.Controls.Add(lblFactura);
            this.Controls.Add(txtFactura);
            this.Controls.Add(lblFecha);
            this.Controls.Add(dtpFecha);
            this.Controls.Add(lblCliente);
            this.Controls.Add(txtCliente);
            this.Controls.Add(lblArticulo);
            this.Controls.Add(txtArticulo);
            this.Controls.Add(lblCantidad);
            this.Controls.Add(numCantidad);
            this.Controls.Add(lblPrecioUnitario);
            this.Controls.Add(txtPrecioUnitario);
            this.Controls.Add(btnAgregarDetalle);
            this.Controls.Add(dgvDetalles);
            this.Controls.Add(btnFinalizarVenta);

            // Eventos de los botones
            btnAgregarDetalle.Click += (sender, e) =>
            {
                try
                {
                    // Validar entrada
                    if (string.IsNullOrWhiteSpace(txtArticulo.Text) || string.IsNullOrWhiteSpace(txtPrecioUnitario.Text))
                    {
                        MessageBox.Show("Por favor, complete todos los campos del detalle.");
                        return;
                    }

                    // Crear el artículo y detalle
                    string articuloNombre = txtArticulo.Text;
                    int cantidad = (int)numCantidad.Value;
                    decimal precioUnitario = decimal.Parse(txtPrecioUnitario.Text);
                    decimal subtotal = cantidad * precioUnitario;

                    Articulo articulo = new Articulo { Nombre = articuloNombre };
                    DetalleVenta detalle = new DetalleVenta
                    {
                        Articulo = articulo,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnitario
                    };

                    // Agregar detalle a la venta actual
                    ventaActual.Detalles.Add(detalle);

                    // Mostrar detalle en DataGridView
                    dgvDetalles.Rows.Add(articulo.Nombre, detalle.Cantidad, detalle.PrecioUnitario, detalle.Subtotal);

                    // Limpiar campos de detalle
                    txtArticulo.Clear();
                    numCantidad.Value = 1;
                    txtPrecioUnitario.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar detalle: " + ex.Message);
                }
            };

            btnFinalizarVenta.Click += (sender, e) =>
            {
                try
                {
                    // Validar campos de venta
                    if (string.IsNullOrWhiteSpace(txtFactura.Text) || string.IsNullOrWhiteSpace(txtCliente.Text))
                    {
                        MessageBox.Show("Por favor, complete los campos de la venta.");
                        return;
                    }

                    // Configurar datos de la venta
                    ventaActual.NumeroFactura = txtFactura.Text;
                    ventaActual.Fecha = dtpFecha.Value;
                    ventaActual.Cliente = new Cliente { Nombre = txtCliente.Text };

                    // Mostrar resumen de la venta
                    MessageBox.Show($"Venta registrada con éxito.\nTotal: {ventaActual.Total:C}\nNúmero de Detalles: {ventaActual.Detalles.Count}");

                    // Reiniciar la venta actual
                    ventaActual = new Venta();
                    dgvDetalles.Rows.Clear();
                    txtFactura.Clear();
                    txtCliente.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al finalizar la venta: " + ex.Message);
                }
            };
        }

        private void NuevoRegistroDeVentas_Load(object sender, EventArgs e)
        {

        }
    }
}
