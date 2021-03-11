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
    class AuditoriaDAO
    {
        Connection Con;
        // SqlCommand Cmd;
        //SqlDataAdapter DAdapter;

        public String RegistrarAuditoria(String Encargado, String Documento, String Comprobante, int Tipo)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            String Resultado = "";

            try
            {
                Con.OpenCon();
                // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
                if (Tipo == 1)
                {
                    SqlSelect = "Insert into Auditoria (Fecha,Hora,Nombre,Documento,Comprobante,Tipo,Transac) " +
                                "values (GETDATE(),GETDATE(),'" + Encargado + "','" + Documento + "','" + Comprobante + "','WSDIAN-ENVIODIAN','Admon'); ";
                }
                else
                {
                    SqlSelect = "Insert into Auditoria (Fecha,Hora,Nombre,Documento,Comprobante,Tipo,Transac) " +
                                "values (GETDATE(),GETDATE(),'" + Encargado + "','" + Documento + "','" + Comprobante + "','WSDIAN-ENVIOCLIENTE','Admon'); ";
                }
                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    command.ExecuteNonQuery();
                    
                }
                Con.CloseCon();
                Resultado = "Ok";
            }
            catch (Exception ex)
            {
                Resultado = "";
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Manifiestos Detalles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Resultado;
        }
    }
}
