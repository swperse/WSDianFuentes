using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Modelo
{
    class Correo
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

        public String SendEmail(String UrlDocumento)
        {
            try
            {
                string file = UrlDocumento;
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

                //Servidor
                SmtpClient mySmtpClient = new SmtpClient(this.Servidor); 
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                //Credenciales de Conexion
                System.Net.NetworkCredential(this.CorreoEmail, this.Password);
                mySmtpClient.Credentials = basicAuthenticationInfo;

                // add from,to mailaddresses
                MailAddress from = new MailAddress(this.CorreoEmail, "DAVID FERNANDO PARRA");
                MailAddress to = new MailAddress(this.CorreoEmail, "DAVID FERNANDO PARRA");
                MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
                /*
                // add ReplyTo
                MailAddress replyto = new MailAddress("reply@example.com");
                myMail.ReplyToList.Add(replyTo);
                */
                // set subject and encoding
                myMail.Subject = "Test message";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = "<b>David Fernando Parra</b><br> Ingeniro de Sistemas <b> DIAN </b>.";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.Attachments.Add(data);
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        }
    }
}
