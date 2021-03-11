using FacturacionElectronica.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.DAO
{
    class CorreoDAO
    {
        Connection Con;
        SqlCommand Cmd;
        public Correo getConfiguracion()
        {
            Con = Connection.Instancia;
            String SqlSelect;
            String Url = "";
            Correo miCorreo;
            //try
            //{
            Con.OpenCon();
            miCorreo = new Correo();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " select Usuario,Correo, Password,Nombre,Cargo1,Movil1,Fijo1,Dominio,Servidor,Puerto,Abs,Copia " +
                        "   from Correos, CorreosDominio " +
                        "  where Substring (Correos.Correo, CHARINDEX('@', Correos.Correo), LEN(Correos.Correo)) = CorreosDominio.Dominio " + 
                        "    and Usuario = '11442244'";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        miCorreo.Usuario = reader.getString(0);as 
                    }
                }
            }
            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                miCorreo = null;
                Url = "";
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Manifiestos Detalles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return miCorreo;
        }
    }
}
