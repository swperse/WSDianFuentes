using FacturacionElectronica.Conexion;
using FacturacionElectronica.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica.DAO
{
    class ConfiguracionDIANDAO
    {
        Connection Con;
        //SqlCommand Cmd;
        // SqlDataAdapter DAdapter;

        public ConfiguracionDIAN getConfiguracion(String NroFactura, string idFacturaNota, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            ConfiguracionDIAN Resultado = null;

            try
            {
                if (tipoFactura == "F")
                {
                    Con.OpenCon();

                    SqlSelect = "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.ClTec,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado, CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV  " +
                                "      FROM Resoluciones R, Mostrador P, Terceros T " +
                                "       WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Facturanumero = '" + NroFactura + "' " +
                                "     UNION ALL " +
                                "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.ClTec,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado,CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV " +
                                "      FROM Resoluciones R , Pedidos P, Terceros T " +
                                "      WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Facturanumero = '" + NroFactura + "' ";
                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Resultado = new ConfiguracionDIAN();
                                Resultado.IdConfiguracionDIAN = reader.GetInt32(0);
                                Resultado.NroResolucion = reader.GetString(1);
                                Resultado.FechaResolucion = reader.GetString(2);
                                Resultado.Prefijo = reader.GetString(3);
                                Resultado.Desde = reader.GetString(4);
                                Resultado.Hasta = reader.GetString(5);
                                Resultado.IdProveedor = reader.GetString(6);
                                Resultado.IdSoftware = reader.GetString(7);
                                Resultado.ClaveTecnica = reader.GetString(8);
                                Resultado.NombreSoftware = reader.GetString(9);
                                Resultado.Pin = reader.GetString(10);
                                Resultado.CodigoSeguridad = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                                Resultado.Estado = reader.GetInt32(12);
                                Resultado.Vigencia = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                                Resultado.TipoAmbiente = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                                Resultado.DV = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                            }
                        }
                        Con.CloseCon();
                    }
                }

                if (tipoFactura == "D")
                {
                    Con.OpenCon();

                    SqlSelect = "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.Pin,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado, CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV  " +
                                "      FROM Resoluciones R, NDebitoClientes P, Terceros T " +
                                "       WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Idncredito = '" + NroFactura + "' " +
                                "     UNION ALL " +
                                "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.Pin,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado,CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV  " +
                                "      FROM Resoluciones R , Pedidos P, Terceros T " +
                                "      WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Facturanumero = '" + idFacturaNota + "' ";
                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Resultado = new ConfiguracionDIAN();
                                Resultado.IdConfiguracionDIAN = reader.GetInt32(0);
                                Resultado.NroResolucion = reader.GetString(1);
                                Resultado.FechaResolucion = reader.GetString(2);
                                Resultado.Prefijo = reader.GetString(3);
                                Resultado.Desde = reader.GetString(4);
                                Resultado.Hasta = reader.GetString(5);
                                Resultado.IdProveedor = reader.GetString(6);
                                Resultado.IdSoftware = reader.GetString(7);
                                Resultado.ClaveTecnica = reader.GetString(8);
                                Resultado.NombreSoftware = reader.GetString(9);
                                Resultado.Pin = reader.GetString(10);
                                Resultado.CodigoSeguridad = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                                Resultado.Estado = reader.GetInt32(12);
                                Resultado.Vigencia = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                                Resultado.TipoAmbiente = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                                Resultado.DV = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                            }
                        }
                        Con.CloseCon();
                    }
                }

                if (tipoFactura == "C")
                {
                    Con.OpenCon();

                    SqlSelect = "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.Pin,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado, CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV  " +
                                "      FROM Resoluciones R, NCreditoPos P, Terceros T " +
                                "       WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Idncreditopos = '" + NroFactura + "' " +
                                "     UNION ALL " +
                                "     SELECT R.IdCentros,R.NroResolucion,CONVERT(char(10), R.FechaResolucion,126) FechaResolucion,R.Centro,R.Desde, " +
                                "            R.Hasta,R.IdProveedor,R.IdSoftware,R.Pin,R.NombreSoftware, " +
                                "      	   R.Pin,R.CodigoSeguridad,R.Estado,CONVERT(char(10),R.Vigencia,126) Vigencia, R.TipoAmbiente, T.DV  " +
                                "      FROM Resoluciones R , Pedidos P, Terceros T " +
                                "      WHERE R.Estado = '3' AND R.IdProveedor = T.Identificacion AND R.centro = '" + resolucion + "' AND P.Resolucion ='" + resolucion + "' AND P.Facturanumero = '" + idFacturaNota + "' ";
                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Resultado = new ConfiguracionDIAN();
                                Resultado.IdConfiguracionDIAN = reader.GetInt32(0);
                                Resultado.NroResolucion = reader.GetString(1);
                                Resultado.FechaResolucion = reader.GetString(2);
                                Resultado.Prefijo = reader.GetString(3);
                                Resultado.Desde = reader.GetString(4);
                                Resultado.Hasta = reader.GetString(5);
                                Resultado.IdProveedor = reader.GetString(6);
                                Resultado.IdSoftware = reader.GetString(7);
                                Resultado.ClaveTecnica = reader.GetString(8);
                                Resultado.NombreSoftware = reader.GetString(9);
                                Resultado.Pin = reader.GetString(10);
                                Resultado.CodigoSeguridad = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                                Resultado.Estado = reader.GetInt32(12);
                                Resultado.Vigencia = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                                Resultado.TipoAmbiente = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                                Resultado.DV = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                            }
                        }
                        Con.CloseCon();
                    }
                }
            }
            catch (Exception ex)
            {
                Resultado = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Configuracion Dian ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Resultado;
        }

        public string ObtenerUrlDian(String nit)
        {
            string respuesta = "";
            Con = Connection.Instancia;
            String SqlSelect;

            try
            {

                Con.OpenCon();
                SqlSelect = "SELECT URLDian FROM Entrada where NIT = '" + nit + "'";

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            respuesta = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                        }
                    }
                }
                Con.CloseCon();
                return respuesta;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Url Dian",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return respuesta;
            }
        }

        public int ObtenerCantidadDocumentos(String tipoFactura, int year, string resolucion)
        {
            int respuesta = 0;
            Con = Connection.Instancia;
            String SqlSelect = "";

            try
            {
                Con.OpenCon();

                if (tipoFactura == "F")
                {
                    SqlSelect = " SELECT ((SELECT COUNT(*) FROM Mostrador " +
                                " Where StatusDian = '3' and YEAR(FDian) = " + year + " and resolucion = '" + resolucion + "') + " + 
                                " (SELECT COUNT(*) FROM Pedidos Where StatusDian = '3' and YEAR(FDian) = " + year + " and resolucion = '" + resolucion + "'));";
                }
                else if (tipoFactura == "D")
                {
                    SqlSelect = " SELECT COUNT(*) FROM NDebitoClientes " +
                                " Where StatusDian = '3' and YEAR(FDian) = " + year + " and resolucion = '" + resolucion + "';";
                }
                else
                {
                    SqlSelect = "SELECT ((SELECT COUNT(*) FROM NCreditoPos " +
                               " Where StatusDian = '3' and YEAR(FDian) = " + year + " and resolucion = '" + resolucion + "') + " +
                               "(SELECT COUNT(*) FROM NCredito Where StatusDian = '3' and YEAR(FDian) = " + year + " and resolucion = '" + resolucion + "')); ";
                }

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            respuesta = reader.GetInt32(0);
                        }
                    }
                }
                Con.CloseCon();

                return respuesta;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Cantidad Documentos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return respuesta;
            }
        }

        public string ObtenerNombreArchivo(String tipoFactura, string numeroFactura, string resolucion)
        {
            var respuesta = "";
            Con = Connection.Instancia;
            String SqlSelect = "";

            try
            {
                string tabla = ObtenerTablaActualizar(numeroFactura, tipoFactura, resolucion);

                string campo = "";

                if (tabla == "Mostrador" || tabla == "Pedidos") campo = "Facturanumero";
                else if (tabla == "NCredito") campo = "Idncredito";
                else if (tabla == "NDebitoClientes") campo = "Idncredito";
                else if (tabla == "NCreditoPos") campo = "IdncreditoPos";

                SqlSelect = " SELECT ArchivoDian FROM " + tabla +
                            " where " + campo + " = '" + numeroFactura + "' and resolucion = '" + resolucion + "';";

                Con.OpenCon();

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            respuesta = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        }
                    }
                }
                Con.CloseCon();

                return respuesta;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consultar nombre archivo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return respuesta;
            }
        }

        public string ObtenerTablaActualizar(string facturaNumero, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            string resultado = "";

            //try
            //{


            if (tipoFactura == "F")
            {
                Con.OpenCon();
                SqlSelect = "SELECT COUNT(*) FROM Mostrador where Facturanumero = '" + facturaNumero + "' and resolucion = '" + resolucion + "';";
                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) > 0) resultado = "Mostrador";
                            else resultado = "Pedidos";
                        }
                    }
                }
                Con.CloseCon();
            }

            if (tipoFactura == "C")
            {
                Con.OpenCon();
                SqlSelect = "SELECT COUNT(*) FROM NCreditoPos where Idncreditopos = '" + facturaNumero + "' and resolucion = '" + resolucion + "';";
                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) > 0) resultado = "NCreditoPos";
                            else resultado = "NCredito";
                        }
                    }
                }
                Con.CloseCon();
            }

            if (tipoFactura == "D")
            {
                resultado = "NDebitoClientes";
            }

            return resultado;
        }
    }
}