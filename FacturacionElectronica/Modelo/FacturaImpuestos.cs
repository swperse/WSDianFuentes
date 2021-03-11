using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Modelo
{
    class FacturaImpuestos
    {
        private int _FacturaNumero;
        private String _TipoImpuesto;
        private double _PImpuesto;
        private double _ValorImpuesto;
        private double _BaseImponible;
        private string _Nombre;

        public int FacturaNumero
        {
            get
            {
                return _FacturaNumero;
            }

            set
            {
                _FacturaNumero = value;
            }
        }

        public String TipoImpuesto
        {
            get
            {
                return _TipoImpuesto;
            }

            set
            {
                _TipoImpuesto = value;
            }
        }

        public double PImpuesto
        {
            get
            {
                return _PImpuesto;
            }

            set
            {
                _PImpuesto = value;
            }
        }

        public double  ValorImpuesto
        {
            get
            {
                return _ValorImpuesto;
            }

            set
            {
                _ValorImpuesto = value;
            }
        }

        public double BaseImponible
        {
            get
            {
                return _BaseImponible;
            }

            set
            {
                _BaseImponible = value;
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

        public String toXml(string tipoMoneda)
        {
            //String estructura =
            StringBuilder estructura = new StringBuilder();
            estructura.AppendLine(" <cac:TaxTotal>");          //  + Environment.NewLine +  
            estructura.AppendLine("     <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
            estructura.AppendLine("     <cbc:TaxEvidenceIndicator>false</cbc:TaxEvidenceIndicator>");          //  + Environment.NewLine +
            estructura.AppendLine("     <cac:TaxSubtotal>");          //  + Environment.NewLine +
            estructura.AppendLine("         <cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.BaseImponible.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");          //  + Environment.NewLine +
            estructura.AppendLine("         <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +            
            estructura.AppendLine("         <cac:TaxCategory>");          //  + Environment.NewLine +
            estructura.AppendLine("         <cbc:Percent>" + Math.Abs(this.PImpuesto).ToString().Replace(',', '.') + "</cbc:Percent>");          //  + Environment.NewLine +
            estructura.AppendLine("             <cac:TaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("                 <cbc:ID>" + this.TipoImpuesto + "</cbc:ID>");          //  + Environment.NewLine +
            estructura.AppendLine("             </cac:TaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("         </cac:TaxCategory>");          //  + Environment.NewLine +
            estructura.AppendLine("     </cac:TaxSubtotal>");          //  + Environment.NewLine +
            estructura.AppendLine(" </cac:TaxTotal>");

            return estructura.ToString();
        }

        public String retencionesToXml(string tipoMoneda, string tipoFactura)
        {
            if (tipoFactura == "F")
            {
                //String estructura =
                StringBuilder estructura = new StringBuilder();
                estructura.AppendLine(" <cac:WithholdingTaxTotal>");          //  + Environment.NewLine +  
                estructura.AppendLine("     <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("     <cac:TaxSubtotal>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.BaseImponible.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +            
                estructura.AppendLine("         <cac:TaxCategory>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:Percent>" + Math.Abs(this.PImpuesto).ToString().Replace(',', '.') + "</cbc:Percent>");          //  + Environment.NewLine +
                estructura.AppendLine("             <cac:TaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("                 <cbc:ID>" + this.TipoImpuesto + "</cbc:ID>");          //  + Environment.NewLine +
                estructura.AppendLine("             </cac:TaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("         </cac:TaxCategory>");          //  + Environment.NewLine +
                estructura.AppendLine("     </cac:TaxSubtotal>");          //  + Environment.NewLine +
                estructura.AppendLine(" </cac:WithholdingTaxTotal>");

                return estructura.ToString();
            }
            else
            {
                //String estructura =
                StringBuilder estructura = new StringBuilder();
                //estructura.AppendLine(" <cac:WithholdingTaxTotal>");
                estructura.AppendLine(" <cac:TaxTotal>"); //  + Environment.NewLine +  
                estructura.AppendLine("     <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("     <cac:TaxSubtotal>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.BaseImponible.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.ValorImpuesto),2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +            
                estructura.AppendLine("         <cac:TaxCategory>");          //  + Environment.NewLine +
                estructura.AppendLine("         <cbc:Percent>" + Math.Abs(this.PImpuesto).ToString().Replace(',', '.') + "</cbc:Percent>");          //  + Environment.NewLine +
                estructura.AppendLine("             <cac:TaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("                 <cbc:ID>" + this.TipoImpuesto + "</cbc:ID>");          //  + Environment.NewLine +
                estructura.AppendLine("             </cac:TaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("         </cac:TaxCategory>");          //  + Environment.NewLine +
                estructura.AppendLine("     </cac:TaxSubtotal>");          //  + Environment.NewLine +
                estructura.AppendLine(" </cac:TaxTotal>");
                //estructura.AppendLine(" </cac:WithholdingTaxTotal>");

                return estructura.ToString();
            }
        }
    }
}
