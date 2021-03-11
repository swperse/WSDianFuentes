using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica.Conexion
{
    class Connection
    {
        private static Connection InstanceConexion = null;
        private SqlConnection miCon;
        private Connection()
        {
            try
            {
                Configuracion Config;
                Config = Configuracion.Instancia;

                var connectionString = string.Format("Data Source=localhost;Initial Catalog={0};Integrated Security=SSPI;User ID={1};Password={2};", Config.DataBase, Config.User, Config.Password);

                if (!string.IsNullOrEmpty(Config.InstanciaBD))
                {
                    if (!string.IsNullOrEmpty(Config.NombreServidor))
                    {
                        connectionString = string.Format("Data Source={4}\\{3};Initial Catalog={0};User ID={1};Password={2};", Config.DataBase, Config.User, Config.Password, Config.InstanciaBD, Config.NombreServidor);
                    }
                    else
                    {
                        connectionString = string.Format("Data Source=localhost\\{3};Initial Catalog={0};Integrated Security=SSPI;User ID={1};Password={2};", Config.DataBase, Config.User, Config.Password, Config.InstanciaBD);
                    }
                }
                else if (!string.IsNullOrEmpty(Config.NombreServidor))
                {
                    connectionString = string.Format("Data Source={3};Initial Catalog={0};Integrated Security=SSPI;User ID={1};Password={2};", Config.DataBase, Config.User, Config.Password, Config.NombreServidor);
                }

                Con = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, " Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Connection Instancia
        {
            get
            {
                if (InstanceConexion == null)
                {
                    InstanceConexion = new Connection();
                }
                return InstanceConexion;
            }
        }

        public bool OpenCon()
        {
            bool estado = false;
            try
            {
                estado = true;
                Con.Open();
            }
            catch (Exception ex)
            {
                estado = false;
                MessageBox.Show(" Ocurrió un error : " + ex.Message, " Clase Conexion ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return estado;
        }
        public bool CloseCon()
        {
            bool estado = false;
            try
            {
                estado = true;
                Con.Close();
            }
            catch (Exception ex)
            {
                estado = false;
                MessageBox.Show(" Ocurrió un error : " + ex.Message, " Clase Conexion ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return estado;
        }
        public SqlConnection Con
        {
            get { return miCon; }
            set { miCon = value; }
        }
    }
}
