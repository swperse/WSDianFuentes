using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Modelo
{
    class DetalleFactura
    {
        int _Facturanumero;
        String _Referencia;
        String _Nombreref;
        double _Precioventa;
        double _Cantidad;
        double _Descuento;
        double _Precio;
        double _PIva;
        double _Total;
        int _Neto;
        int _Subtotal;
        string _concepto;
        string _Marca;
        string _Modelo;
        string _Subgrupo;
        string _CodigoIva;
        string _CodigoRete;
        double _PRete;
        double _VPropina;
        string _CodigoIConsumo;
        double _PIConsumo;
        int _Idpedir;
        int _TDescuento;
        string _CODREF;
        string _Proveedor;
        string _ProveedorClase;
        string _ProveedorDV;
        double _PReteIcasub;
        double _ReteIcasub;
        string _CodigoReteIca;
        int _Costo;



        public int Facturanumero
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
        public string Concepto
        {
            get
            {
                return _concepto;
            }

            set
            {
                _concepto = value;
            }
        }


        public String Referencia
        {
            get
            {
                return _Referencia;
            }

            set
            {
                _Referencia = value;
            }
        }

        public String Nombreref
        {
            get
            {
                return _Nombreref;
            }

            set
            {
                _Nombreref = value;
            }
        }
        public String Marca
        {
            get
            {
                return _Marca;
            }

            set
            {
                _Marca = value;
            }
        }

        public String Modelo
        {
            get
            {
                return _Modelo;
            }

            set
            {
                _Modelo = value;
            }
        }


        public double Precioventa
        {
            get
            {
                return _Precioventa;
            }

            set
            {
                _Precioventa = value;
            }
        }

        public double Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                _Cantidad = value;
            }
        }

        public double Descuento
        {
            get
            {
                return _Descuento;
            }

            set
            {
                _Descuento = value;
            }
        }

        public double Precio
        {
            get
            {
                return _Precio;
            }

            set
            {
                _Precio = value;
            }
        }

        public double PIva
        {
            get
            {
                return _PIva;
            }

            set
            {
                _PIva = value;
            }
        }

        public double Total
        {
            get
            {
                return _Total;
            }

            set
            {
                _Total = value;
            }
        }

        public int Neto
        {
            get
            {
                return _Neto;
            }

            set
            {
                _Neto = value;
            }
        }

        public int Subtotal
        {
            get
            {
                return _Subtotal;
            }

            set
            {
                _Subtotal = value;
            }
        }

        public string Subgrupo
        {
            get
            {
                return _Subgrupo;
            }

            set
            {
                _Subgrupo = value;
            }
        }
        public string CodigoIva
        {
            get
            {
                return _CodigoIva;
            }

            set
            {
                _CodigoIva = value;
            }
        }

        public string CodigoRete
        {
            get
            {
                return _CodigoRete;
            }

            set
            {
                _CodigoRete = value;
            }
        }

        public double PRete
        {
            get
            {
                return _PRete;
            }

            set
            {
                _PRete = value;
            }
        }

        public double VPropina
        {
            get
            {
                return _VPropina;
            }

            set
            {
                _VPropina = value;
            }
        }

        public string CodigoIConsumo
        {
            get
            {
                return _CodigoIConsumo;
            }

            set
            {
                _CodigoIConsumo = value;
            }
        }

        public double PIConsumo
        {
            get
            {
                return _PIConsumo;
            }

            set
            {
                _PIConsumo = value;
            }
        }

        public int Idpedir
        {
            get
            {
                return _Idpedir;
            }

            set
            {
                _Idpedir = value;
            }
        }

        public int TDescuento
        {
            get
            {
                return _TDescuento;
            }

            set
            {
                _TDescuento = value;
            }
        }

        public string CODREF
        {
            get
            {
                return _CODREF;
            }

            set
            {
                _CODREF = value;
            }
        }

        public string Proveedor
        {
            get
            {
                return _Proveedor;
            }

            set
            {
                _Proveedor = value;
            }
        }

        public string ProveedorDV
        {
            get
            {
                return _ProveedorDV;
            }

            set
            {
                _ProveedorDV = value;
            }
        }

        public string ProveedorClase
        {
            get
            {
                return _ProveedorClase;
            }

            set
            {
                _ProveedorClase = value;
            }
        }

        public double PReteIcasub
        {
            get
            {
                return _PReteIcasub;
            }

            set
            {
                _PReteIcasub = value;
            }
        }

        public double ReteIcasub
        {
            get
            {
                return _ReteIcasub;
            }

            set
            {
                _ReteIcasub = value;
            }
        }

        public string CodigoReteIca
        {
            get
            {
                return _CodigoReteIca;
            }

            set
            {
                _CodigoReteIca = value;
            }
        }

        public int Costo
        {
            get
            {
                return _Costo;
            }

            set
            {
                _Costo = value;
            }
        }


        //  Campo de control interno para registrar la información de costos de proveedor
        public bool ValorProveedorPorRegistrar { get; set; }

        public String toXML(string tipoFactura, string numeroDocumento, string dv, string tipoMoneda,
                            string clienteDV, string clienteIdentificacion, string tipoOperacion, string tipoDocumento, List<FacturaImpuestos> retenciones)
        {
            var documentType = dv != "" ? 31 : 13;
            var documentTypeCustomer = clienteDV != "" ? 31 : 13;

            StringBuilder str = new StringBuilder();

            //  Registra el valor del costo de proveedor de factura de mandato
            if (ValorProveedorPorRegistrar)
            {
                this.Referencia = (1 + Convert.ToInt32(this.Referencia)).ToString();

                str.AppendLine("<cac:InvoiceLine>");
                str.AppendLine(" <cbc:ID>" + this.Referencia + "</cbc:ID>");
                str.AppendLine("	<cbc:InvoicedQuantity>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:InvoicedQuantity>");
                str.AppendLine("	<cbc:LineExtensionAmount currencyID='COP'>" + Costo + "</cbc:LineExtensionAmount>");
                str.AppendLine("	<cbc:FreeOfChargeIndicator>" + "false" + "</cbc:FreeOfChargeIndicator >");

                str.AppendLine("	<cac:Item>");
                str.AppendLine("		<cbc:Description>" + this.Nombreref + "</cbc:Description>");
                str.AppendLine("	<cac:SellersItemIdentification>");
                str.AppendLine("		<cbc:ID>" + this.CODREF + "</cbc:ID>");
                str.AppendLine("	</cac:SellersItemIdentification>");
                str.AppendLine(" <cac:StandardItemIdentification>");
                str.AppendLine("   <cbc:ID schemeAgencyID = '' schemeID = '999' schemeName = 'Estándar de adopción del contribuyente'></cbc:ID>");
                str.AppendLine(" </cac:StandardItemIdentification>");

                str.AppendLine("	    <cac:InformationContentProviderParty>");
                str.AppendLine("		    <cac:PowerOfAttorney>");
                str.AppendLine("		        <cac:AgentParty>");
                str.AppendLine("		            <cac:PartyIdentification>");
                str.AppendLine("                     <cbc:ID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + ProveedorDV + "' schemeName='" + ProveedorClase + "'>" + Proveedor + "</cbc:ID>");
                str.AppendLine("		            </cac:PartyIdentification>");
                str.AppendLine("		        </cac:AgentParty>");
                str.AppendLine("		    </cac:PowerOfAttorney>");
                str.AppendLine("	    </cac:InformationContentProviderParty>");

                str.AppendLine("	</cac:Item>");
                str.AppendLine("	<cac:Price>");
                str.AppendLine("		<cbc:PriceAmount currencyID='" + tipoMoneda + "'>" + Costo + "</cbc:PriceAmount>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:BaseQuantity unitCode='EA'>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:BaseQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	</cac:Price>");
                str.AppendLine("</cac:InvoiceLine>");

                ValorProveedorPorRegistrar = false;

                return str.ToString();
            }


            if (tipoFactura == "F")
            {
                str.AppendLine("<cac:InvoiceLine>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:ID>" + this.Referencia + "</cbc:ID>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:InvoicedQuantity>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:InvoicedQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:LineExtensionAmount currencyID='COP'>" +
                                        (tipoOperacion.Equals("11") && Costo > 0 ? (this.Neto - this.Costo) : (this.Neto)) +
                                    "</cbc:LineExtensionAmount>");
                str.AppendLine("	<cbc:FreeOfChargeIndicator>" + "false" + "</cbc:FreeOfChargeIndicator >");
                if (Descuento > 0)
                {
                    str.AppendLine("     <cac:AllowanceCharge>");
                    str.AppendLine("         <cbc:ID>" + this.Referencia + "</cbc:ID>");
                    str.AppendLine("         <cbc:ChargeIndicator>" + "false" + "</cbc:ChargeIndicator>");
                    str.AppendLine("         <cbc:AllowanceChargeReason>" + "Descuento" + "</cbc:AllowanceChargeReason>");
                    str.AppendLine("         <cbc:MultiplierFactorNumeric>" + this.Descuento.ToString().Replace(',', '.') + "</cbc:MultiplierFactorNumeric>");
                    str.AppendLine("         <cbc:Amount currencyID='COP'>" + this.TDescuento + "</cbc:Amount>");
                    str.AppendLine("         <cbc:BaseAmount currencyID='COP'>" + this.Subtotal + "</cbc:BaseAmount>");
                    str.AppendLine("     </cac:AllowanceCharge>");
                }



                if (tipoOperacion == "09")
                {
                    str.AppendLine("     <cbc:Note>Contrato de servicios AIU por concepto de: " + this.Nombreref + "</cbc:Note>");
                }

                if (this.PIva > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIva.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "01" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "IVA" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");

                    var reteIva = retenciones.Where(r => r.TipoImpuesto == "05").FirstOrDefault();
                    if (reteIva != null)
                    {
                        //Colocar el reteiva
                        str.AppendLine("	<cac:WithholdingTaxTotal>");          //  + Environment.NewLine +
                        str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round(((this.Total - this.Neto) * (reteIva.PImpuesto / 100)), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                        str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                        str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIva / 100)).ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                        str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round(((this.Total - this.Neto) * (reteIva.PImpuesto / 100)), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                        str.AppendLine("	<cac:TaxCategory>");
                        str.AppendLine("		<cbc:Percent>" + reteIva.PImpuesto.ToString().Replace(',', '.') + "</cbc:Percent>");
                        str.AppendLine("	<cac:TaxScheme>");
                        str.AppendLine("		<cbc:ID>" + "05" + "</cbc:ID>");
                        str.AppendLine("		<cbc:Name>" + "ReteIVA," + "</cbc:Name>");
                        str.AppendLine("	</cac:TaxScheme>");
                        str.AppendLine("	</cac:TaxCategory>");
                        str.AppendLine("	</cac:TaxSubtotal>");
                        str.AppendLine("	</cac:WithholdingTaxTotal>");
                    }
                }
                if (this.PIConsumo > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIConsumo.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "04" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "INC" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");
                }
                if (this.PRete > 0)
                {
                    str.AppendLine("	<cac:WithholdingTaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Neto * (this.PRete / 100)), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Neto * (this.PRete / 100)), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PRete.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "06" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "ReteFuente" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:WithholdingTaxTotal>");
                }

                str.AppendLine("	<cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:Description>" + this.Nombreref + "</cbc:Description>");   // + Environment.NewLine +
                str.AppendLine("	<cac:SellersItemIdentification>");
                str.AppendLine("		<cbc:ID>" + this.CODREF + "</cbc:ID>");
                str.AppendLine("	</cac:SellersItemIdentification>");
                str.AppendLine(" <cac:StandardItemIdentification>");
                str.AppendLine("   <cbc:ID schemeAgencyID = '' schemeID = '999' schemeName = 'Estándar de adopción del contribuyente'></cbc:ID>");
                str.AppendLine(" </cac:StandardItemIdentification>");

                if (tipoOperacion == "11" && this.Marca.Replace(" ", "").Contains("terceros") && Costo == 0)//Se debe agregar si el grupo es Ingresos para Terceros
                { 
                    //ValorProveedorPorRegistrar = true;
                    str.AppendLine("	    <cac:InformationContentProviderParty>");          //  + Environment.NewLine +
                    str.AppendLine("		    <cac:PowerOfAttorney>");
                    str.AppendLine("		        <cac:AgentParty>");
                    str.AppendLine("		            <cac:PartyIdentification>");
                    str.AppendLine("                     <cbc:ID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + ProveedorDV + "' schemeName='" + ProveedorClase + "'>" + Proveedor + "</cbc:ID>");
                    str.AppendLine("		            </cac:PartyIdentification>");
                    str.AppendLine("		        </cac:AgentParty>");
                    str.AppendLine("		    </cac:PowerOfAttorney>");
                    str.AppendLine("	    </cac:InformationContentProviderParty>");
                }
                if (tipoOperacion == "11"  && Costo > 0){
                    ValorProveedorPorRegistrar = true;
                }
                if (tipoDocumento == "04")//Exportación
                {
                    str.AppendLine(" <cbc:BrandName>" + this.Marca + "</cbc:BrandName>");
                    str.AppendLine(" <cbc:ModelName>" + this.Modelo + "</cbc:ModelName>");
                }
                str.AppendLine("	</cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("	<cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:PriceAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Subtotal / this.Cantidad), 2).ToString().Replace(',', '.') + "</cbc:PriceAmount>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:BaseQuantity unitCode='EA'>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:BaseQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	</cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("</cac:InvoiceLine>");
            }

            if (tipoFactura == "D")
            {
                str.AppendLine("<cac:DebitNoteLine>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:ID>" + this.Referencia + "</cbc:ID>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:DebitedQuantity>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:DebitedQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:LineExtensionAmount currencyID='" + tipoMoneda + "'>" + this.Neto + "</cbc:LineExtensionAmount>");


                if (this.PIva > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIva.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "01" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "IVA" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");

                    var reteIva = retenciones.Where(r => r.TipoImpuesto == "05").FirstOrDefault();
                    if (reteIva != null)
                    {
                        //Colocar el reteiva
                        //estructura.AppendLine("	<cac:WithholdingTaxTotal>");  
                        //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + ((this.Total - this.Neto) * (reteIva.PImpuesto / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                        //estructura.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                        //estructura.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIva / 100)).ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                        //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + ((this.Total - this.Neto) * (reteIva.PImpuesto / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                        //estructura.AppendLine("	<cac:TaxCategory>");
                        //estructura.AppendLine("		<cbc:Percent>" + reteIva.PImpuesto.ToString().Replace(',', '.') + "</cbc:Percent>");
                        //estructura.AppendLine("	<cac:TaxScheme>");
                        //estructura.AppendLine("		<cbc:ID>" + "05" + "</cbc:ID>");
                        //estructura.AppendLine("		<cbc:Name>" + "ReteIVA," + "</cbc:Name>");
                        //estructura.AppendLine("	</cac:TaxScheme>");
                        //estructura.AppendLine("	</cac:TaxCategory>");
                        //estructura.AppendLine("	</cac:TaxSubtotal>");
                        //estructura.AppendLine("	</cac:WithholdingTaxTotal>");
                    }
                }
                if (this.PIConsumo > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIConsumo.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "04" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "INC" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");
                }
                if (this.PRete > 0)
                {
                    //estructura.AppendLine("	<cac:WithholdingTaxTotal>");
                    //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PRete / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    //estructura.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PRete / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    //estructura.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:Percent>" + this.PRete.ToString().Replace(',', '.') + "</cbc:Percent>");
                    //estructura.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:ID>" + "06" + "</cbc:ID>");
                    //estructura.AppendLine("		<cbc:Name>" + "ReteFuente" + "</cbc:Name>");
                    //estructura.AppendLine("	</cac:TaxScheme>");
                    //estructura.AppendLine("	</cac:TaxCategory>");
                    //estructura.AppendLine("	</cac:TaxSubtotal>");
                    //estructura.AppendLine("	</cac:WithholdingTaxTotal>");
                }


                str.AppendLine("	<cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:Description>" + this.Nombreref + "</cbc:Description>");   // + Environment.NewLine +
                str.AppendLine("	<cac:SellersItemIdentification>");
                str.AppendLine("		<cbc:ID>" + this.Nombreref + "</cbc:ID>");
                str.AppendLine("	</cac:SellersItemIdentification>");
                str.AppendLine("	<cac:InformationContentProviderParty>");
                str.AppendLine("	        <cac:PowerOfAttorney>");
                str.AppendLine("		        <cac:AgentParty>");
                str.AppendLine("		            <cac:PartyIdentification>");
                str.AppendLine("		                <cbc:ID schemeAgencyID='195' schemeID='" + dv + "' schemeName='" + documentType + "'>" + numeroDocumento + "</cbc:ID>");
                str.AppendLine("	                </cac:PartyIdentification>");
                str.AppendLine("	            </cac:AgentParty>");
                str.AppendLine("	        </cac:PowerOfAttorney>");
                str.AppendLine("	</cac:InformationContentProviderParty>");



                str.AppendLine("	</cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("	<cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:PriceAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Subtotal / this.Cantidad), 2).ToString().Replace(',', '.') + "</cbc:PriceAmount>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:BaseQuantity unitCode='EA'>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:BaseQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	</cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("</cac:DebitNoteLine>");
            }

            if (tipoFactura == "C")
            {
                str.AppendLine("<cac:CreditNoteLine>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:ID>" + this.Referencia + "</cbc:ID>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:CreditedQuantity>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:CreditedQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	<cbc:LineExtensionAmount currencyID='" + tipoMoneda + "'>" + this.Neto + "</cbc:LineExtensionAmount>");


                if (this.PIva > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Total - this.Neto), 2).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIva.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "01" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "IVA" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");

                    var reteIva = retenciones.Where(r => r.TipoImpuesto == "05").FirstOrDefault();
                    if (reteIva != null)
                    {
                        //Colocar el reteiva
                        //estructura.AppendLine("	<cac:WithholdingTaxTotal>");
                        //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + ((this.Total - this.Neto) * (reteIva.PImpuesto / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                        //estructura.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                        //estructura.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIva / 100)).ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                        //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + ((this.Total - this.Neto) * (reteIva.PImpuesto / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                        //estructura.AppendLine("	<cac:TaxCategory>");
                        //estructura.AppendLine("		<cbc:Percent>" + reteIva.PImpuesto.ToString().Replace(',', '.') + "</cbc:Percent>");
                        //estructura.AppendLine("	<cac:TaxScheme>");
                        //estructura.AppendLine("		<cbc:ID>" + "05" + "</cbc:ID>");
                        //estructura.AppendLine("		<cbc:Name>" + "ReteIVA," + "</cbc:Name>");
                        //estructura.AppendLine("	</cac:TaxScheme>");
                        //estructura.AppendLine("	</cac:TaxCategory>");
                        //estructura.AppendLine("	</cac:TaxSubtotal>");
                        //estructura.AppendLine("	</cac:WithholdingTaxTotal>");
                    }
                }
                if (this.PIConsumo > 0)
                {
                    str.AppendLine("	<cac:TaxTotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    str.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    str.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PIConsumo / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    str.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:Percent>" + this.PIConsumo.ToString().Replace(',', '.') + "</cbc:Percent>");
                    str.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    str.AppendLine("		<cbc:ID>" + "04" + "</cbc:ID>");
                    str.AppendLine("		<cbc:Name>" + "INC" + "</cbc:Name>");
                    str.AppendLine("	</cac:TaxScheme>");
                    str.AppendLine("	</cac:TaxCategory>");
                    str.AppendLine("	</cac:TaxSubtotal>");
                    str.AppendLine("	</cac:TaxTotal>");
                }
                if (this.PRete > 0)
                {
                    //estructura.AppendLine("	<cac:WithholdingTaxTotal>");
                    //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PRete / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");          //  + Environment.NewLine +
                    //estructura.AppendLine("	<cac:TaxSubtotal>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:TaxableAmount currencyID='" + tipoMoneda + "'>" + this.Neto.ToString().Replace(',', '.') + "</cbc:TaxableAmount>");
                    //estructura.AppendLine("		<cbc:TaxAmount currencyID='" + tipoMoneda + "'>" + (this.Neto * (this.PRete / 100)).ToString().Replace(',', '.') + "</cbc:TaxAmount>");
                    //estructura.AppendLine("	<cac:TaxCategory>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:Percent>" + this.PRete.ToString().Replace(',', '.') + "</cbc:Percent>");
                    //estructura.AppendLine("	<cac:TaxScheme>");          //  + Environment.NewLine +
                    //estructura.AppendLine("		<cbc:ID>" + "06" + "</cbc:ID>");
                    //estructura.AppendLine("		<cbc:Name>" + "ReteFuente" + "</cbc:Name>");
                    //estructura.AppendLine("	</cac:TaxScheme>");
                    //estructura.AppendLine("	</cac:TaxCategory>");
                    //estructura.AppendLine("	</cac:TaxSubtotal>");
                    //estructura.AppendLine("	</cac:WithholdingTaxTotal>");
                }


                str.AppendLine("	<cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:Description>" + this.Nombreref + "</cbc:Description>");   // + Environment.NewLine +
                str.AppendLine("	<cac:SellersItemIdentification>");
                str.AppendLine("		<cbc:ID>" + this.Nombreref + "</cbc:ID>");
                str.AppendLine("	</cac:SellersItemIdentification>");
                str.AppendLine("	<cac:InformationContentProviderParty>");
                str.AppendLine("	        <cac:PowerOfAttorney>");
                str.AppendLine("		        <cac:AgentParty>");
                str.AppendLine("		            <cac:PartyIdentification>");
                str.AppendLine("		                <cbc:ID schemeAgencyID='195' schemeID='" + dv + "' schemeName='" + documentType + "'>" + numeroDocumento + "</cbc:ID>");
                str.AppendLine("	                </cac:PartyIdentification>");
                str.AppendLine("	            </cac:AgentParty>");
                str.AppendLine("	        </cac:PowerOfAttorney>");
                str.AppendLine("	</cac:InformationContentProviderParty>");



                str.AppendLine("	</cac:Item>");          //  + Environment.NewLine +
                str.AppendLine("	<cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:PriceAmount currencyID='" + tipoMoneda + "'>" + Math.Round((this.Subtotal / this.Cantidad), 2).ToString().Replace(',', '.') + "</cbc:PriceAmount>");          //  + Environment.NewLine +
                str.AppendLine("		<cbc:BaseQuantity unitCode='EA'>" + this.Cantidad.ToString().Replace(',', '.') + "</cbc:BaseQuantity>");          //  + Environment.NewLine +
                str.AppendLine("	</cac:Price>");          //  + Environment.NewLine +
                str.AppendLine("</cac:CreditNoteLine>");
            }


            return str.ToString();
        }

    }
}
