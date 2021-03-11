using FacturacionElectronica.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionElectronica.Conexion;

namespace FacturacionElectronica.Modelo
{
    class Correos
    {
        private String _Usuario;
        private String _CorreoEmail;
        private String _Password;
        private String _Nombre;
        private String _Cargo;
        private String _Movil;
        private String _Fijo;
        private String _Dominio;
        private String _Servidor;
        private String _Puerto;
        private String _Abs;
        private String _Copia;
        private string Directorio = AppDomain.CurrentDomain.BaseDirectory;

#region gettersSetters
        public string Usuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
            }
        }

        public string CorreoEmail
        {
            get
            {
                return _CorreoEmail;
            }

            set
            {
                _CorreoEmail = value;
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }

            set
            {
                _Password = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }

        public string Cargo
        {
            get
            {
                return _Cargo;
            }

            set
            {
                _Cargo = value;
            }
        }

        public string Movil
        {
            get
            {
                return _Movil;
            }

            set
            {
                _Movil = value;
            }
        }

        public string Fijo
        {
            get
            {
                return _Fijo;
            }

            set
            {
                _Fijo = value;
            }
        }

        public string Dominio
        {
            get
            {
                return _Dominio;
            }

            set
            {
                _Dominio = value;
            }
        }

        public string Servidor
        {
            get
            {
                return _Servidor;
            }

            set
            {
                _Servidor = value;
            }
        }

        public string Puerto
        {
            get
            {
                return _Puerto;
            }

            set
            {
                _Puerto = value;
            }
        }

        public string Abs
        {
            get
            {
                return _Abs;
            }

            set
            {
                _Abs = value;
            }
        }

        public string Copia
        {
            get
            {
                return _Copia;
            }

            set
            {
                _Copia = value;
            }
        }

#endregion

        public String SendEmail(List<string> adjuntos, String NombreDestino, String CorreoDestino, string documentoNumero, string tipoDocumento, string resolucion)
        {
            FacturasDAO facDAO = new FacturasDAO();
            String respuesta = "";
            try
            {
                var miFactura = new Factura();

                var documento = "";
                var tipoFactura = "";
                if (tipoDocumento == "F")
                {
                    documento = "Factura";
                    tipoFactura = "01";
                    miFactura = facDAO.selectManDetalles(documentoNumero, tipoDocumento, resolucion);
                }
                else if (tipoDocumento == "D")
                {
                    documento = "Nota debito";
                    tipoFactura = "92";
                    miFactura = facDAO.selectManDetallesDebito(documentoNumero, tipoDocumento, resolucion);
                }
                else
                {
                    documento = "Nota credito";
                    tipoFactura = "91";
                    miFactura = facDAO.selectManDetallesCredito(documentoNumero, tipoDocumento, resolucion);
                }

                var productos = facDAO.ListadoProductos(tipoDocumento == "F" ? documentoNumero : miFactura.Facturanumero, documentoNumero, tipoDocumento, resolucion, miFactura.TRM);
                var adquiriente = facDAO.InfoAdquiriente();
                var tercero = facDAO.InfoTercero(miFactura.Cliente);


                MailAddress from = new MailAddress(this.CorreoEmail, this.Nombre);
                MailAddress to = new MailAddress(CorreoDestino, NombreDestino);
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                myMail.Subject = adquiriente.Nit + ";" + adquiriente.NombreUsuario + ";" + resolucion + documentoNumero + ";" + tipoFactura + ";" + adquiriente.NombreUsuario + ";";

                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                var datosNotificacion = this.Servidor + ";" + this.Puerto + ";" + this.CorreoEmail + ";" + this.Password + ";" + documento
                        + ";" + documentoNumero + ";" + CorreoDestino;

                var apiExterno = "https://crm.cru.org.co/ApiExterno.ashx?data=" + datosNotificacion;

                string styleButton1 = "background-color: #15A3C8; border: none; color: white; padding: 13px 24px; text-align: center;text-decoration: none;display: inline-block;font-size: 16px;";
                string styleButton2 = "background-color: #f44336;; border: none; color: white; padding: 13px 20px; text-align: center;text-decoration: none;display: inline-block;font-size: 16px;";



                String Firma = "";

                if (Configuracion.Instancia.URL_EnviosValidacion != null)
                {
                    Firma = "<br>Estimado(a) Sr(a) " + tercero.Nombre +
                        "<br>" +
                        "<br>" +
                        "<br>Reciba un cordial saludo," +
                        "<br>" +
                        "<br>" +
                        "<br>Adjunto enviamos la factura " + resolucion + documentoNumero + " con fecha " + Convert.ToDateTime(miFactura.Fechafac).ToString("yyyy/MM/dd") + " para los fines pertinentes." +
                        "<br>" +
                        "<br>" +
                        "<br>Saludos y quedamos atentos." +
                        "<br><br>" +
                        "<br><br>" +
                        "<br>Cordialmente," +
                        "<br><br>" +
                        "<B><font color=Teal size = 4>" + this.Nombre + "</font></B><br>" +
                        "<B><font size=3>" + this.Cargo + "</font></B><br>" +
                        "<B><font color=Teal>Móvil :</font></B> " + this.Movil + "<br>" +
                        "<B><font color=Teal>Fijo :</font></B> " + this.Fijo + "<br>" +
                        "<br><br>" +
                        "<br><center><i><b><font size=2>Para dar cumplimiento a los terminos del decreto 2242 lo invitamos a realizar la acepctación o rechazo del documento:</font></center></i></b>" +
                        "<br>" +

                        "<center>" +
                        //  Botones
                        //"   <a style=\"" + styleButton1 + $"\" href=\"https://{Configuracion.Instancia.URL_EnviosValidacion}/Home/ValidarFactura/?Resol={resolucion}&Num_fac={documentoNumero}&acept=true\">" +
                        //"   Aceptar " +
                        //"   </a> " +
                        //"&nbsp; &nbsp; &nbsp;" +
                        //$"<a style=\"" + styleButton2 + $"\" href=\"https://{Configuracion.Instancia.URL_EnviosValidacion}/Home/ValidarFactura/?Resol={resolucion}&Num_fac={documentoNumero}&acept=false\" > Rechazar </a>" +


                        //mailto:?subject=Aceptación&nbsp;Documento&nbsp;Electrónico&amp;body=Aceptar

                        "   <a style=\"" + styleButton1 + $"\" href=\"mailto:?subject=Aceptación_{ resolucion + documentoNumero }&nbsp;Documento&nbsp;Electrónico&amp;body=Aceptar\">" +
                        "   Aceptar " +
                        "   </a> " +
                        "&nbsp; &nbsp; &nbsp;" +
                        $"<a style=\"" + styleButton2 + $"\" href=\"mailto:?subject=Rechazo_{ resolucion + documentoNumero }&nbsp;Documento&nbsp;Electrónico&amp;body=Rechazado\" > Rechazar </a>" +


                        //Body:  https://localhost:44338/Home/ValidarFactura/?Resol=PRUEBA&Num_fac=123&acept=false

                        "</center>" +
                        //
                        //"<center><br>Si está de acuerdo con el documento, ingrese al siguiente link: <br></center>" +
                        //$"<center><B><i><font color=Teal size = 2>{($"https://localhost:44338/Home/ValidarFactura/?Resol={resolucion}&Num_fac={documentoNumero}&acept=true")}</font><i></B><br></center>" +
                        //"<center><br>Si NO está de acuerdo con el documento, ingrese al siguiente link: <br></center>" +
                        //$"<center><B><i><font color=Teal size = 2>{($"https://localhost:44338/Home/ValidarFactura/?Resol={resolucion}&Num_fac={documentoNumero}&acept=false")}</font><i></B><br></center>" +
                        //"<br>" +
                        "<br><center><i><b><font size=2>Si transcurridos 3 días sin recibir confirmación de aceptación o rechazo de este documento, se dará por recibido el documento automáticamente.</font></center></i></b>" +
                        "<br><br>" +
                        "<img style='display:none;' src='" + apiExterno + "'>";
                }
                else
                {
                    Firma = "<br>Estimado(a) Sr(a) " + tercero.Nombre +
                        "<br>" +
                        "<br>" +
                        "<br>Reciba un cordial saludo," +
                        "<br>" +
                        "<br>" +
                        "<br>Adjunto enviamos la factura " + resolucion + documentoNumero + " con fecha " + Convert.ToDateTime(miFactura.Fechafac).ToString("yyyy/MM/dd") + " para los fines pertinentes." +
                        "<br>" +
                        "<br>" +
                        "<br>Saludos y quedamos atentos." +
                        "<br><br>" +
                        "<br><br>" +
                        "<br>Cordialmente," +
                        "<br><br>" +
                        "<B><font color=Teal size = 4>" + this.Nombre + "</font></B><br>" +
                        "<B><font size=3>" + this.Cargo + "</font></B><br>" +
                        "<B><font color=Teal>Móvil :</font></B> " + this.Movil + "<br>" +
                        "<B><font color=Teal>Fijo :</font></B> " + this.Fijo + "<br><br>" +
                        "<img style='display:none;' src='" + apiExterno + "'>";
                }

                myMail.Body = Firma;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;



                myMail.IsBodyHtml = true;

                foreach (var adjunto in adjuntos)
                {
                    string file = Directorio + adjunto;
                    try
                    {
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                        myMail.Attachments.Add(data);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("No se encontró el archivo " + file + " para poder ser adjuntado.");
                    }
                }

                SmtpClient clienteSmtp = new SmtpClient();
                clienteSmtp.Host = this.Servidor;
                clienteSmtp.Port = Convert.ToInt32(this.Puerto);
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                clienteSmtp.Credentials = new System.Net.NetworkCredential(this.CorreoEmail, this.Password);
                clienteSmtp.EnableSsl = true;
                clienteSmtp.Send(myMail);
                facDAO.ActualizarEstadoFacturaCliente(documentoNumero, tipoDocumento, resolucion);
                respuesta = "";

                return respuesta;
            }

            catch (SmtpException ex)
            {
                return "SmtpException " + ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public String toString()
        {
            return " Usuario " + Usuario + "\n" +
                    " CorreoEmail " + CorreoEmail + "\n" +
                    " Password " + Password + "\n" +
                    " Nombre " + Nombre + "\n" +
                    " Cargo " + Cargo + "\n" +
                    " Movil " + Movil + "\n" +
                    " Fijo " + Fijo + "\n" +
                    " Dominio " + Dominio + "\n" +
                    " Servidor " + Servidor + "\n" +
                    " Puerto " + Puerto + "\n" +
                    " Abs " + Abs + "\n" +
                    " Copia " + Copia;
        }
    }
}
