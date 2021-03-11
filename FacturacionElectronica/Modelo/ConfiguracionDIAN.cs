using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Modelo
{
    class ConfiguracionDIAN
    {
        int _IdConfiguracionDIAN;
        String _NroResolucion;
        String _FechaResolucion;
        String _Prefijo;
        String _Desde;
        String _Hasta;
        String _IdProveedor;
        String _IdSoftware;
        String _ClaveTecnica;
        String _NombreSoftware;
        String _Pin;
        String _CodigoSeguridad;
        String _Vigencia;
        String _TipoAmbiente;
        int _Estado;
        String _DV;

        public int IdConfiguracionDIAN
        {
            get
            {
                return _IdConfiguracionDIAN;
            }

            set
            {
                _IdConfiguracionDIAN = value;
            }
        }

        public string NroResolucion
        {
            get
            {
                return _NroResolucion;
            }

            set
            {
                _NroResolucion = value;
            }
        }

        public string FechaResolucion
        {
            get
            {
                return _FechaResolucion;
            }

            set
            {
                _FechaResolucion = value;
            }
        }

        public string Prefijo
        {
            get
            {
                return _Prefijo;
            }

            set
            {
                _Prefijo = value;
            }
        }

        public string Desde
        {
            get
            {
                return _Desde;
            }

            set
            {
                _Desde = value;
            }
        }

        public string Hasta
        {
            get
            {
                return _Hasta;
            }

            set
            {
                _Hasta = value;
            }
        }

        public string IdProveedor
        {
            get
            {
                return _IdProveedor;
            }

            set
            {
                _IdProveedor = value;
            }
        }

        public string IdSoftware
        {
            get
            {
                return _IdSoftware;
            }

            set
            {
                _IdSoftware = value;
            }
        }

        public string ClaveTecnica
        {
            get
            {
                return _ClaveTecnica;
            }

            set
            {
                _ClaveTecnica = value;
            }
        }

        public string NombreSoftware
        {
            get
            {
                return _NombreSoftware;
            }

            set
            {
                _NombreSoftware = value;
            }
        }

        public string Pin
        {
            get
            {
                return _Pin;
            }

            set
            {
                _Pin = value;
            }
        }

        public string CodigoSeguridad
        {
            get
            {
                return _CodigoSeguridad;
            }

            set
            {
                _CodigoSeguridad = value;
            }
        }

        public int Estado
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

        public string Vigencia
        {
            get
            {
                return _Vigencia;
            }

            set
            {
                _Vigencia = value;
            }
        }

        public string TipoAmbiente
        {
            get
            {
                return _TipoAmbiente;
            }

            set
            {
                _TipoAmbiente = value;
            }
        }


        public string DV
        {
            get
            {
                return _DV;
            }

            set
            {
                _DV = value;
            }
        }

        public string SHA384(string str)
        {
            SHA384 sha384 = SHA384Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha384.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public String toXML(string tipoDocumento, Factura documento, string facturador, string cliente, double totalFactura)
        {
            var documentType = string.IsNullOrEmpty(this.DV) == false ? 31 : 13;
            //String estructura = "";
            StringBuilder estructura = new StringBuilder();

            if (tipoDocumento == "F")
            {
                //Encabezado
                estructura.AppendLine("<?xml version='1.0' encoding='UTF-8' standalone='no'?>");
                estructura.AppendLine("<Invoice xmlns='urn:oasis:names:specification:ubl:schema:xsd:Invoice-2' xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' xmlns:sts='dian:gov:co:facturaelectronica:Structures-2-1' xmlns:xades='http://uri.etsi.org/01903/v1.3.2#' xmlns:xades141='http://uri.etsi.org/01903/v1.4.1#' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:oasis:names:specification:ubl:schema:xsd:Invoice-2     http://docs.oasis-open.org/ubl/os-UBL-2.1/xsd/maindoc/UBL-Invoice-2.1.xsd'>");       // + Environment.NewLine +
                estructura.AppendLine("<ext:UBLExtensions>");
                //Extensión para la firma
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");
                //Configuración
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("	<sts:DianExtensions>");
                estructura.AppendLine("		<sts:InvoiceControl>");
                estructura.AppendLine("			<sts:InvoiceAuthorization>" + this.NroResolucion + "</sts:InvoiceAuthorization>");
                estructura.AppendLine("			<sts:AuthorizationPeriod>");
                estructura.AppendLine("				<cbc:StartDate>" + this.FechaResolucion + "</cbc:StartDate>");
                estructura.AppendLine("               <cbc:EndDate>" + this.Vigencia + "</cbc:EndDate>");
                estructura.AppendLine("			</sts:AuthorizationPeriod>");
                estructura.AppendLine("			<sts:AuthorizedInvoices>");
                estructura.AppendLine("				<sts:Prefix>" + this.Prefijo + "</sts:Prefix>");
                estructura.AppendLine("				<sts:From>" + this.Desde + "</sts:From>");
                estructura.AppendLine("				<sts:To>" + this.Hasta + "</sts:To>");
                estructura.AppendLine("			</sts:AuthorizedInvoices>");
                estructura.AppendLine("		</sts:InvoiceControl>");
                estructura.AppendLine("		<sts:InvoiceSource>");
                estructura.AppendLine("			<cbc:IdentificationCode listAgencyID='6' listAgencyName='United Nations Economic Commission for Europe' listSchemeURI='urn:oasis:names:specification:ubl:codelist:gc:CountryIdentificationCode-2.1'>CO</cbc:IdentificationCode>");
                estructura.AppendLine("		</sts:InvoiceSource>");
                estructura.AppendLine("		<sts:SoftwareProvider>");
                estructura.AppendLine("			<sts:ProviderID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Direccion de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.IdProveedor + "</sts:ProviderID>");
                estructura.AppendLine("			<sts:SoftwareID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.IdSoftware + "</sts:SoftwareID>");
                estructura.AppendLine("		</sts:SoftwareProvider>");
                estructura.AppendLine("		<sts:SoftwareSecurityCode schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.SHA384(this.IdSoftware + this.Pin + documento.Resolucion + documento.Facturanumero) + "</sts:SoftwareSecurityCode>");
                estructura.AppendLine("		<sts:AuthorizationProvider>");
                estructura.AppendLine("			<sts:AuthorizationProviderID schemeAgencyID='195' schemeID='4' schemeName='31' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>800197268</sts:AuthorizationProviderID>");
                estructura.AppendLine("		</sts:AuthorizationProvider>");
                estructura.AppendLine("		<sts:QRCode>" + "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + documento.CUFE + "</sts:QRCode>");
                //estructura.AppendLine("		<sts:QRCode> NroFactura=" + documento.Resolucion + documento.Facturanumero);
                //estructura.AppendLine("			NitFacturador=" + facturador);
                //estructura.AppendLine("			NitAdquiriente=" + cliente);
                //estructura.AppendLine("			FechaFactura=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy"));
                //estructura.AppendLine("			ValorTotalFactura=" + totalFactura.ToString().Replace(',', '.'));
                //estructura.AppendLine("			CUFE=" + documento.CUFE);
                //estructura.AppendLine("			URL=https://catalogo-vpfe-hab.dian.gov.co/Document/FindDocument?documentKey=" + documento.CUFE + "&amp;partitionKey=co|06|94&amp;emissionDate=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy") + "</sts:QRCode>");
                estructura.AppendLine("	</sts:DianExtensions>");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");

                estructura.AppendLine("</ext:UBLExtensions>");
            }

            if (tipoDocumento == "D")
            {
                //Encabezado
                estructura.AppendLine("<?xml version='1.0' encoding='UTF-8' standalone='no'?>");
                estructura.AppendLine("<DebitNote xmlns='urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2' xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' xmlns:sts='http://www.dian.gov.co/contratos/facturaelectronica/v1/Structures' xmlns:xades='http://uri.etsi.org/01903/v1.3.2#' xmlns:xades141='http://uri.etsi.org/01903/v1.4.1#' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2    http://docs.oasis-open.org/ubl/os-UBL-2.1/xsd/maindoc/UBL-DebitNote-2.1.xsd'>");
                estructura.AppendLine("<ext:UBLExtensions>");
                //Extensión para la firma
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");
                //Configuración
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("	<sts:DianExtensions>");
                estructura.AppendLine("		<sts:InvoiceSource>");
                estructura.AppendLine("			<cbc:IdentificationCode listAgencyID='6' listAgencyName='United Nations Economic Commission for Europe' listSchemeURI='urn:oasis:names:specification:ubl:codelist:gc:CountryIdentificationCode-2.1'>CO</cbc:IdentificationCode>");
                estructura.AppendLine("		</sts:InvoiceSource>");
                estructura.AppendLine("		<sts:SoftwareProvider>");
                estructura.AppendLine("			<sts:ProviderID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Direccion de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.IdProveedor + "</sts:ProviderID>");
                estructura.AppendLine("			<sts:SoftwareID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.IdSoftware + "</sts:SoftwareID>");
                estructura.AppendLine("		</sts:SoftwareProvider>");
                estructura.AppendLine("		<sts:SoftwareSecurityCode schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.SHA384(this.IdSoftware + this.Pin + documento.Resolucion + documento.Id) + "</sts:SoftwareSecurityCode>");
                estructura.AppendLine("		<sts:AuthorizationProvider>");
                estructura.AppendLine("			<sts:AuthorizationProviderID schemeAgencyID='195' schemeID='4' schemeName='31' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>800197268</sts:AuthorizationProviderID>");
                estructura.AppendLine("		</sts:AuthorizationProvider>");
                estructura.AppendLine("		<sts:QRCode>" + "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + documento.CUDE + "</sts:QRCode>");
                //estructura.AppendLine("		<sts:QRCode> NroFactura=" + documento.Resolucion + documento.Id);
                //estructura.AppendLine("			NitFacturador=" + facturador);
                //estructura.AppendLine("			NitAdquiriente=" + cliente);
                //estructura.AppendLine("			FechaFactura=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy"));
                //estructura.AppendLine("			ValorTotalFactura=" + totalFactura.ToString().Replace(',', '.'));
                //estructura.AppendLine("			CUFE=" + documento.CUFE);
                //estructura.AppendLine("			URL=https://catalogo-vpfe-hab.dian.gov.co/Document/FindDocument?documentKey=" + documento.CUFE + "&amp;partitionKey=co|06|94&amp;emissionDate=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy") + "</sts:QRCode>");
                estructura.AppendLine("	</sts:DianExtensions>");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");

                estructura.AppendLine("</ext:UBLExtensions>");
            }

            if (tipoDocumento == "C")
            {
                //Encabezado
                estructura.AppendLine("<?xml version='1.0' encoding='UTF-8' standalone='no'?>");
                estructura.AppendLine("<CreditNote xmlns='urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2' xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' xmlns:sts='http://www.dian.gov.co/contratos/facturaelectronica/v1/Structures' xmlns:xades='http://uri.etsi.org/01903/v1.3.2#' xmlns:xades141='http://uri.etsi.org/01903/v1.4.1#' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2    http://docs.oasis-open.org/ubl/os-UBL-2.1/xsd/maindoc/UBL-CreditNote-2.1.xsd'>");       // + Environment.NewLine +
                estructura.AppendLine("<ext:UBLExtensions>");
                //Extensión para la firma
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");
                //Configuración
                estructura.AppendLine("<ext:UBLExtension>");
                estructura.AppendLine("<ext:ExtensionContent>");
                estructura.AppendLine("	<sts:DianExtensions>");
                estructura.AppendLine("		<sts:InvoiceSource>");
                estructura.AppendLine("			<cbc:IdentificationCode listAgencyID='6' listAgencyName='United Nations Economic Commission for Europe' listSchemeURI='urn:oasis:names:specification:ubl:codelist:gc:CountryIdentificationCode-2.1'>CO</cbc:IdentificationCode>");
                estructura.AppendLine("		</sts:InvoiceSource>");
                estructura.AppendLine("		<sts:SoftwareProvider>");
                estructura.AppendLine("			<sts:ProviderID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Direccion de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.IdProveedor + "</sts:ProviderID>");
                estructura.AppendLine("			<sts:SoftwareID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.IdSoftware + "</sts:SoftwareID>");
                estructura.AppendLine("		</sts:SoftwareProvider>");
                estructura.AppendLine("		<sts:SoftwareSecurityCode schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>" + "" + this.SHA384(this.IdSoftware + this.Pin + documento.Resolucion + documento.Id) + "</sts:SoftwareSecurityCode>");
                estructura.AppendLine("		<sts:AuthorizationProvider>");
                estructura.AppendLine("			<sts:AuthorizationProviderID schemeAgencyID='195' schemeID='4' schemeName='31' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)'>800197268</sts:AuthorizationProviderID>");
                estructura.AppendLine("		</sts:AuthorizationProvider>");
                estructura.AppendLine("		<sts:QRCode>" + "https://catalogo-vpfe.dian.gov.co/document/searchqr?documentkey=" + documento.CUDE + "</sts:QRCode>");
                //estructura.AppendLine("		<sts:QRCode> NroFactura=" + documento.Resolucion + documento.Id);
                //estructura.AppendLine("			NitFacturador=" + facturador);
                //estructura.AppendLine("			NitAdquiriente=" + cliente);
                //estructura.AppendLine("			FechaFactura=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy"));
                //estructura.AppendLine("			ValorTotalFactura=" + totalFactura.ToString().Replace(',', '.'));
                //estructura.AppendLine("			CUFE=" + documento.CUFE);
                //estructura.AppendLine("			URL=https://catalogo-vpfe-hab.dian.gov.co/Document/FindDocument?documentKey=" + documento.CUFE + "&amp;partitionKey=co|06|94&amp;emissionDate=" + Convert.ToDateTime(documento.Fechafac).ToString("dd-MM-yyyy") + "</sts:QRCode>");
                estructura.AppendLine("	</sts:DianExtensions>");
                estructura.AppendLine("</ext:ExtensionContent>");
                estructura.AppendLine("</ext:UBLExtension>");

                estructura.AppendLine("</ext:UBLExtensions>");
            }
            return estructura.ToString();
        }
    }
}
