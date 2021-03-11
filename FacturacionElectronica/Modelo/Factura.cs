using FacturacionElectronica.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionElectronica.Modelo
{
    class Factura
    {
        string _Resolucion;
        string _Facturanumero;
        string _Estado;
        string _Fechafac;
        string _Tipo;
        string _Cliente;
        string _Promotor;
        string _Observaciones;
        string _Valortransporte;
        string _Formapago;
        string _Dias;
        string _Vencimiento;
        string _Documento;
        string _Saldo;
        string _Medio;
        string _CentroCostos;
        string _Letras;
        string _PReteIca;
        string _Reteica;
        string _PReteIva;
        string _ReteIva;
        string _PDescuento;
        string _TipoRetencion;
        string _Descarga;
        string _Diferencia;
        string _ARetencion;
        string _Anticipos;
        string _Tipotercero;
        string _Pcree;
        string _Retecree;
        string _CUFE;
        string _Horafin;
        string _Id;
        string _Concepto;
        string _RespuestaDian;
        string _TipoOperacion;
        string _TipoFactura;
        double _TRM;
        string _TipoMoneda;
        string _NumeroOrden;
        string _FechaOrden;
        string _RefFacturaNumero;
        string _RefFacturaResolucion;
        Factura _RefFactura;
        string _RecajaNo;
        string _FechaRC;
        string _CUDE;


        public string Resolucion
        {
            get
            {
                return _Resolucion;
            }

            set
            {
                _Resolucion = value;
            }
        }

        public string Facturanumero
        {
            get
            {
                return _Facturanumero;
            }

            set
            {
                _Facturanumero = value;
            }
        }

        public string Estado
        {
            get
            {
                return _Estado;
            }

            set
            {
                _Estado = value;
            }
        }

        public string Fechafac
        {
            get
            {
                return _Fechafac;
            }

            set
            {
                _Fechafac = value;
            }
        }

        public string Tipo
        {
            get
            {
                return _Tipo;
            }

            set
            {
                _Tipo = value;
            }
        }

        public string Cliente
        {
            get
            {
                return _Cliente;
            }

            set
            {
                _Cliente = value;
            }
        }

        public string Promotor
        {
            get
            {
                return _Promotor;
            }

            set
            {
                _Promotor = value;
            }
        }

        public string Observaciones
        {
            get
            {
                return _Observaciones;
            }

            set
            {
                _Observaciones = value;
            }
        }

        public string Valortransporte
        {
            get
            {
                return _Valortransporte;
            }

            set
            {
                _Valortransporte = value;
            }
        }

        public string Formapago
        {
            get
            {
                return _Formapago;
            }

            set
            {
                _Formapago = value;
            }
        }

        public string Dias
        {
            get
            {
                return _Dias;
            }

            set
            {
                _Dias = value;
            }
        }

        public string Vencimiento
        {
            get
            {
                return _Vencimiento;
            }

            set
            {
                _Vencimiento = value;
            }
        }

        public string Documento
        {
            get
            {
                return _Documento;
            }

            set
            {
                _Documento = value;
            }
        }

        public string Saldo
        {
            get
            {
                return _Saldo;
            }

            set
            {
                _Saldo = value;
            }
        }

        public string Medio
        {
            get
            {
                return _Medio;
            }

            set
            {
                _Medio = value;
            }
        }

        public string CentroCostos
        {
            get
            {
                return _CentroCostos;
            }

            set
            {
                _CentroCostos = value;
            }
        }

        public string Letras
        {
            get
            {
                return _Letras;
            }

            set
            {
                _Letras = value;
            }
        }

        public string PReteIca
        {
            get
            {
                return _PReteIca;
            }

            set
            {
                _PReteIca = value;
            }
        }

        public string Reteica
        {
            get
            {
                return _Reteica;
            }

            set
            {
                _Reteica = value;
            }
        }

        public string PReteIva
        {
            get
            {
                return _PReteIva;
            }

            set
            {
                _PReteIva = value;
            }
        }

        public string ReteIva
        {
            get
            {
                return _ReteIva;
            }

            set
            {
                _ReteIva = value;
            }
        }

        public string PDescuento
        {
            get
            {
                return _PDescuento;
            }

            set
            {
                _PDescuento = value;
            }
        }

        public string TipoRetencion
        {
            get
            {
                return _TipoRetencion;
            }

            set
            {
                _TipoRetencion = value;
            }
        }

        public string Descarga
        {
            get
            {
                return _Descarga;
            }

            set
            {
                _Descarga = value;
            }
        }

        public string Diferencia
        {
            get
            {
                return _Diferencia;
            }

            set
            {
                _Diferencia = value;
            }
        }

        public string ARetencion
        {
            get
            {
                return _ARetencion;
            }

            set
            {
                _ARetencion = value;
            }
        }

        public string Anticipos
        {
            get
            {
                return _Anticipos;
            }

            set
            {
                _Anticipos = value;
            }
        }

        public string Tipotercero
        {
            get
            {
                return _Tipotercero;
            }

            set
            {
                _Tipotercero = value;
            }
        }

        public string Pcree
        {
            get
            {
                return _Pcree;
            }

            set
            {
                _Pcree = value;
            }
        }

        public string Retecree
        {
            get
            {
                return _Retecree;
            }

            set
            {
                _Retecree = value;
            }
        }

        public string CUFE
        {
            get
            {
                return _CUFE;
            }

            set
            {
                _CUFE = value;
            }
        }

        public string Horafin
        {
            get
            {
                return _Horafin;
            }

            set
            {
                _Horafin = value;
            }
        }

        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }


        public string Concepto
        {
            get
            {
                return _Concepto;
            }

            set
            {
                _Concepto = value;
            }
        }

        public string RespuestaDian
        {
            get
            {
                return _RespuestaDian;
            }

            set
            {
                _RespuestaDian = value;
            }
        }

        public string TipoOperacion
        {
            get
            {
                return _TipoOperacion;
            }

            set
            {
                _TipoOperacion = value;
            }
        }

        public string TipoFactura
        {
            get
            {
                return _TipoFactura;
            }

            set
            {
                _TipoFactura = value;
            }
        }

        public double TRM
        {
            get
            {
                return _TRM;
            }

            set
            {
                _TRM = value;
            }
        }
        
        public string TipoMoneda
        {
            get
            {
                return _TipoMoneda;
            }

            set
            {
                _TipoMoneda = value;
            }
        }


        public string NumeroOrden
        {
            get
            {
                return _NumeroOrden;
            }

            set
            {
                _NumeroOrden = value;
            }
        }

        public string FechaOrden
        {
            get
            {
                return _FechaOrden;
            }

            set
            {
                _FechaOrden = value;
            }
        }

        public Factura RefFactura
        {
            get
            {
                return _RefFactura;
            }

            set
            {
                _RefFactura = value;
            }
        }

        public string RefFacturaNumero
        {
            get
            {
                return _RefFacturaNumero;
            }

            set
            {
                _RefFacturaNumero = value;
            }
        }

        public string RefFacturaResolucion
        {
            get
            {
                return _RefFacturaResolucion;
            }

            set
            {
                _RefFacturaResolucion = value;
            }
        }

        public string RecajaNo
        {
            get
            {
                return _RecajaNo;
            }

            set
            {
                _RecajaNo = value;
            }
        }

        public string FechaRC
        {
            get
            {
                return _FechaRC;
            }

            set
            {
                _FechaRC = value;
            }
        }

        public string CUDE
        {
            get
            {
                return _CUDE;
            }

            set
            {
                _CUDE = value;
            }
        }


        public String toSha1Hexa(String Cadena)
        {
            String Final = "";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(Cadena);
            SHA1 sha = new SHA1CryptoServiceProvider();
            foreach (byte res in sha.ComputeHash(data))
            {
                Final = Final + res.ToString();
            }
            return Final;
        }

        public String toXML(string tipoDocumento, int countFacturas, string tipoAmbiente, string resolucion)
        {
            DateTime myDate = DateTime.Parse(this.Horafin);
            String Fecha = myDate.ToString("yyyy-MM-dd");
            String Hora = myDate.ToString("HH:mm:ss");

            // String estructura = "";
            StringBuilder estructura = new StringBuilder();

            if (tipoDocumento == "F")
            {
                estructura.AppendLine("<cbc:UBLVersionID>UBL 2.1</cbc:UBLVersionID>");
                estructura.AppendLine("<cbc:CustomizationID>" + (this.TipoOperacion == "" ? "10" : this.TipoOperacion) + "</cbc:CustomizationID>");//Validar luego para los demás tipo de facturas
                estructura.AppendLine("<cbc:ProfileID>DIAN 2.1</cbc:ProfileID>");
                estructura.AppendLine("<cbc:ProfileExecutionID>" + tipoAmbiente + "</cbc:ProfileExecutionID>");
                estructura.AppendLine("<cbc:ID>" + this.Resolucion + this.Facturanumero + "</cbc:ID>");
                // No coger los porcentages.
                if (this.TipoFactura == "03" || this.TipoFactura == "04")
                {
                    estructura.AppendLine("<cbc:UUID schemeID='" + tipoAmbiente + "' schemeName='CUDE-SHA384'>" + this.CUFE + "</cbc:UUID>");
                }
                else
                {
                    estructura.AppendLine("<cbc:UUID schemeID='" + tipoAmbiente + "' schemeName='CUFE-SHA384'>" + this.CUFE + "</cbc:UUID>");
                }
                estructura.AppendLine("<cbc:IssueDate>" + Convert.ToDateTime(this.Fechafac).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");          //  + Environment.NewLine +
                estructura.AppendLine("<cbc:IssueTime>" + Hora + "-05:00" + "</cbc:IssueTime>");          //  + Environment.NewLine +
                estructura.AppendLine("<cbc:InvoiceTypeCode listAgencyID='195' listAgencyName='CO, DIAN (Direccion de Impuestos y Aduanas Nacionales)' listSchemeURI='http://www.dian.gov.co/contratos/facturaelectronica/v1/InvoiceType'>" + (this.TipoFactura == "" ? "01" : this.TipoFactura) + "</cbc:InvoiceTypeCode>");    // + Environment.NewLine +
                estructura.AppendLine("<cbc:Note>" + this.Observaciones.Replace("&", "&amp;") + "</cbc:Note>");
                estructura.AppendLine("<cbc:DocumentCurrencyCode listAgencyID='6' listAgencyName='United Nations Economic Commission for Europe' listID='ISO 4217 Alpha'>" + this.TipoMoneda + "</cbc:DocumentCurrencyCode>");//Revisar si es COP en todos los casos
                estructura.AppendLine("<cbc:LineCountNumeric>" + countFacturas + "</cbc:LineCountNumeric>");

                if (this.NumeroOrden != string.Empty && this.FechaOrden != string.Empty)
                {
                    estructura.AppendLine("<cac:OrderReference>");
                    estructura.AppendLine("     <cbc:ID>" + this.NumeroOrden + "</cbc:ID>");
                    estructura.AppendLine("     <cbc:IssueDate>" + Convert.ToDateTime(this.FechaOrden).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");
                    estructura.AppendLine("</cac:OrderReference>");
                }

                if (this.RefFacturaNumero != "" && this.RefFacturaResolucion != "" &&
                    (this.TipoFactura == "03" || this.TipoFactura == "04"))
                {
                    estructura.AppendLine("<cac:AdditionalDocumentReference>");
                    estructura.AppendLine("     <cbc:ID>" + this.RefFacturaResolucion + this.RefFacturaNumero + "</cbc:ID>");
                    estructura.AppendLine("     <cbc:IssueDate>" + Convert.ToDateTime(this.Fechafac).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");
                    estructura.AppendLine("     <cbc:DocumentTypeCode>" + "FTC" + "</cbc:DocumentTypeCode>");
                    estructura.AppendLine("</cac:AdditionalDocumentReference>");
                }
            }

            if (tipoDocumento == "D" || tipoDocumento == "C")
            {
                var facDAO = new FacturasDAO();
                var miFactura = new Factura();
                miFactura = facDAO.selectManDetalles(this.Facturanumero, "F", resolucion);

                estructura.AppendLine("<cbc:UBLVersionID>UBL 2.1</cbc:UBLVersionID>");
                estructura.AppendLine("<cbc:CustomizationID>" + this.TipoOperacion + "</cbc:CustomizationID>");
                estructura.AppendLine("<cbc:ProfileID>DIAN 2.1</cbc:ProfileID>");
                estructura.AppendLine("<cbc:ProfileExecutionID>" + tipoAmbiente + "</cbc:ProfileExecutionID>");
                estructura.AppendLine("<cbc:ID>" + this.Resolucion + this.Id + "</cbc:ID>");
                estructura.AppendLine("<cbc:UUID schemeID='" + tipoAmbiente + "' schemeName='CUDE-SHA384'>" + this.CUDE + "</cbc:UUID>");
                // No coger los porcentages.
                estructura.AppendLine("<cbc:IssueDate>" + Convert.ToDateTime(this.Fechafac).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");          //  + Environment.NewLine +
                estructura.AppendLine("<cbc:IssueTime>" + Hora + "-05:00" + "</cbc:IssueTime>");          //  + Environment.NewLine +
                if (tipoDocumento == "C")
                {
                    estructura.AppendLine("<cbc:CreditNoteTypeCode>" + this.TipoFactura + "</cbc:CreditNoteTypeCode>");
                }
                estructura.AppendLine("<cbc:Note>" + this.Observaciones + "</cbc:Note>");
                estructura.AppendLine("<cbc:DocumentCurrencyCode>" + "COP" + "</cbc:DocumentCurrencyCode>");//Revisar si es COP en todos los casos
                estructura.AppendLine("<cbc:LineCountNumeric>" + countFacturas + "</cbc:LineCountNumeric>");
                estructura.AppendLine("<cac:DiscrepancyResponse>");
                estructura.AppendLine("     <cbc:ReferenceID>" + miFactura.Resolucion + miFactura.Facturanumero + "</cbc:ReferenceID>");
                estructura.AppendLine("     <cbc:ResponseCode>" + this.Concepto + "</cbc:ResponseCode>");
                estructura.AppendLine("     <cbc:Description>" + this.Concepto + "</cbc:Description>");
                estructura.AppendLine("</cac:DiscrepancyResponse>");

                if (this.NumeroOrden != string.Empty && this.FechaOrden != string.Empty)
                {
                    estructura.AppendLine("<cac:OrderReference>");
                    estructura.AppendLine("     <cbc:ID>" + this.NumeroOrden + "</cbc:ID>");
                    estructura.AppendLine("     <cbc:IssueDate>" + Convert.ToDateTime(this.FechaOrden).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");
                    estructura.AppendLine("</cac:OrderReference>");
                }

                estructura.AppendLine("<cac:BillingReference>");
                estructura.AppendLine("     <cac:InvoiceDocumentReference>");
                estructura.AppendLine("         <cbc:ID>" + miFactura.Resolucion + miFactura.Facturanumero + "</cbc:ID>");
                estructura.AppendLine("         <cbc:UUID schemeName='CUFE-SHA384'>" + miFactura.CUFE + "</cbc:UUID>");
                estructura.AppendLine("         <cbc:IssueDate>" + Convert.ToDateTime(miFactura.Fechafac).ToString("yyyy-MM-dd") + "</cbc:IssueDate>");
                estructura.AppendLine("     </cac:InvoiceDocumentReference>");
                estructura.AppendLine("</cac:BillingReference>");
            }
            return estructura.ToString();
        }
        public String TotalToXml(List<double> Totales, string tipoFactura, string tipoMoneda,
            double totalBaseImponibleImp, double totalRetenciones, double totaLineas)
        {

            //String estructura =
            StringBuilder estructura = new StringBuilder();
            if (tipoFactura == "F" || tipoFactura == "C")
            {
                estructura.AppendLine("<cac:LegalMonetaryTotal>");          //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:LineExtensionAmount currencyID='" + tipoMoneda + "'>" + totaLineas.ToString("F").Replace(',', '.') + "</cbc:LineExtensionAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:TaxExclusiveAmount currencyID='" + tipoMoneda + "'>" + totalBaseImponibleImp.ToString("F").Replace(',', '.') + "</cbc:TaxExclusiveAmount>");
                estructura.AppendLine("    <cbc:TaxInclusiveAmount currencyID='" + tipoMoneda + "'>" + (totaLineas + Totales[2]).ToString("F").Replace(',', '.') + "</cbc:TaxInclusiveAmount>"); //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:PrepaidAmount currencyID='" + tipoMoneda + "'>" + Totales[4].ToString("F").Replace(',', '.') + "</cbc:PrepaidAmount>");
                estructura.AppendLine("    <cbc:PayableAmount currencyID='" + tipoMoneda + "'>" + (totaLineas + Totales[2] - Totales[4]).ToString("F").Replace(',', '.') + "</cbc:PayableAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("</cac:LegalMonetaryTotal>");
            }
            else if (tipoFactura == "D")
            {

                estructura.AppendLine("<cac:RequestedMonetaryTotal>");          //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:LineExtensionAmount currencyID='COP'>" + totaLineas.ToString("F").Replace(',', '.') + "</cbc:LineExtensionAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:TaxExclusiveAmount currencyID='COP'>" + totalBaseImponibleImp.ToString("F").Replace(',', '.') + "</cbc:TaxExclusiveAmount>");
                estructura.AppendLine("    <cbc:TaxInclusiveAmount currencyID='COP'>" + (totaLineas + Totales[2]).ToString("F").Replace(',', '.') + "</cbc:TaxInclusiveAmount>"); //  + Environment.NewLine +
                estructura.AppendLine("    <cbc:PayableAmount currencyID='COP'>" + (totaLineas + Totales[2] - Totales[4]).ToString("F").Replace(',', '.') + "</cbc:PayableAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("</cac:RequestedMonetaryTotal>");
            }

            return estructura.ToString();
        }

        public String RetencionesToXml(List<FacturaImpuestos> retenciones, string tipoMoneda, string tipoFactura)
        {
            var retencionesList = (from r in retenciones
                                   group r by r.TipoImpuesto into pg
                                   select new
                                   {
                                       totalRetencion = pg.Sum(rr => rr.ValorImpuesto),
                                       retenciones = pg
                                   }).ToList();

            StringBuilder estructura = new StringBuilder();

            foreach (var retencion in retencionesList)
            {
                if (tipoFactura == "F")
                {
                    estructura.AppendLine(" <cac:WithholdingTaxTotal>");          //  + Environment.NewLine +  
                    estructura.AppendLine("     <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (retencion.totalRetencion).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    foreach (var retencionHijo in retencion.retenciones)
                    {
                        estructura.AppendLine("     <cac:TaxSubtotal>");          //  + Environment.NewLine +
                        estructura.AppendLine("         <cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + retencionHijo.BaseImponible.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");          //  + Environment.NewLine +
                        estructura.AppendLine("         <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (retencionHijo.ValorImpuesto).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +            
                        estructura.AppendLine("         <cac:TaxCategory>");          //  + Environment.NewLine +
                        estructura.AppendLine("         <cbc:Percent>" + Math.Abs(retencionHijo.PImpuesto).ToString().Replace(',', '.') + "</cbc:Percent>");          //  + Environment.NewLine +
                        estructura.AppendLine("             <cac:TaxScheme>");          //  + Environment.NewLine +
                        estructura.AppendLine("                 <cbc:ID>" + retencionHijo.TipoImpuesto + "</cbc:ID>");
                        estructura.AppendLine("                 <cbc:Name>" + retencionHijo.Nombre + "</cbc:Name>");
                        estructura.AppendLine("             </cac:TaxScheme>");          //  + Environment.NewLine +
                        estructura.AppendLine("         </cac:TaxCategory>");          //  + Environment.NewLine +
                        estructura.AppendLine("     </cac:TaxSubtotal>");          //  + Environment.NewLine +
                    }
                    estructura.AppendLine(" </cac:WithholdingTaxTotal>");
                }
            }
            return estructura.ToString();
        }

        public String ImpuestosToXml(List<FacturaImpuestos> impuestos, string tipoMoneda)
        {
            var impuestosList = (from r in impuestos
                                 group r by r.TipoImpuesto into pg
                                 select new
                                 {
                                     totalImpuesto = pg.Sum(rr => rr.ValorImpuesto),
                                     impuestos = pg
                                 }).ToList();

            StringBuilder estructura = new StringBuilder();

            foreach (var impuesto in impuestosList)
            {
                estructura.AppendLine(" <cac:TaxTotal>");          //  + Environment.NewLine +  
                estructura.AppendLine("     <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((impuesto.totalImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                foreach (var impuestoHijo in impuesto.impuestos)
                {
                    estructura.AppendLine("     <cac:TaxSubtotal>");          //  + Environment.NewLine +
                    estructura.AppendLine("         <cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + impuestoHijo.BaseImponible.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");          //  + Environment.NewLine +
                    estructura.AppendLine("         <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((impuestoHijo.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +            
                    estructura.AppendLine("         <cac:TaxCategory>");          //  + Environment.NewLine +
                    estructura.AppendLine("         <cbc:Percent>" + Math.Abs(impuestoHijo.PImpuesto).ToString().Replace(',', '.') + "</cbc:Percent>");          //  + Environment.NewLine +
                    estructura.AppendLine("             <cac:TaxScheme>");          //  + Environment.NewLine +
                    estructura.AppendLine("                 <cbc:ID>" + impuestoHijo.TipoImpuesto + "</cbc:ID>");
                    estructura.AppendLine("                 <cbc:Name>" + impuestoHijo.Nombre + "</cbc:Name>"); //  + Environment.NewLine +
                    estructura.AppendLine("             </cac:TaxScheme>");          //  + Environment.NewLine +
                    estructura.AppendLine("         </cac:TaxCategory>");          //  + Environment.NewLine +
                    estructura.AppendLine("     </cac:TaxSubtotal>");          //  + Environment.NewLine +
                }
                estructura.AppendLine(" </cac:TaxTotal>");
            }
            return estructura.ToString();
        }

        public String FormaPagoToXml()
        {
            //String estructura =
            StringBuilder estructura = new StringBuilder();

            estructura.AppendLine("<cac:PaymentMeans>");          //  + Environment.NewLine +
            estructura.AppendLine("    <cbc:ID>" + this.Formapago + "</cbc:ID>");          //  + Environment.NewLine +
            estructura.AppendLine("    <cbc:PaymentMeansCode>" + this.Medio + "</cbc:PaymentMeansCode>");
            estructura.AppendLine("    <cbc:PaymentDueDate>" + Convert.ToDateTime(this.Vencimiento).ToString("yyyy-MM-dd") + "</cbc:PaymentDueDate>");
            estructura.AppendLine("    <cbc:PaymentID>" + this.Id + "</cbc:PaymentID>");
            estructura.AppendLine("</cac:PaymentMeans>");
            return estructura.ToString();
        }

        public String AnticiposToXml(double anticiposValor, string tipoMoneda)
        {
            //String estructura =
            StringBuilder estructura = new StringBuilder();

            estructura.AppendLine("<cac:PrepaidPayment>");          //  + Environment.NewLine +
            estructura.AppendLine("    <cbc:ID>" + this.RecajaNo + "</cbc:ID>");
            estructura.AppendLine("    <cbc:PaidAmount currencyID='" + tipoMoneda + "'>" + anticiposValor.ToString().Replace(',', '.') + "</cbc:PaidAmount>");
            estructura.AppendLine("    <cbc:ReceivedDate>" + Convert.ToDateTime(this.FechaRC).ToString("yyyy-MM-dd") + "</cbc:ReceivedDate>");
            estructura.AppendLine("</cac:PrepaidPayment>");
            return estructura.ToString();
        }

        public string CalculateCUFE(string numFac, string fecFac, string horFac, string valFac, string codImp1,
                                    string valImp1, string codImp2, string valImp2, string codImp3, string valImp3,
                                    string valTot, string nitFE, string numAdq, string clTec, string tipoAmbiente)
        {

            string source = numFac + fecFac + horFac + valFac + codImp1 + valImp1 + codImp2 + valImp2 + codImp3 +
                            valImp3 + valTot + nitFE + numAdq + clTec + tipoAmbiente;

            using (SHA384 sha384Hash = SHA384.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha384Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash.ToLower();
            }
        }
        public String toString()
        {
            String Informacion = "";

            DateTime myDate = DateTime.Parse(this.Fechafac);
            String Fecha = myDate.ToString("yyyy-MM-dd");
            String Hora = myDate.ToString("HH:MM:ss");

            Informacion = "Nro.Factura : " + this.Resolucion + this.Facturanumero + Environment.NewLine +
                          "Fecha : " + Fecha + Environment.NewLine +
                          "Hora : " + Hora + Environment.NewLine +
                          "Observaciones : " + this.Observaciones + Environment.NewLine + Environment.NewLine;
            return Informacion;
        }

        public String toStringImpu(List<double> Totales)
        {
            String Informacion = "";
            Informacion = "Subtotal : " + Totales[1] + Environment.NewLine +
                          "Impuestos : " + Totales[2] + Environment.NewLine +
                          "Total : " + Totales[3] + Environment.NewLine + Environment.NewLine;
            return Informacion;
        }
    }
}
