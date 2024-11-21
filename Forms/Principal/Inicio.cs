using OrquideaShoes.Logica;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrquideaShoes
{
    public partial class Inicio : Form
    {
        private readonly GestorUsuarios _gestorUsuarios;
        private int intentosLogin = 0;
        private const int MAX_INTENTOS = 3;

        public Inicio()
        {
            InitializeComponent();
            _gestorUsuarios = new GestorUsuarios();
            ConfigurarFormulario();
            EstablecerPlaceholders();
        }

        private void ConfigurarFormulario()
        {
            // Configurar propiedades del formulario
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Orquidea Shoes - Inicio de Sesión";

            // Configurar TextBoxes
            ConfigurarTextBox(textBox1, "Usuario");
            ConfigurarTextBox(textBox2, "Contraseña");
            textBox2.PasswordChar = '●';
            textBox2.UseSystemPasswordChar = true;

            // Configurar botones
            ConfigurarBotones();

            // Manejar Enter en ambos TextBoxes
            textBox1.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) textBox2.Focus(); };
            textBox2.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) button1.PerformClick(); };

            // Establecer colores y estilos
            this.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void ConfigurarTextBox(TextBox textBox, string placeholder)
        {
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;
            textBox.BorderStyle = BorderStyle.FixedSingle;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    if (textBox == textBox2)
                        textBox2.UseSystemPasswordChar = true;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                    if (textBox == textBox2)
                        textBox2.UseSystemPasswordChar = false;
                }
            };
        }

        private void ConfigurarBotones()
        {
            // Botón de inicio de sesión
            button1.BackColor = Color.FromArgb(255, 128, 255);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Cursor = Cursors.Hand;

            // Botón de cancelar/limpiar
            button2.BackColor = Color.FromArgb(255, 192, 255);
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button2.Font = new Font("Segoe UI", 10F);
            button2.ForeColor = Color.Black;
            button2.Cursor = Cursors.Hand;
            button2.Text = "Limpiar";
        }

        private void EstablecerPlaceholders()
        {
            if (textBox1.Text == "") textBox1.Text = "Usuario";
            if (textBox2.Text == "")
            {
                textBox2.Text = "Contraseña";
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos vacíos o placeholders
                if (textBox1.Text == "Usuario" || textBox2.Text == "Contraseña" ||
                    string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Por favor ingrese usuario y contraseña",
                        "Campos requeridos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Validar credenciales
                if (_gestorUsuarios.ValidarCredenciales(textBox1.Text.Trim(), textBox2.Text.Trim()))
                {
                    var usuario = _gestorUsuarios.ObtenerUsuarioActual();
                    MessageBox.Show($"Bienvenido {usuario.Nombre}",
                        "Acceso exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);


                    var menu = new Opciones();
                    this.Hide();
                    menu.FormClosed += (s, args) => this.Close();

                    menu.Show();


                    
                }
                else
                {
                    intentosLogin++;
                    if (intentosLogin >= MAX_INTENTOS)
                    {
                        MessageBox.Show("Ha excedido el número máximo de intentos.\nLa aplicación se cerrará.",
                            "Acceso bloqueado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show($"Usuario o contraseña incorrectos.\nIntentos restantes: {MAX_INTENTOS - intentosLogin}",
                            "Error de acceso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        textBox2.Clear();
                        textBox2.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar ingresar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            textBox1.Text = "Usuario";
            textBox2.Text = "Contraseña";
            textBox1.ForeColor = Color.Gray;
            textBox2.ForeColor = Color.Gray;
            textBox2.UseSystemPasswordChar = false;
            textBox1.Focus();
        }

        // Eventos para mantener la funcionalidad de los placeholders
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "Usuario" && textBox1.Text.Length > 0)
                textBox1.ForeColor = Color.Black;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "Contraseña" && textBox2.Text.Length > 0)
                textBox2.ForeColor = Color.Black;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
                Application.Exit();
        }
    }
}