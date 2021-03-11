using FacturacionElectronica.Conexion;
using FacturacionElectronica.DAO;
using FacturacionElectronica.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Loading()
        {
            String Usuario;
            String Password;
            Usuario = TxtUsuario.Text;
            Password = TxtPassword.Text;
            /*try
            {*/
                UsuarioDAO nuevaAux = new UsuarioDAO();
                int sesion = nuevaAux.ValidarUsuarioPassword(Usuario, Password);
                switch (sesion)
                {
                    case -3:
                        MessageBox.Show("Usuario No existente", "INICIO DE SESION",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -2:
                        MessageBox.Show("Password equivocado \n Por favor intentelo nuevamente", "INICIO DE SESION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case -1:
                        MessageBox.Show("Acceso Concedido", "INICIO DE SESION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Application.EnableVisualStyles();
                        ContenedorFacturas Contenedor = new ContenedorFacturas(Usuario);
                        Contenedor.ShowDialog();
                        Contenedor = null;
                        this.Dispose();
                        break;
                    case 0:
                        MessageBox.Show("Permisos insuficientes para el \nusuario : " + Usuario, "INICIO DE SESION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        break;
                }

            /*}
            catch (Exception ex)
            {
                MessageBox.Show("Error Admision : " + ex.Message, "INICIO DE SESION",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void StartLoading()
        {
            Configuracion miConfi;
            miConfi = Configuracion.Instancia;
            int ResValidacion = miConfi.validarLicencia();
            if (ResValidacion == 0)
            {
                MessageBox.Show("Hoy vence su licencia, para más dias por favor comunicarse con su proveedor.", "LICENCIA DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Loading();
            }
            else if (ResValidacion == -1)
            {
                MessageBox.Show("Error Admision : Su licencia ha caducado,por favor comunicarse con su proveedor.", "LICENCIA DEL SISTEMA",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ResValidacion == 1)
            {

                Loading();
            }
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            StartLoading();
            Cursor.Current = Cursors.Default;
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyValue) == 13)
            {
                StartLoading();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void LblLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
