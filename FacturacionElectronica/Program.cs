using FacturacionElectronica.Conexion;
using FacturacionElectronica.Controlador;
using FacturacionElectronica.DAO;
using FacturacionElectronica.Modelo;
using FacturacionElectronica.Vista;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace FacturacionElectronica
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Login());

            /*
            Utilitarios util = new Utilitarios();
            MessageBox.Show(util.EncodinHex(NumFac));
            
            ConstructorPDF mi = new ConstructorPDF();
            mi.Construir("980000003");
            /*
            String Url = "D:/" + "ws_f0830025600003a699d03" + ".zip";
            Correos mi = new Correos();
            CorreosDAO miDao = new CorreosDAO();
            mi = miDao.getConfiguracion("11442244");
            MessageBox.Show(mi.toString());
            MessageBox.Show(mi.SendEmail(Url));
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            //Application.Run(new ContenedorFacturas("1015435094")); // Login() , ContenedorFacturas()
            //EnviarADian miEnviar = new EnviarADian();
            //miEnviar.Enviar("271");
            /*
            SendQuote sendQuote = new SendQuote();
            sendQuote.Enviar("271");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ContenedorFacturas());
            //Application.Run(new Login());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
            
            FacturasDAO mi = new FacturasDAO();
            Tercero listado = mi.InfoTercero(" 78693564");
            
                MessageBox.Show(listado.toXML());
            */

        }
    }
}
