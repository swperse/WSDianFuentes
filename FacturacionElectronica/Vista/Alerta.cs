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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica
{
    public partial class Alerta : Form
    {
        public Alerta()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public Alerta(string titulo, string contenido)
        {
            InitializeComponent();
            this.Text = titulo;
            lblContenido.Text = contenido;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void Mostrar(string titulo, string contenido, int tiempo)
        {
            bool done = false;
            ThreadPool.QueueUserWorkItem((x) =>
            {
                using (var splashForm = new Alerta(titulo, contenido))
                {
                    splashForm.Show();
                    while (!done)
                        Application.DoEvents();                    
                    splashForm.Close();
                }
            });
            Thread.Sleep(tiempo);
            done = true;
        }

        public void Cerrar()
        {
            this.Close();
        }

    }
}
