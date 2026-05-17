using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.DTOs.User;
using WinFormsClient;

namespace WinFormsApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Por favor, complete todos los campos.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnLogin.Enabled = false;
                btnLogin.Text = "Iniciando sesión...";

                var loginDto = new UserLoginDTO
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text
                };

                var response = await Program.AuthClient.LoginAsync(loginDto);

                if (response != null)
                {
                    var userRole = SessionManager.GetUserRole();

                    if (string.Equals(userRole, "Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Acceso denegado. Solo administradores pueden iniciar sesión.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Program.AuthClient.Logout();
                    }
                }
                else
                {
                    MessageBox.Show("Email o contraseña incorrectos.", "Error de autenticación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el servidor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
            }
        }
    }
}
