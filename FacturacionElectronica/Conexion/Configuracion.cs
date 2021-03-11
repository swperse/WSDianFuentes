using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica.Conexion
{
    class Configuracion
    {
        private static Configuracion InstanceConexion = null;

        string _UserDian;
        string _PswDian;
        string _Firma;
        string _NombreEmpresa;
        string _NombreCertificado;
        string _ContraseñaCertificado;
        string _FL;
        string _DataBase;
        string _User;
        string _Password;
        string _InstanceDB;
        string _ServerName;

        //  URL Externo en la cual el cliente enviará la validación de la factura
        public string URL_EnviosValidacion { get; set; }

        private Configuracion()
        {

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = currentDir + "Configura.properties"; //Configura
            string Atributo;
            string Valor;
            try
            {
                if (File.Exists(path))
                {

                    string[] readText = File.ReadAllLines(path);
                    foreach (string s in readText)
                    {
                        if (s.Trim() != string.Empty)
                        {
                            Atributo = s.Split('=')[0];
                            Valor = s.Split('=')[1];
                            if (Atributo.Equals("UserDian"))
                            {
                                this.UserDian = Valor;
                            }
                            if (Atributo.Equals("PswDian"))
                            {
                                this.PswDian = Valor;
                            }
                            if (Atributo.Equals("NombreEmpresa"))
                            {
                                this.NombreEmpresa = Valor;
                            }
                            if (Atributo.Equals("NombreCertificado"))
                            {
                                this.NombreCertificado = Valor;
                            }
                            if (Atributo.Equals("PswCertificado"))
                            {
                                this.ContraseñaCertificado = Valor;
                            }
                            else if (Atributo.Equals("FL"))
                            {
                                this.FL = Valor;
                            }
                            else if (Atributo.Equals("Firma"))
                            {
                                this.Firma = Valor;
                            }
                            else if (Atributo.Equals("DataBase"))
                            {
                                this.DataBase = Valor;
                            }
                            else if (Atributo.Equals("User"))
                            {
                                this._User = Valor;
                            }
                            else if (Atributo.Equals("Password"))
                            {
                                this._Password = Valor;
                            }
                            else if (Atributo.Equals("InstanciaBD"))
                            {
                                this._InstanceDB = Valor;
                            }
                            else if (Atributo.Equals("NombreServidor"))
                            {
                                this._ServerName = Valor;
                            }
                            else if (Atributo.Equals("URL_EnviosValidacion"))
                            {
                                this.URL_EnviosValidacion = Valor;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Lectura Archivo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static Configuracion Instancia
        {
            get
            {
                if (InstanceConexion == null)
                {
                    InstanceConexion = new Configuracion();
                }
                return InstanceConexion;
            }
        }


        public string Firma
        {
            get
            {
                return _Firma;
            }

            set
            {
                _Firma = value;
            }
        }

        public string FL
        {
            get
            {
                return _FL;
            }

            set
            {
                _FL = value;
            }
        }

        public string UserDian
        {
            get
            {
                return _UserDian;
            }

            set
            {
                _UserDian = value;
            }
        }

        public string PswDian
        {
            get
            {
                return _PswDian;
            }

            set
            {
                _PswDian = value;
            }
        }

        public string NombreEmpresa
        {
            get
            {
                return _NombreEmpresa;
            }

            set
            {
                _NombreEmpresa = value;
            }
        }

        public string NombreCertificado
        {
            get
            {
                return _NombreCertificado;
            }

            set
            {
                _NombreCertificado = value;
            }
        }

        public string ContraseñaCertificado
        {
            get
            {
                return _ContraseñaCertificado;
            }

            set
            {
                _ContraseñaCertificado = value;
            }
        }

        public string DataBase
        {
            get
            {
                return _DataBase;
            }

            set
            {
                _DataBase = value;
            }
        }

        public string User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = value;
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

        public string InstanciaBD
        {
            get
            {
                return _InstanceDB;
            }

            set
            {
                _InstanceDB = value;
            }
        }
        public string NombreServidor
        {
            get
            {
                return _ServerName;
            }

            set
            {
                _ServerName = value;
            }
        }

        public int validarLicencia()
        {
            try
            {
                DateTime FechaLicencia = DateTime.ParseExact(this.FL, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime FechaSistema = DateTime.ParseExact(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ;

                int Diff = DateTime.Compare(FechaLicencia, FechaSistema);
                if (Diff < 0)
                {
                    return -1;
                }
                else if (Diff == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Configuración - Validar Fecha",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -2;
            }

        }
    }
}
