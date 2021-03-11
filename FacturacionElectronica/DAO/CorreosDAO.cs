using FacturacionElectronica.Conexion;
using FacturacionElectronica.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.DAO
{
    class CorreosDAO
    {
        Connection Con;
        SqlCommand Cmd;
        public Correos getConfiguracion(String Usuario)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            // String Url = "";
            Correos miCorreo;
            //try
            //{
            Con.OpenCon();
            miCorreo = new Correos();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " select Usuario,Correo, Password,Nombre,Cargo1,Movil1,Fijo1,Dominio,Servidor,Puerto,Abs,Copia " +
                        "   from Correos, CorreosDominio " +
                        "  where Substring (Correos.Correo, CHARINDEX('@', Correos.Correo), LEN(Correos.Correo)) = CorreosDominio.Dominio " +
                        "    and Usuario = '" + Usuario + "';";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        miCorreo.Usuario = reader.GetString(0);
                        miCorreo.CorreoEmail = reader.GetString(1);
                        miCorreo.Password = reader.GetString(2);
                        miCorreo.Nombre = reader.GetString(3);
                        miCorreo.Cargo = reader.GetString(4);
                        miCorreo.Movil = reader.GetString(5);
                        miCorreo.Fijo = reader.GetString(6);
                        miCorreo.Dominio = reader.GetString(7);
                        miCorreo.Servidor = reader.GetString(8);
                        miCorreo.Puerto = reader.GetString(9);
                        miCorreo.Abs = reader.GetString(10);
                        miCorreo.Copia = reader.GetString(11);
                    }
                }
            }
            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                miCorreo = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Manifiestos Detalles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return miCorreo;
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
                SqlSelect = "SELECT COUNT(*) FROM Mostrador where Facturanumero = '" + facturaNumero + "' and resolucion = '" + resolucion+ "';";
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

        public List<String> getDestino(String NroFactura, string tipoDocumento, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            List<String> CongiguracionDestino = null;

            string tabla = ObtenerTablaActualizar(NroFactura, tipoDocumento, resolucion);
            string campo = "";
            string campoCliente = "";

            if (tabla == "Mostrador" || tabla == "Pedidos")
            {
                campo = "Facturanumero";
                campoCliente = "Cliente";
            }
            else if (tabla == "NCredito" || tabla == "NDebitoClientes")
            {
                campo = "Idncredito";
                campoCliente = "Nombre";
            }
            else if (tabla == "NCreditoPos")
            {
                campo = "IdncreditoPos";
                campoCliente = "Nombre";
            }

            //try
            //{
            Con.OpenCon();
            CongiguracionDestino = new List<String>();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = "  SELECT T.Nombre , T.Web " +
                        "    FROM " + tabla + "  F , Terceros T " +
                        "   WHERE F." + campoCliente + " = T.Identificacion " +
                        "     AND F." + campo + " = '" + NroFactura + "' " + 
                        "     AND F.resolucion = '" + resolucion + "' ";

            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Web"].ToString() == "")
                        {
                            Con.CloseCon();
                            throw new Exception("El cliente no tiene un correo configurado.");
                        }
                        else
                        {
                            CongiguracionDestino.Add(reader["Nombre"].ToString());
                            CongiguracionDestino.Add(reader["Web"].ToString());
                        }
                    }
                }
            }
            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                miCorreo = null;
                Con.CloseCon();
                CongiguracionDestino = null;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Manifiestos Detalles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return CongiguracionDestino;
        }
    }
}
