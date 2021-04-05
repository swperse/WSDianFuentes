using FacturacionElectronica.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionElectronica.Modelo;
using System.Text.RegularExpressions;

namespace FacturacionElectronica.DAO
{
    class FacturasDAO
    {
        Connection Con;
        SqlCommand Cmd;
        SqlDataAdapter DAdapter;

        public DataTable selectListFacturas()
        {
            DataTable DTableListMan;
            Con = Connection.Instancia;
            String SqlSelect;
            try
            {
                Con.OpenCon();
                SqlSelect = "  SELECT F.Resolucion 'Prefijo', F.Facturanumero 'Consecutivo',T.Nombre  + ' - ' + F.Cliente 'ClienteNombre' ,CAST(StaDian.Tipo AS varchar ) + ' : ' + StaDian.Nombre StatusDIAN ,CAST(StaClien.Tipo AS varchar ) + ' : ' + StaClien.Nombre StatusCliente " +
                            "    FROM Resoluciones R, Pedidos  F , Terceros T , Entrada E , TipoStatusDIAN StaDian, TipoStatusDIAN StaClien  " +
                            "   WHERE F.Estado = 3 AND F.Resolucion = R.Centro AND (R.Tipo = 'CONTINGENCIA' OR R.Tipo = 'ELECTRONICA') AND (F.StatusDian = 0 OR F.StatusCliente = 0)  AND F.Cliente = T.Identificacion  " +
                            "     AND StaClien.Tipo = F.StatusCliente and StaDian.Tipo = F.StatusDIAN " +
                            "   UNION  " +
                            "  SELECT F.Resolucion 'Prefijo', F.Facturanumero 'Consecutivo',T.Nombre  + ' - ' + F.Cliente 'ClienteNombre' ,CAST(StaDian.Tipo AS varchar ) + ' : ' + StaDian.Nombre StatusDIAN ,CAST(StaClien.Tipo AS varchar ) + ' : ' + StaClien.Nombre StatusCliente " +
                            "    FROM Resoluciones R, Mostrador F , Terceros T , Entrada E , TipoStatusDIAN StaDian, TipoStatusDIAN StaClien " +
                            "   WHERE F.Estado = 3 AND F.Resolucion = R.Centro AND (R.Tipo = 'CONTINGENCIA' OR R.Tipo = 'ELECTRONICA') AND (F.StatusDian = 0 OR F.StatusCliente = 0)  AND F.Cliente = T.Identificacion " +
                            "    AND StaClien.Tipo = F.StatusCliente and StaDian.Tipo = F.StatusDIAN " +
                            "   ORDER BY 2";

                DAdapter = new SqlDataAdapter(SqlSelect, Con.Con);
                DTableListMan = new DataTable();
                DAdapter.Fill(DTableListMan);
                Con.CloseCon();
                /*
                MessageBox.Show("Total: " + DTableListMan.Rows.Count, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);*/
            }
            catch (Exception ex)
            {
                DTableListMan = null;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado Facturas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DTableListMan;
        }

        public DataTable selectListNCredito()
        {
            DataTable DTableListMan;
            Con = Connection.Instancia;
            String SqlSelect;
            try
            {
                Con.OpenCon();
                SqlSelect = "   SELECT F.Resolucion 'Prefijo', F.Idncredito 'Consecutivo',F.Nombre + ' - ' + T.Nombre 'Tercero' , CAST(StaDian.Tipo AS varchar ) + ' : ' + StaDian.Nombre StatusDIAN ,CAST(StaClien.Tipo AS varchar ) + ' : ' + StaClien.Nombre StatusCliente " +
                            "     FROM Resoluciones R, NCredito  F , Terceros T , Entrada E , TipoStatusDIAN StaDian, TipoStatusDIAN StaClien " +
                            "    WHERE F.Estado = 3 AND F.Resolucion = R.Centro AND (R.Tipo = 'CONTINGENCIA' OR R.Tipo = 'ELECTRONICA') AND (F.StatusDian = 0 OR F.StatusCliente = 0)  AND F.Nombre = T.Identificacion  " +
                            "      AND StaClien.Tipo = F.StatusCliente and StaDian.Tipo = F.StatusDIAN  " +
                            "    UNION " +
                            "   SELECT F.Resolucion 'Prefijo', F.Idncreditopos 'Consecutivo',F.Nombre + ' - ' + T.Nombre 'Cliente' ,CAST(StaDian.Tipo AS varchar ) + ' : ' + StaDian.Nombre StatusDIAN ,CAST(StaClien.Tipo AS varchar ) + ' : ' + StaClien.Nombre StatusCliente " +
                            "     FROM Resoluciones R, NCreditoPos F , Terceros T , Entrada E , TipoStatusDIAN StaDian, TipoStatusDIAN StaClien " +
                            "    WHERE F.Estado = 3 AND F.Resolucion = R.Centro AND (R.Tipo = 'CONTINGENCIA' OR R.Tipo = 'ELECTRONICA') AND (F.StatusDian = 0 OR F.StatusCliente = 0)  AND F.Nombre = T.Identificacion " +
                            "      AND StaClien.Tipo = F.StatusCliente and StaDian.Tipo = F.StatusDIAN ;";

                DAdapter = new SqlDataAdapter(SqlSelect, Con.Con);
                DTableListMan = new DataTable();
                DAdapter.Fill(DTableListMan);
                Con.CloseCon();
                /*MessageBox.Show("Total: " + DTableListMan.Rows.Count, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);*/
            }
            catch (Exception ex)
            {
                DTableListMan = null;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DTableListMan;
        }

        public DataTable selectListNDebito()
        {
            DataTable DTableListMan;
            Con = Connection.Instancia;
            String SqlSelect;
            try
            {
                Con.OpenCon();
                SqlSelect = "  SELECT F.Resolucion 'Prefijo', F.Idncredito 'Consecutivo',F.Nombre + ' - ' + T.Nombre 'Cliente' ,CAST(StaDian.Tipo AS varchar ) + ' : ' + StaDian.Nombre StatusDIAN ,CAST(StaClien.Tipo AS varchar ) + ' : ' + StaClien.Nombre StatusCliente " +
                            "   FROM Resoluciones R, NDebitoClientes  F , Terceros T , Entrada E , TipoStatusDIAN StaDian, TipoStatusDIAN StaClien " +
                            "  WHERE F.Estado = 3 AND F.Resolucion = R.Centro AND (R.Tipo = 'CONTINGENCIA' OR R.Tipo = 'ELECTRONICA') AND (F.StatusDian = 0 OR F.StatusCliente = 0)  AND F.Nombre = T.Identificacion " +
                            "    AND StaClien.Tipo = F.StatusCliente and StaDian.Tipo = F.StatusDIAN;";
                DAdapter = new SqlDataAdapter(SqlSelect, Con.Con);
                DTableListMan = new DataTable();
                DAdapter.Fill(DTableListMan);
                Con.CloseCon();
                /*MessageBox.Show("Total: " + DTableListMan.Rows.Count, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);*/
            }
            catch (Exception ex)
            {
                DTableListMan = null;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DTableListMan;
        }

        public Factura selectManDetalles(String NroFactura, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            Factura Resultado = null;

            var tabla = ObtenerTablaActualizar(NroFactura, tipoFactura, resolucion);

            //try
            //{
            Con.OpenCon();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " SELECT F.Resolucion, F.Facturanumero,F.Estado,F.Fechafac,F.Tipo,F.Cliente, " +
                        "       ISNULL(F.Promotor,'NA') ,ISNULL(F.Observaciones,'NA'),F.Valortransporte,F.Formapago,F.Dias, " +
                        "       F.Vencimiento,F.Documento,F.Saldo,F.Medio,F.[Centro costos],F.Letras,F.PReteIca,F.Reteica, " +
                        "       F.PReteIva,F.ReteIva,F.PDescuento,T.Regimen,F.Descarga,F.Diferencia,F.ARetencion, " +
                        "       F.Anticipos,F.Tipotercero,F.Pcree,F.Retecree,F.[CUFE], F.Horafin, F.IdFACTURAS, " +
                        "       FP.Codigo FormaPago, F.Medio MedioPago, F.RespuestaDian, F.TipoFactura, F.TipoOperacion, F.TipoMoneda," +
                        "       F.TRM, ISNULL(F.Pedidos,'NA'), ISNULL(F.FechaOrden, F.Fechafac), F.RecajaNo, F.FechaRC " +  //, F.FacReferencia " +
                        "       FROM " + tabla + " F , Terceros T, Formapago FP " +
                        "       WHERE F.Cliente = T.Identificacion and F.Formapago = FP.Formapago and " +
                        "       F.Facturanumero = '" + NroFactura + "' and F.resolucion='" + resolucion + "'; ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Resultado = new Factura();
                        Resultado.Resolucion = reader.GetString(0);
                        Resultado.Facturanumero = reader.GetInt32(1).ToString();
                        Resultado.Estado = reader.GetInt32(2).ToString();
                        Resultado.Fechafac = reader.GetDateTime(3).ToString();
                        Resultado.Tipo = reader.GetString(4);
                        Resultado.Cliente = reader.GetString(5);
                        Resultado.Promotor = reader.GetString(6);
                        Resultado.Observaciones = reader.GetString(7);
                        Resultado.Valortransporte = reader.GetInt32(8).ToString();
                        Resultado.Formapago = reader.GetString(9);
                        Resultado.Dias = reader.GetDouble(10).ToString();
                        Resultado.Vencimiento = reader.GetSqlDateTime(11).ToString();
                        Resultado.CUFE = reader.GetString(30);
                        Resultado.Horafin = reader.GetDateTime(31).ToString();
                        Resultado.Id = reader.GetInt32(32).ToString();
                        Resultado.Formapago = !reader.IsDBNull(33) ? reader.GetString(33) : "";
                        Resultado.Medio = !reader.IsDBNull(34) ? reader.GetString(34) : "";
                        Resultado.RespuestaDian = !reader.IsDBNull(35) ? reader.GetString(35) : "";
                        Resultado.TipoFactura = !reader.IsDBNull(36) ? reader.GetString(36) : "";
                        Resultado.TipoOperacion = !reader.IsDBNull(37) ? reader.GetString(37) : "";
                        Resultado.TipoMoneda = "COP"; //Resultado.TipoMoneda = !reader.IsDBNull(38) ? reader.GetString(38) == "US" ? "USD" : "COP" : "COP";
                        Resultado.TRM = 1; //!reader.IsDBNull(39) ? reader.GetDouble(39) : 1;
                        Resultado.NumeroOrden = !reader.IsDBNull(40) ? reader.GetString(40) : "";
                        Resultado.FechaOrden = !reader.IsDBNull(41) ? reader.GetDateTime(41).ToString() : "";
                        Resultado.RefFacturaNumero = reader.GetInt32(1).ToString();
                        Resultado.RefFacturaResolucion = reader.GetString(0);
                        Resultado.RecajaNo = !reader.IsDBNull(42) ? reader.GetInt32(42).ToString() : "";
                        Resultado.FechaRC = !reader.IsDBNull(43) ? reader.GetDateTime(43).ToString() : "";


                        //if (!reader.IsDBNull(42) && reader.GetString(42) != "")
                        //{
                        //    char[] separator = { '-' };
                        //    String[] strlist = reader.GetString(42).Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        //    Resultado.RefFacturaNumero = strlist[1];
                        //    Resultado.RefFacturaResolucion = strlist[0];
                        //}
                        //else
                        //{
                        //    Resultado.RefFacturaNumero = "";
                        //    Resultado.RefFacturaResolucion = "";
                        //}
                    }
                }
            }
            Con.CloseCon();
            return Resultado;
        }

        public Factura selectManDetallesCredito(String NroFactura, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            Factura Resultado = null;

            var campo = "";
            var tabla = ObtenerTablaActualizar(NroFactura, tipoFactura, resolucion);

            if (tabla == "NCredito") campo = "Idncredito";
            else campo = "IdncreditoPos";

            //try
            //{
            Con.OpenCon();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " SELECT ISNULL(F.Resolucion,'0'), F.Facturanumero,0,F.Fecha,F.Tipo,F.Nombre, " +
                        "       'NA', 'NA', 0,'NA',0, " +
                        "       GETDATE(),'NA','NA','NA','NA','NA','NA','NA', " +
                        "       'NA','NA','NA',T.Regimen,'NA','NA','NA', " +
                        "       'NA','NA','NA',F.CUDE,'NA', F.Fproceso, F." + campo + ", F.Codconcepto Codconcepto, F.RespuestaDian, F.TipoFactura, F.TipoOperacion, " +
                        "       '', null " +
                        "       FROM " + tabla + " F , Terceros T " +
                        "       WHERE F.Nombre = T.Identificacion and F." + campo + " = '" + NroFactura + "' " +
                        "       and F.resolucion='" + resolucion + "'; ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Resultado = new Factura();
                        Resultado.Resolucion = reader.GetString(0);
                        Resultado.Facturanumero = reader.GetInt32(1).ToString();
                        Resultado.Estado = reader.GetInt32(2).ToString();
                        Resultado.Fechafac = reader.GetDateTime(3).ToString();
                        Resultado.Tipo = reader.GetString(4);
                        Resultado.Cliente = reader.GetString(5);
                        Resultado.Promotor = reader.GetString(6);
                        Resultado.Observaciones = reader.GetString(7);
                        Resultado.Valortransporte = reader.GetInt32(8).ToString();
                        Resultado.Formapago = !reader.IsDBNull(9) ? reader.GetString(9) : "NA";
                        Resultado.Dias = reader.GetInt32(10).ToString();
                        Resultado.Vencimiento = reader.GetSqlDateTime(11).ToString();
                        Resultado.CUDE = reader.GetString(29);
                        Resultado.Horafin = reader.GetDateTime(31).ToString();
                        Resultado.Id = reader.GetInt32(32).ToString();
                        Resultado.Concepto = !reader.IsDBNull(33) ? reader.GetString(33) : "";
                        Resultado.Medio = "NA";
                        Resultado.RespuestaDian = !reader.IsDBNull(34) ? reader.GetString(34) : "";
                        Resultado.TipoFactura = !reader.IsDBNull(35) ? reader.GetString(35) : "";
                        Resultado.TipoOperacion = !reader.IsDBNull(36) ? reader.GetString(36) : "";
                        Resultado.NumeroOrden = !reader.IsDBNull(37) ? reader.GetString(37) : "";
                        Resultado.FechaOrden = !reader.IsDBNull(38) ? reader.GetDateTime(38).ToString() : "";
                        Resultado.TipoMoneda = "COP";
                    }
                }
            }
            Con.CloseCon();
            return Resultado;
        }

        public Factura selectManDetallesDebito(String NroFactura, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            Factura Resultado = null;

            var campo = "";
            var tabla = ObtenerTablaActualizar(NroFactura, tipoFactura, resolucion);

            if (tabla == "NDebitoClientes") campo = "Idncredito";
            else campo = "IdncreditoPos";

            //try
            //{
            Con.OpenCon();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " SELECT ISNULL(F.Resolucion,'0'), F.Facturanumero,0,F.Fecha,F.Tipo,F.Nombre, " +
                        "       'NA', 'NA', 0,'NA',0, " +
                        "       GETDATE(),'NA','NA','NA','NA','NA','NA','NA', " +
                        "       'NA','NA','NA',T.Regimen,'NA','NA','NA', " +
                        "       'NA','NA','NAN',F.CUDE,'NANN', F.Fproceso, F.Idncredito, F.Codconcepto Codconcepto, " +
                        "       F.RespuestaDian, F.TipoFactura, F.TipoOperacion , null, null " +
                        "       FROM " + tabla + " F , Terceros T " +
                        "       WHERE F.Nombre = T.Identificacion and F." + campo + " = '" + NroFactura + "' " +
                        "       and F.resolucion='" + resolucion + "'; ";

            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Resultado = new Factura();
                        Resultado.Resolucion = reader.GetString(0);
                        Resultado.Facturanumero = reader.GetInt32(1).ToString();
                        Resultado.Estado = reader.GetInt32(2).ToString();
                        Resultado.Fechafac = reader.GetDateTime(3).ToString();
                        Resultado.Tipo = reader.GetString(4);
                        Resultado.Cliente = reader.GetString(5);
                        Resultado.Promotor = reader.GetString(6);
                        Resultado.Observaciones = reader.GetString(7);
                        Resultado.Valortransporte = reader.GetInt32(8).ToString();
                        Resultado.Formapago = !reader.IsDBNull(9) ? reader.GetString(9) : "NA";
                        Resultado.Dias = reader.GetInt32(10).ToString();
                        Resultado.Vencimiento = reader.GetDateTime(11).ToString();
                        Resultado.CUDE = reader.GetString(29);
                        Resultado.Horafin = reader.GetDateTime(31).ToString();
                        Resultado.Id = reader.GetInt32(32).ToString();
                        Resultado.Concepto = !reader.IsDBNull(33) ? reader.GetString(33) : "";
                        Resultado.Medio = "NA";
                        Resultado.RespuestaDian = !reader.IsDBNull(34) ? reader.GetString(34) : "";
                        Resultado.TipoFactura = !reader.IsDBNull(35) ? reader.GetString(35) : "";
                        Resultado.TipoOperacion = !reader.IsDBNull(36) ? reader.GetString(36) : "";
                        Resultado.NumeroOrden = !reader.IsDBNull(37) ? reader.GetString(37) : "";
                        Resultado.FechaOrden = !reader.IsDBNull(38) ? reader.GetDateTime(38).ToString() : "";
                        Resultado.TipoMoneda = "COP";
                    }
                }
            }
            Con.CloseCon();
            return Resultado;
        }

        public DataTable selectListNomina()
        {
            DataTable DTableListMan;
            Con = Connection.Instancia;
            String SqlSelect;
            try
            {
                Con.OpenCon();

                SqlSelect = "SELECT " +
                            "N.Prefijo, " +
                            "N.Consecutivo, " +
                            "N.Cedula AS Cédula, " +
                            "(E.Nombre1 + ' ' + E.Nombre2 + ' ' + E.Apellido1 + ' ' + E.Apellido2) AS [Nombres y Apellidos], " +
                            "CAST(N.StatusDian AS varchar ) + ': ' + StaDian.Nombre StatusDIAN " +
                            "FROM Nomina N, Empleado E, TipoStatusDIAN StaDian " +
                            "WHERE " +
                            "StaDian.Tipo = N.StatusDIAN AND " +
                            "N.Estado = 3 AND " +
                            "N.Cedula = E.Cedula;";


                DAdapter = new SqlDataAdapter(SqlSelect, Con.Con);
                DTableListMan = new DataTable();
                DAdapter.Fill(DTableListMan);
                Con.CloseCon();
            }
            catch (Exception ex)
            {
                DTableListMan = null;
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado Manifiestos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return DTableListMan;
        }

        public List<DetalleFactura> ListadoProductos(String NroFactura, string numDocumento, string tipoFactura, string resolucion, double TRM)
        {
            List<DetalleFactura> listado = new List<DetalleFactura>();
            DetalleFactura miProducto;
            Con = Connection.Instancia;
            String SqlSelect;
            if (tipoFactura == "F")
            {
                try
                {
                    //Traer el campo que se llama subgrupo
                    Con.OpenCon();
                    SqlSelect = "  SELECT  M.Facturanumero, R.Referencia,R.Nombreref,R.Precioventa,M.Cantidad, M.Descuento, " +
                                "   (M.Subtotal / M.Cantidad) PRECIO,M.PIva,M.[Precio total] TOTAL, M.Neto, M.Subtotal, 'NA' , 'NA', " +
                                "   R.Marca, M.PRete, M.VPropina, M.Pimpoconsumo, M.Idpedir, (M.Subtotal - M.Neto) TDESCUENTO, (R.Referencia) CODREF, M.Proveedor, " +
                                " (select CASE	WHEN M.Proveedor IS NOT NULL  THEN ts.DV  ELSE '' END from Terceros ts where ts.Identificacion = M.Proveedor), " +
                                " (select CASE	WHEN M.Proveedor IS NOT NULL  THEN ts.Clase  ELSE '' END from Terceros ts where ts.Identificacion = M.Proveedor), M.PReteIcasub, M.ReteIcasub, M.Costo " +
                                "    FROM MostradorSub M, Referencias R " +
                                "   WHERE M.Referencia = R.Referencia  " +
                                "     AND M.Facturanumero = '" + NroFactura + "' and M.resolucion='" + resolucion + "'" +
                                "  UNION " +
                                "  SELECT  M.Facturanumero, R.Referencia,R.Nombreref,R.Precioventa,M.Cantidad, M.Descuento, " +
                                "  (M.Subtotal / M.Cantidad) PRECIO,M.PIva,M.[Precio total] TOTAL, M.Neto, M.Subtotal, 'NA', 'NA', " +
                                "   R.Marca, M.PRete, M.VPropina, M.Pimpoconsumo, M.Idpedir, (M.Subtotal - M.Neto) TDESCUENTO, (R.Referencia) CODREF, M.Proveedor, " +
                                " (select CASE	WHEN M.Proveedor IS NOT NULL  THEN ts.DV  ELSE '' END from Terceros ts where ts.Identificacion = M.Proveedor), " +
                                " (select CASE	WHEN M.Proveedor IS NOT NULL  THEN ts.Clase  ELSE '' END from Terceros ts where ts.Identificacion = M.Proveedor), M.PReteIcasub, M.ReteIcasub, M.Costo " +
                                "    FROM Pedisub M, Referencias R " +
                                "   WHERE M.Referencia = R.Referencia  " +
                                "     AND M.Facturanumero = '" + NroFactura + "' and M.resolucion='" + resolucion + "';";
                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var index = 1;
                            while (reader.Read())
                            {
                                miProducto = new DetalleFactura();
                                miProducto.Facturanumero = reader.GetInt32(0);
                                miProducto.Referencia = index.ToString(); //reader.GetString(1);
                                miProducto.Nombreref = Regex.Replace(reader.GetString(2), @"[^0-9A-Za-z]", " ", RegexOptions.None);
                                miProducto.Precioventa = reader.GetDouble(3) / TRM;
                                miProducto.Cantidad = reader.GetDouble(4);
                                miProducto.Descuento = reader.GetDouble(5) / TRM;
                                miProducto.Precio = reader.GetDouble(6) / TRM;
                                miProducto.PIva = reader.GetDouble(7);
                                miProducto.CodigoIva = "01";
                                miProducto.Total = reader.GetDouble(8) / TRM;
                                miProducto.Neto = reader.GetInt32(9) / (int)TRM;
                                miProducto.Subtotal = reader.GetInt32(10) / (int)TRM;
                                miProducto.Modelo = reader.IsDBNull(11) ? "NA" : reader.GetString(11);
                                miProducto.Subgrupo = reader.IsDBNull(12) ? "NA" : reader.GetString(12);
                                miProducto.Marca  = reader.IsDBNull(13) ? "" : reader.GetString(13).ToLower();
                                miProducto.PRete = reader.IsDBNull(14) ? 0 : reader.GetDouble(14);
                                miProducto.CodigoRete = "06";
                                miProducto.VPropina = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                                miProducto.PIConsumo = reader.IsDBNull(16) ? 0 : reader.GetDouble(16);
                                miProducto.Idpedir = reader.GetInt32(17);
                                miProducto.TDescuento = reader.GetInt32(18);
                                miProducto.CODREF = reader.GetString(19);
                                miProducto.Proveedor = reader.IsDBNull(20) ? "" : reader.GetString(20);
                                miProducto.ProveedorDV = reader.IsDBNull(21) ? "" : reader.GetString(21);
                                miProducto.ProveedorClase = reader.IsDBNull(22) ? "" : reader.GetString(22);
                                miProducto.CodigoIConsumo = "04";
                                miProducto.PReteIcasub = reader.IsDBNull(23) ? 0 : reader.GetDouble(23);
                                miProducto.ReteIcasub = reader.IsDBNull(24) ? 0 : reader.GetDouble(24);
                                miProducto.CodigoReteIca = "07";
                                miProducto.Costo = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);

                                listado.Add(miProducto);
                                ++index;

                            }
                        }
                    }
                    Con.CloseCon();

                }
                catch (Exception ex)
                {
                    listado = null;
                    Con.CloseCon();
                    MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado de Productos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (tipoFactura == "D")
            {
                try
                {
                    Con.OpenCon();
                    SqlSelect = "  SELECT M.Idncredito, R.Referencia,R.Nombreref,0,M.Cantidad,M.Descuento,(M.Subtotal / M.Cantidad) PRECIO, " +
                                "  M.PIva,M.[Precio total] TOTAL, M.Neto, M.Subtotal, M.PRete, M.VPropina, " +
                                "  M.Pimpoconsumo, M.Idncresub " +
                                "    FROM NDebitoClientesub M, Referencias R " +
                                "   WHERE M.Referencia = R.Referencia  " +
                                "     AND M.Idncredito = '" + numDocumento + "'  and M.resolucion='" + resolucion + "';";

                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var index = 1;
                            while (reader.Read())
                            {
                                miProducto = new DetalleFactura();
                                miProducto.Facturanumero = reader.GetInt32(0);
                                miProducto.Referencia = index.ToString(); // reader.GetString(1);  
                                miProducto.Nombreref = reader.GetString(2);
                                miProducto.Precioventa = 0;
                                miProducto.Cantidad = reader.GetDouble(4);
                                miProducto.Descuento = reader.GetDouble(5);
                                miProducto.Precio = reader.GetDouble(6);
                                miProducto.PIva = reader.GetDouble(7);
                                miProducto.Total = reader.GetInt32(8);
                                miProducto.Neto = reader.GetInt32(9);
                                miProducto.Subtotal = reader.GetInt32(10);
                                miProducto.PRete = reader.IsDBNull(11) ? 0 : reader.GetDouble(11);
                                miProducto.CodigoRete = "06";
                                miProducto.CodigoIva = "01";
                                miProducto.VPropina = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                                miProducto.PIConsumo = reader.IsDBNull(13) ? 0 : reader.GetDouble(13);
                                miProducto.CodigoIConsumo = "04";
                                listado.Add(miProducto);
                                ++index;
                            }
                        }
                    }
                    Con.CloseCon();

                }
                catch (Exception ex)
                {
                    listado = null;
                    Con.CloseCon();
                    MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado de Productos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (tipoFactura == "C")
            {
                try
                {
                    Con.OpenCon();
                    SqlSelect = "  SELECT M.Idncreditopos, R.Referencia,'NA',0,M.Cantidad,M.Descuento, " +
                                "   (M.Subtotal / M.Cantidad) PRECIO, M.PIva, M.[Precio total] TOTAL, M.Neto, M.Subtotal, M.PRete, " +
                                "   M.VPropina, M.Pimpoconsumo, M.Idncresub " +
                                "    FROM NCreditosubPos M, Referencias R " +
                                "   WHERE M.Referencia = R.Referencia  " +
                                "     AND M.Idncreditopos = '" + numDocumento + "' and M.resolucion='" + resolucion + "'" +
                                "  UNION " +
                                "  SELECT M.Idncredito, R.Referencia,'NA',0,M.Cantidad,M.Descuento, " +
                                "   (M.Subtotal / M.Cantidad) PRECIO, M.PIva, M.[Precio total] TOTAL, M.Neto, M.Subtotal, M.PRete, " +
                                "   M.VPropina, M.Pimpoconsumo, M.Idncresub " +
                                "    FROM NCreditosub M, Referencias R " +
                                "   WHERE M.Referencia = R.Referencia  " +
                                "     AND M.Idncredito = '" + numDocumento + "' and M.resolucion='" + resolucion + "';";

                    using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            var index = 1;
                            while (reader.Read())
                            {
                                miProducto = new DetalleFactura();
                                miProducto.Facturanumero = reader.GetInt32(0);
                                miProducto.Referencia = index.ToString(); // reader.GetString(1);  
                                miProducto.Nombreref = reader.GetString(2);
                                miProducto.Precioventa = 0;
                                miProducto.Cantidad = reader.GetDouble(4);
                                miProducto.Descuento = reader.GetDouble(5);
                                miProducto.Precio = reader.GetDouble(6);
                                miProducto.PIva = reader.GetDouble(7);
                                miProducto.Total = reader.GetInt32(8);
                                miProducto.Neto = reader.GetInt32(9);
                                miProducto.Subtotal = reader.GetInt32(10);
                                miProducto.PRete = reader.IsDBNull(11) ? 0 : reader.GetDouble(11);
                                miProducto.CodigoRete = "06";
                                miProducto.CodigoIva = "01";
                                miProducto.VPropina = reader.IsDBNull(12) ? 0 : reader.GetInt32(12);
                                miProducto.PIConsumo = reader.IsDBNull(13) ? 0 : reader.GetDouble(13);
                                miProducto.CodigoIConsumo = "04";
                                listado.Add(miProducto);
                                ++index;
                            }
                        }
                    }
                    Con.CloseCon();

                }
                catch (Exception ex)
                {
                    listado = null;
                    Con.CloseCon();
                    MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Listado de Productos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return listado;
        }

        public Adquiriente InfoAdquiriente()
        {

            Adquiriente empresa = null;
            Con = Connection.Instancia;
            String SqlSelect;

            try
            {
                Con.OpenCon();
                SqlSelect = "SELECT E.[Nombre usuario] NombreUsuario, E.Nit , E.Direccion , C.Nombre Ciudad, T.Clase, " +
                            "P.PaisISO Paises, P.CodigoAlfa2 PaisCodAlfa, C.Depto Departamento, C.CodDpto CodDepto, C.Municipio CodCiudad, " +
                            "T.CodigoPostal codigoPostal, T.Web Email, T.Telefono Telefono, T.DV DV, T.Regimen Regimen, T.ResFiscal, T.Tipopersona " +
                            "  FROM Entrada E, Terceros T, Paises P, Codigos C " +
                            " WHERE E.NIT = T.Identificacion  AND P.Codigo = T.Pais AND T.CodigoMpio = C.Municipio ";
                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            empresa = new Adquiriente();
                            empresa.NombreUsuario = reader.GetString(0);
                            empresa.Nit = reader.GetString(1);
                            empresa.Direccion = reader.GetString(2);
                            empresa.Ciudad = reader.GetString(3);
                            empresa.Clase = reader.GetString(4);
                            empresa.Pais = reader.GetString(5);
                            empresa.PaisCodAlfa = reader.GetString(6);
                            empresa.Departamento = reader.GetString(7);
                            empresa.CodigoDepto = reader.GetString(8);
                            empresa.CodigoCiudad = reader.GetString(9);
                            empresa.CodigoPostal = reader.GetString(10);
                            empresa.Email = reader.IsDBNull(11) ? "" : reader.GetString(11);
                            empresa.Telefono = reader.IsDBNull(12) ? "" : reader.GetString(12);
                            empresa.DV = reader.IsDBNull(13) ? "" : reader.GetString(13);
                            empresa.Regimen = reader.IsDBNull(14) ? "" : reader.GetString(14);
                            empresa.ResFiscal = reader.IsDBNull(15) ? "" : reader.GetString(15);
                            empresa.Tipopersona = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        }
                    }
                }
                Con.CloseCon();

            }
            catch (Exception ex)
            {
                empresa = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Informacion Adquiriente",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return empresa;
        }

        public Tercero InfoTercero(String IdTercero)
        {

            Tercero cliente = null;
            Con = Connection.Instancia;
            String SqlSelect;

            try
            {
                Con.OpenCon();
                SqlSelect = "    SELECT T.Identificacion,LTRIM(RTRIM(T.Nombre)) Nombre,ISNULL(T.Nombre2,' ') NombreS,ISNULL(T.Apellido1,T.Nombre) Apellido, ISNULL(T.Direccion,'Sin DIreccion') Direccion, " +
                            "    	   P.PaisISO Paises, LEFT(T.CodigoMpio,2) Departamento, RIGHT(T.CodigoMpio,3) Municipio,C.Nombre Ciudad , T.Clase, " +
                            " P.CodigoAlfa2 PaisCodAlfa, C.Depto Departamento, C.Municipio CodCiudad, T.Web Email , T.Telefono Telefono, " +
                            " T.DV DV, T.Regimen Regimen, T.ResFiscal, T.Tipopersona " +
                            "     FROM Terceros T , Paises P, Codigos C " +
                            "     WHERE T.Identificacion = '" + IdTercero + "' " +
                            "       AND P.Codigo = T.Pais AND T.CodigoMpio = C.Municipio ";
                ;
                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cliente = new Tercero();
                            cliente.Identificacion = reader.GetString(0);
                            cliente.Nombre = reader.GetString(1);
                            cliente.NombreS = reader.GetString(2);
                            cliente.Apellido = reader.GetString(3);
                            cliente.Direccion = reader.GetString(4);
                            cliente.Pais = reader.GetString(5);
                            cliente.CodigoDepto = reader.GetString(6);
                            cliente.Municipio = reader.GetString(7);
                            cliente.Ciudad = reader.GetString(8);
                            cliente.Clase = reader.GetString(9);
                            cliente.Exterior = reader.GetString(9) == "42" || reader.GetString(9) == "50"
                               || reader.GetString(9) == "91" ? true : false;
                            cliente.PaisCodAlfa = reader.GetString(10);
                            cliente.Departamento = reader.GetString(11);
                            cliente.CodigoCiudad = reader.GetString(12);
                            cliente.Email = reader.IsDBNull(13) ? "" : reader.GetString(13);
                            cliente.Telefono = reader.IsDBNull(14) ? "" : reader.GetString(14);
                            cliente.DV = reader.IsDBNull(15) ? "" : reader.GetString(15);
                            cliente.Regimen = reader.IsDBNull(16) ? "" : reader.GetString(16);
                            cliente.ResFiscal = reader.IsDBNull(17) ? "" : reader.GetString(17);
                            cliente.Tipopersona = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        }
                    }
                }
                Con.CloseCon();

            }
            catch (Exception ex)
            {
                cliente = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Informacion Cliente",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cliente;
        }

        public List<FacturaImpuestos> selectImpuestos(String NroFactura, string tipoFactura, string resolucion, double TRM)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            List<FacturaImpuestos> Lista = null;
            FacturaImpuestos Resultado = null;
            /*
            try
            {*/
            Con.OpenCon();
            if (tipoFactura == "F")
            {
                SqlSelect = "  select Ms.facturanumero Factura , '01' Tipo_Impuesto ,Ms.PIva PImpuesto ,SUM(Ms.[Precio total]-Ms.[Neto]-(Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From Pedisub Ms  " +
                            "   Where Ms.PIva > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.PIva, Ms.resolucion " +
                            "  UNION " +
                            "   select Ms.facturanumero Factura , '04' Tipo_Impuesto ,Ms.Pimpoconsumo PImpuesto ,SUM((Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From Pedisub Ms  " +
                            "   Where Ms.Pimpoconsumo > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.Pimpoconsumo, Ms.resolucion " +
                            "  UNION " +
                            "  select Ms.facturanumero Factura , '01' Tipo_Impuesto ,Ms.PIva PImpuesto ,SUM(Ms.[Precio total]-Ms.[Neto]-(Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From Mostradorsub Ms  " +
                            "   Where Ms.PIva > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.PIva, Ms.resolucion " +
                            "  UNION " +
                            "   select Ms.facturanumero Factura , '04' Tipo_Impuesto ,Ms.Pimpoconsumo PImpuesto ,SUM((Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From Mostradorsub Ms  " +
                            "   Where Ms.Pimpoconsumo > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.Pimpoconsumo, Ms.resolucion; ";


                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = Math.Abs(reader.GetDouble(3));
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            if (tipoFactura == "D")
            {
                SqlSelect = "  select Ms.Idncredito Factura , '01' Tipo_Impuesto ,Ms.PIva PImpuesto ,SUM(Ms.[Precio total]-Ms.[Neto]-(Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NDebitoClientesub Ms  " +
                            "   Where Ms.PIva > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito ,Ms.PIva, Ms.resolucion " +
                            "  UNION " +
                            "   select Ms.Idncredito Factura , '04' Tipo_Impuesto ,Ms.Pimpoconsumo PImpuesto ,SUM((Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NDebitoClientesub Ms  " +
                            "   Where Ms.Pimpoconsumo > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito ,Ms.Pimpoconsumo, Ms.resolucion;";

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = Math.Abs(reader.GetDouble(3));
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            if (tipoFactura == "C")
            {
                SqlSelect = "  select Ms.Idncreditopos Factura , '01' Tipo_Impuesto ,Ms.PIva PImpuesto ,SUM(Ms.[Precio total]-Ms.[Neto]-(Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NCreditosubPos Ms  " +
                            "   Where Ms.PIva > 0 and Ms.Idncreditopos = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncreditopos ,Ms.PIva, Ms.resolucion " +
                            " UNION " +
                            "   select Ms.Idncreditopos Factura , '04' Tipo_Impuesto ,Ms.Pimpoconsumo PImpuesto ,SUM((Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NCreditosubPos Ms  " +
                            "   Where Ms.Pimpoconsumo > 0 and Ms.Idncreditopos = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncreditopos ,Ms.Pimpoconsumo, Ms.resolucion  " +
                            " UNION " +
                            "  select Ms.Idncredito Factura , '01' Tipo_Impuesto ,Ms.PIva PImpuesto ,SUM(Ms.[Precio total]-Ms.[Neto]-(Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NCreditosub Ms  " +
                            "   Where Ms.PIva > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito ,Ms.PIva, Ms.resolucion " +
                            " UNION " +
                            "   select Ms.Idncredito Factura , '04' Tipo_Impuesto ,Ms.Pimpoconsumo PImpuesto ,SUM((Ms.Neto * (Ms.Pimpoconsumo /100))) ValorImpuesto  " +
                            "    From NCreditosub Ms  " +
                            "   Where Ms.Pimpoconsumo > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito ,Ms.Pimpoconsumo, Ms.resolucion; ";

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = Math.Abs(reader.GetDouble(3));
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                Resultado = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Impuestos Factura",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return Lista;
        }

        public List<FacturaImpuestos> selectRetenciones(String NroFactura, string tipoFactura, string resolucion, double TRM)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            List<FacturaImpuestos> Lista = null;
            FacturaImpuestos Resultado = null;
            /*
            try
            {*/
            Con.OpenCon();
            if (tipoFactura == "F")
            {
                SqlSelect = "    select Ms.facturanumero Factura , '06' Tipo_Impuesto ,Ms.PRete PImpuesto ,SUM(Ms.Retencion) ValorImpuesto  " +
                            "    From Pedisub Ms  " +
                            "   Where Ms.PRete > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.PRete, Ms.resolucion " +
                            "  UNION " +
                            "     select Ms.facturanumero Factura , '05' Tipo_Impuesto ,Ms.PReteIva PImpuesto ,Ms.ReteIva ValorImpuesto  " +
                            "    From Pedidos Ms  " +
                            "   Where Ms.PReteIva > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "  UNION " +
                            "    select Ms.facturanumero Factura , '07' Tipo_Impuesto ,(Ms.PReteIcasub/10) PImpuesto ,Sum(Ms.ReteIcasub) ValorImpuesto  " +
                            "    From Pedisub Ms  " +
                            "   Where Ms.PReteIcasub > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero ,Ms.PReteIcasub, Ms.resolucion "+
                            "  UNION " +
                            "     select Ms.facturanumero Factura , '06' Tipo_Impuesto ,Ms.PRete PImpuesto ,Sum(Ms.Retencion) ValorImpuesto  " +
                            "    From Mostradorsub Ms  " +
                            "   Where Ms.PRete > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.facturanumero,Ms.PRete, Ms.resolucion " +
                            "  UNION " +
                            "     select Ms.facturanumero Factura , '05' Tipo_Impuesto , Ms.PReteIva PImpuesto ,Ms.ImpReteiva ValorImpuesto  " +
                            "    From Mostrador Ms  " +
                            "   Where Ms.PReteIva > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "  UNION " +
                            "    select Ms.facturanumero Factura , '07' Tipo_Impuesto ,(Ms.PReteIcasub/10) PImpuesto ,Sum(Ms.Reteicasub) ValorImpuesto  " +
                            "    From Mostradorsub Ms  " +
                            "   Where Ms.PReteIcasub > 0 and Ms.Facturanumero = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "'" +
                            "   group by Ms.facturanumero ,Ms.PReteIcasub, Ms.resolucion ;";


                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = Math.Abs(reader.GetDouble(3));
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            if (tipoFactura == "D")
            {
                SqlSelect = "     select Ms.Idncredito Factura , '06' Tipo_Impuesto ,Ms.PRete PImpuesto ,Sum(Ms.Retencion) ValorImpuesto  " +
                            "    From NDebitoClientesub Ms  " +
                            "   Where Ms.PRete > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito,Ms.PRete, Ms.resolucion " +
                            "  UNION " +
                            "     select Ms.Idncredito Factura , '05' Tipo_Impuesto ,Ms.PReteIva PImpuesto ,Ms.ReteIva ValorImpuesto  " +
                            "    From NDebitoClientes Ms  " +
                            "   Where Ms.PReteIva > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "  UNION " +
                            "    select Ms.Idncredito Factura , '07' Tipo_Impuesto ,(Ms.PReteIca/10) PImpuesto, Ms.Reteica ValorImpuesto " +
                            "    From NDebitoClientes Ms  " +
                            "   Where Ms.PReteIca > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "';";
                ;

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = reader.GetInt32(3);
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            if (tipoFactura == "C")
            {
                SqlSelect = "    select Ms.Idncreditopos Factura , '06' Tipo_Impuesto ,Ms.PRete PImpuesto ,SUM(Ms.Retencion) ValorImpuesto  " +
                            "    From NCreditosubPos Ms  " +
                            "   Where Ms.PRete > 0 and Ms.Idncreditopos = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncreditopos ,Ms.PRete, Ms.resolucion " +
                            "  UNION " +
                            "     select Ms.Idncreditopos Factura , '05' Tipo_Impuesto ,Ms.PReteIva PImpuesto ,Ms.ImpReteiva ValorImpuesto  " +
                            "    From NCreditoPos Ms  " +
                            "   Where Ms.PReteIva > 0 and Ms.Idncreditopos = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "  UNION " +
                            "    select Ms.Idncreditopos Factura , '07' Tipo_Impuesto ,(Ms.PReteIca/10) PImpuesto, Ms.ImpReteica ValorImpuesto  " +
                            "    From NCreditoPos Ms  " +
                            "   Where Ms.PReteIca > 0 and Ms.Idncreditopos = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "'" +
                            "  UNION " +
                            "   select Ms.Idncredito Factura , '06' Tipo_Impuesto ,Ms.PRete PImpuesto ,SUM(Ms.Retencion) ValorImpuesto  " +
                            "    From NCreditosub Ms  " +
                            "   Where Ms.PRete > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "   group by Ms.Idncredito ,Ms.PRete, Ms.resolucion " +
                            "  UNION " +
                            "     select Ms.Idncredito Factura , '05' Tipo_Impuesto , Ms.PReteIva PImpuesto ,Ms.ReteIva ValorImpuesto  " +
                            "    From NCredito Ms  " +
                            "   Where Ms.PReteIva > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "' " +
                            "  UNION " +
                            "    select Ms.Idncredito Factura , '07' Tipo_Impuesto ,(Ms.PReteIca/10) PImpuesto, Ms.Reteica ValorImpuesto  " +
                            "    From NCredito Ms  " +
                            "   Where Ms.PReteIca > 0 and Ms.Idncredito = '" + NroFactura + "' and Ms.resolucion='" + resolucion + "';";

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<FacturaImpuestos>();
                        while (reader.Read())
                        {
                            Resultado = new FacturaImpuestos();
                            Resultado.FacturaNumero = reader.GetInt32(0);
                            Resultado.TipoImpuesto = reader.GetString(1);
                            Resultado.PImpuesto = Math.Abs(reader.GetDouble(2));
                            Resultado.ValorImpuesto = reader.GetInt32(3);
                            Lista.Add(Resultado);
                        }
                    }
                }
            }

            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                Resultado = null;
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Impuestos Factura",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return Lista;
        }

        public List<double> selectTotales(String NroFactura, string idFacturaNota, string tipoFactura, string resolucion, double TRM)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            List<double> Lista = null;
            /*
            try
            {*/
            Con.OpenCon();

            if (tipoFactura == "F")
            {
                SqlSelect = "select M.facturanumero, (select SUM ([Neto]) from Mostradorsub Ms where Ms.Facturanumero = M.Facturanumero AND MS.resolucion=M.resolucion) SubTotal, " +
                            " 		(Select SUM( Ms.[Precio total]-Ms.[Neto]) from Mostradorsub Ms where Ms.Facturanumero = M.Facturanumero AND MS.resolucion = M.resolucion) Impuestos, " +
                            " 	    ((select SUM( Ms.[Precio total]) - Sum(Ms.Retencion) from Mostradorsub Ms where Ms.Facturanumero = M.Facturanumero AND MS.resolucion = M.resolucion) - " +
                            " 	    ImpReteiva - ImpReteica ) Total, M.anticipos " +
                            "   from Mostrador M where M.Facturanumero = '" + NroFactura + "'  and M.resolucion='" + resolucion + "' " +
                            " UNION " +
                            " select M.facturanumero, (select SUM ([Neto]) from Pedisub Ms where Ms.Facturanumero = M.Facturanumero AND MS.resolucion = M.resolucion) SubTotal,  " +
                            " 		(Select SUM( Ms.[Precio total]-Ms.[Neto]) from Pedisub Ms where Ms.Facturanumero = M.Facturanumero AND M.resolucion = MS.resolucion) Impuestos,  " +
                            " 	    ((select SUM( Ms.[Precio total]) - Sum(Ms.Retencion) from Pedisub Ms where Ms.Facturanumero = M.Facturanumero AND MS.resolucion = M.resolucion) - " +
                            " 	    Reteiva - Reteica ) Total, M.anticipos " +
                            "   from Pedidos M where M.Facturanumero =  '" + NroFactura + "' and M.resolucion='" + resolucion + "';";
                ;

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<double>();
                        while (reader.Read())
                        {
                            Lista.Add(Convert.ToDouble(reader.GetInt32(0)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(1)));
                            Lista.Add(Convert.ToDouble(reader.GetDouble(2)));
                            Lista.Add(Convert.ToDouble(reader.GetDouble(3)));
                            Lista.Add(Convert.ToDouble(reader.GetDouble(4)));
                        }
                    }
                }
            }

            if (tipoFactura == "D")
            {
                SqlSelect = " select M.Idncredito, (select SUM ([Neto]) from NDebitoClientesub Ms where Ms.Idncredito = M.Idncredito) SubTotal, " +
                            " 		(Select SUM( Ms.[Precio total]-Ms.[Neto]) from NDebitoClientesub Ms where Ms.Idncredito = M.Idncredito) Impuestos, " +
                            " 	    ((select SUM( Ms.[Precio total]) - Sum(Ms.Retencion) from NDebitoClientesub Ms where Ms.Idncredito = M.Idncredito)- " +
                            " 	    Reteiva - Reteica ) Total, 0 " +
                            "   from NDebitoClientes M where M.Idncredito = '" + NroFactura + "' and M.resolucion='" + resolucion + "';";
                ;

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<double>();
                        while (reader.Read())
                        {
                            Lista.Add(Convert.ToDouble(reader.GetInt32(0)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(1)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(2)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(3)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(4)));
                        }
                    }
                }
            }

            if (tipoFactura == "C")
            {
                SqlSelect = " select M.Idncreditopos, (select SUM ([Neto]) from NCreditosubPos Ms where Ms.Idncreditopos = M.Idncreditopos) SubTotal, " +
                            " 		(Select SUM( Ms.[Precio total]-Ms.[Neto]) from NCreditosubPos Ms where Ms.Idncreditopos = M.Idncreditopos) Impuestos, " +
                            " 	    ((select SUM( Ms.[Precio total]) from NCreditosubPos Ms where Ms.Idncreditopos = M.Idncreditopos) ) Total, 0 " +
                            "   from NCreditoPos M where M.Idncreditopos = '" + NroFactura + "' and M.resolucion='" + resolucion + "' " +
                            " UNION " +
                            " select M.Idncredito, (select SUM ([Neto]) from NCreditosub Ms where Ms.Idncredito = M.Idncredito) SubTotal, " +
                            " 		(Select SUM( Ms.[Precio total]-Ms.[Neto]) from NCreditosub Ms where Ms.Idncredito = M.Idncredito) Impuestos, " +
                            " 	    ((select SUM( Ms.[Precio total]) from NCreditosub Ms where Ms.Idncredito = M.Idncredito)) Total, 0 " +
                            "   from NCredito M where M.Idncredito = '" + NroFactura + "' and M.resolucion='" + resolucion + "';";
                ;

                using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Lista = new List<double>();
                        while (reader.Read())
                        {
                            Lista.Add(Convert.ToDouble(reader.GetInt32(0)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(1)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(2)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(3)));
                            Lista.Add(Convert.ToDouble(reader.GetInt32(4)));
                        }
                    }
                }
            }

            Con.CloseCon();
            return Lista;
        }

        public String getUrl()
        {
            Con = Connection.Instancia;
            String SqlSelect;
            String Url = "";

            //try
            //{
            Con.OpenCon();
            // Saldo no es el valor de la factura, debemos coger la sumatoria del sub como valor neto.
            SqlSelect = " select UrlDian from Entrada ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Url = reader.GetString(0);
                    }
                }
            }
            Con.CloseCon();
            /*
            }
            catch (Exception ex)
            {
                Url = "";
                Con.CloseCon();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Consulta Manifiestos Detalles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            return Url;
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
                SqlSelect = "SELECT COUNT(*) FROM Mostrador where Facturanumero = '" + facturaNumero + "' and resolucion='" + resolucion + "'; ";
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
                SqlSelect = "SELECT COUNT(*) FROM NCreditoPos where Idncreditopos = '" + facturaNumero + "' and resolucion='" + resolucion + "'; ";
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

        public bool ActualizarEstadoFactura(string idFactura, string tipoFactura, string fileName, string respuestaDian, int status, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            bool resultado = false;
            string tabla = ObtenerTablaActualizar(idFactura, tipoFactura, resolucion);
            string campo = "";

            if (tabla == "Mostrador" || tabla == "Pedidos") campo = "Facturanumero";
            else if (tabla == "NCredito") campo = "Idncredito";
            else if (tabla == "NDebitoClientes") campo = "Idncredito";
            else if (tabla == "NCreditoPos") campo = "IdncreditoPos";

            string mensajeError = "No fué posible actualizar el registro " + idFactura + " en la tabla " + tabla + ". ";
            respuestaDian = respuestaDian.Replace(":", "$_$").Replace(" - ", "#_#").Replace("-", "_");
            //try
            //{
            Con.OpenCon();
            // Actualizar estado del registro en mostrador, con estadoDian 3 = Enviada
            SqlSelect = "UPDATE " + tabla + " set StatusDian = " + status + ", FDian = GETDATE(), RespuestaDian ='" + respuestaDian + "'" + (fileName != "" ? ", ArchivoDian='" + fileName + "'" : "") + " where " + campo + " = '" + idFactura + "' and resolucion='" + resolucion + "'; ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                try
                {
                    command.ExecuteReader();
                    resultado = true;
                }
                catch (Exception ex)
                {
                    resultado = false;
                    mensajeError += "Mensaje: " + ex.Message + ". " + ex.InnerException;
                }
            }
            Con.CloseCon();

            if (!resultado)
            {
                MessageBox.Show("Ocurrio un error al actualizar estado del registro: " + mensajeError, "Actualizando estado",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultado;
        }

        public bool ActualizarEstadoFacturaCliente(string idFactura, string tipoFactura, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            bool resultado = true;
            string tabla = ObtenerTablaActualizar(idFactura, tipoFactura, resolucion);
            string campo = "";

            if (tabla == "Mostrador" || tabla == "Pedidos") campo = "Facturanumero";
            else if (tabla == "NCredito" || tabla == "NDebitoClientes") campo = "Idncredito";
            else if (tabla == "NCreditoPos") campo = "IdncreditoPos";


            //try
            //{
            Con.OpenCon();
            // Actualizar estado del registro en mostrador, con estadoDian 3 = Enviada
            SqlSelect = "UPDATE " + tabla + " set StatusCliente = 3, FCliente = GETDATE() where " + campo + " = '" + idFactura + "' and resolucion='" + resolucion + "'; ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                try
                {
                    command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            Con.CloseCon();

            return resultado;
        }

        public bool ActualizarRespuestaSetPruebas(string idFactura, string tipoFactura, string mensaje, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            bool resultado = true;
            string tabla = ObtenerTablaActualizar(idFactura, tipoFactura, resolucion);
            string campo = "";

            if (tabla == "Mostrador" || tabla == "Pedidos") campo = "Facturanumero";
            else if (tabla == "NCredito" || tabla == "NDebitoClientes") campo = "Idncredito";
            else if (tabla == "NCreditoPos") campo = "IdncreditoPos";


            //try
            //{
            Con.OpenCon();
            // Actualizar estado del registro en mostrador, con estadoDian 3 = Enviada
            SqlSelect = "UPDATE " + tabla + " set StatusDian = 3, FDian = GETDATE(), RespuestaDianSetPruebas ='" + mensaje + "' where " + campo + " = '" + idFactura + "' and resolucion='" + resolucion + "'; ";
            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                try
                {
                    command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            Con.CloseCon();

            return resultado;
        }

        public bool ActualizarCUFEFactura(string idFactura, string tipoFactura, string cufe, string resolucion)
        {
            Con = Connection.Instancia;
            String SqlSelect;
            bool resultado = true;
            string tabla = ObtenerTablaActualizar(idFactura, tipoFactura, resolucion);
            string campo = "";

            if (tabla == "Mostrador" || tabla == "Pedidos") campo = "Facturanumero";
            else if (tabla == "NCredito" || tabla == "NDebitoClientes") campo = "Idncredito";
            else if (tabla == "NCreditoPos") campo = "IdncreditoPos";


            //try
            //{
            Con.OpenCon();
            // Actualizar estado del registro en mostrador, con estadoDian 3 = Enviada
            SqlSelect = "UPDATE " + tabla + " set CUFE = '" + cufe + "' where " + campo + " = '" + idFactura + "' and resolucion='" + resolucion + "'; ";

            using (SqlCommand command = new SqlCommand(SqlSelect, Con.Con))
            {
                try
                {
                    command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    resultado = false;
                }
            }
            Con.CloseCon();

            return resultado;
        }
    }
}
