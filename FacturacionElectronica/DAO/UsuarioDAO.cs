using FacturacionElectronica.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica.DAO
{
    class UsuarioDAO
    {
        Connection Con;

        public int ValidarUsuarioPassword(String Usuario, String Password)
        {
            int respuesta = -3;
            Con = Connection.Instancia;
            String SqlSelect;

            try
            {

                Con.OpenCon();
                SqlSelect = "SELECT DIAN, password FROM Users WHERE "
                                + "usuario = '" + Usuario + "' AND DIAN IS NOT NULL;";

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetString(1).Equals(Password))
                            {
                                respuesta = reader.GetInt32(0);
                            }
                            else
                            {
                                respuesta = -2;
                            }
                        }
                        else
                        {
                            respuesta = -3;
                        }
                    }
                }
                Con.CloseCon();

            }

            catch (Exception ex)
            {
                respuesta = -3;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Usuarios",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return respuesta;
        }
    }
}
