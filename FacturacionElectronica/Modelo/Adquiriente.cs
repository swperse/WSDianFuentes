using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.DAO
{
    class Adquiriente
    {
        String _NombreUsuario;
        String _Nit;
        String _Direccion;
        String _Pais;
        String _PaisCodAlfa;
        String _Departamento;
        String _CodigoPostal;
        String _CodigoCiudad;
        String _CodigoDepto;
        String _Ciudad;
        String _Clase;
        String _Email;
        String _Telefono;
        String _DV;
        String _Regimen;
        String _ResFiscal;
        String _Tipopersona;

        public string NombreUsuario
        {
            get
            {
                return _NombreUsuario;
            }

            set
            {
                _NombreUsuario = value;
            }
        }

        public string Nit
        {
            get
            {
                return _Nit;
            }

            set
            {
                _Nit = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public string Ciudad
        {
            get
            {
                return _Ciudad;
            }

            set
            {
                _Ciudad = value;
            }
        }

        public string Clase
        {
            get
            {
                return _Clase;
            }

            set
            {
                _Clase = value;
            }
        }
        public string CodigoCiudad
        {
            get
            {
                return _CodigoCiudad;
            }

            set
            {
                _CodigoCiudad = value;
            }
        }

        public string CodigoDepto
        {
            get
            {
                return _CodigoDepto;
            }

            set
            {
                _CodigoDepto = value;
            }
        }
        public string CodigoPostal
        {
            get
            {
                return _CodigoPostal;
            }

            set
            {
                _CodigoPostal = value;
            }
        }

        public string Departamento
        {
            get
            {
                return _Departamento;
            }

            set
            {
                _Departamento = value;
            }
        }

        public string Pais
        {
            get
            {
                return _Pais;
            }

            set
            {
                _Pais = value;
            }
        }
        public string PaisCodAlfa
        {
            get
            {
                return _PaisCodAlfa;
            }

            set
            {
                _PaisCodAlfa = value;
            }
        }
        public string Email
        {
            get
            {
                return _Email;
            }

            set
            {
                _Email = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
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
        public string Regimen
        {
            get
            {
                return _Regimen;
            }

            set
            {
                _Regimen = value;
            }
        }
        public string ResFiscal
        {
            get
            {
                return _ResFiscal;
            }

            set
            {
                _ResFiscal = value;
            }
        }

        public string Tipopersona
        {
            get
            {
                return _Tipopersona;
            }

            set
            {
                _Tipopersona = value;
            }
        }

        public String toXML(string prefix)
        {
            var documentType = string.IsNullOrEmpty(this.DV) == false ? 31 : 13;

            // String estructura = "";
            StringBuilder estructura = new StringBuilder();
            estructura.AppendLine("<cac:AccountingSupplierParty>");          //  + Environment.NewLine +
            estructura.AppendLine("<cbc:AdditionalAccountID>" + this.Tipopersona + "</cbc:AdditionalAccountID>");   // + Environment.NewLine +
            estructura.AppendLine("	<cac:Party>");
            estructura.AppendLine("		<cac:PartyName>");          //  + Environment.NewLine +
            estructura.AppendLine("			<cbc:Name>" + this.NombreUsuario.Replace("&", "&amp;") + "</cbc:Name>");          //  + Environment.NewLine +
            estructura.AppendLine("		</cac:PartyName>");          //  + Environment.NewLine +
            estructura.AppendLine("		<cac:PhysicalLocation>");          //  + Environment.NewLine +
            estructura.AppendLine("			<cac:Address>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cbc:ID>" + this.CodigoCiudad + "</cbc:ID>");
            estructura.AppendLine("				<cbc:CityName>" + this.Ciudad + "</cbc:CityName>");   // + Environment.NewLine +
            estructura.AppendLine("				<cbc:CountrySubentity>" + this.Departamento + "</cbc:CountrySubentity>");
            estructura.AppendLine("				<cbc:CountrySubentityCode>" + this.CodigoDepto + "</cbc:CountrySubentityCode>");
            estructura.AppendLine("				<cac:AddressLine>");          //  + Environment.NewLine +
            estructura.AppendLine("					<cbc:Line>" + this.Direccion + "</cbc:Line>");   // + Environment.NewLine +//Confirmar Dirección
            estructura.AppendLine("				</cac:AddressLine>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cac:Country>");          //  + Environment.NewLine +
            estructura.AppendLine("					<cbc:IdentificationCode>" + this.PaisCodAlfa + "</cbc:IdentificationCode>");          //  + Environment.NewLine +//Validar PAIS
            estructura.AppendLine("					<cbc:Name languageID='es'>" + this.Pais + "</cbc:Name>");
            estructura.AppendLine("				</cac:Country>");          //  + Environment.NewLine +
            estructura.AppendLine("			</cac:Address>");          //  + Environment.NewLine +
            estructura.AppendLine("		</cac:PhysicalLocation>");          //  + Environment.NewLine +
            estructura.AppendLine("		<cac:PartyTaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("			<cbc:RegistrationName>" + this.NombreUsuario.Replace("&", "&amp;") + "</cbc:RegistrationName>");
            estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.Nit + "</cbc:CompanyID>");
            estructura.AppendLine("			<cbc:TaxLevelCode listName='" + this.Regimen + "'>" + this.ResFiscal + "</cbc:TaxLevelCode>");          //  + Environment.NewLine +//Que es el TaxLevelCode
            estructura.AppendLine("			<cac:RegistrationAddress>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cbc:ID>" + this.CodigoCiudad + "</cbc:ID>");
            estructura.AppendLine("				<cbc:CityName>" + this.Ciudad + "</cbc:CityName>");   // + Environment.NewLine +
            estructura.AppendLine("				<cbc:PostalZone>" + this.CodigoPostal + "</cbc:PostalZone>");
            estructura.AppendLine("				<cbc:CountrySubentity>" + this.Departamento + "</cbc:CountrySubentity>");
            estructura.AppendLine("				<cbc:CountrySubentityCode>" + this.CodigoDepto + "</cbc:CountrySubentityCode>");
            estructura.AppendLine("			    <cac:AddressLine>");          //  + Environment.NewLine +
            estructura.AppendLine("					<cbc:Line>" + this.Direccion + "</cbc:Line>");   // + Environment.NewLine +//Confirmar Dirección
            estructura.AppendLine("				</cac:AddressLine>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cac:Country>");          //  + Environment.NewLine +
            estructura.AppendLine("					<cbc:IdentificationCode>" + this.PaisCodAlfa + "</cbc:IdentificationCode>");          //  + Environment.NewLine +//Validar PAIS
            estructura.AppendLine("					<cbc:Name languageID='es'>" + this.Pais + "</cbc:Name>");
            estructura.AppendLine("				</cac:Country>");          //  + Environment.NewLine +
            estructura.AppendLine("			</cac:RegistrationAddress>");          //  + Environment.NewLine +
            estructura.AppendLine("			<cac:TaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cbc:ID>01</cbc:ID>");          //  + Environment.NewLine +
            estructura.AppendLine("				<cbc:Name>IVA</cbc:Name>");          //  + Environment.NewLine +
            estructura.AppendLine("			</cac:TaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("		</cac:PartyTaxScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("		<cac:PartyLegalEntity>");          //  + Environment.NewLine +            
            estructura.AppendLine("			<cbc:RegistrationName>" + "PJ - " + this.Nit + " - " + this.NombreUsuario.Replace("&", "&amp;") + "</cbc:RegistrationName>");          //  + Environment.NewLine +
            estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.Nit + "</cbc:CompanyID>");
            estructura.AppendLine("	        <cac:CorporateRegistrationScheme>");          //  + Environment.NewLine +
            estructura.AppendLine("		        <cbc:ID>" + prefix + "</cbc:ID>");
            estructura.AppendLine("		        <cbc:Name>10181</cbc:Name>");
            estructura.AppendLine("	        </cac:CorporateRegistrationScheme>");
            estructura.AppendLine("		</cac:PartyLegalEntity>");          //  + Environment.NewLine +
            estructura.AppendLine("	    <cac:Contact>");          //  + Environment.NewLine +
            estructura.AppendLine("		    <cbc:Name>" + this.NombreUsuario.Replace("&", "&amp;") + "</cbc:Name>");
            estructura.AppendLine("		    <cbc:Telephone>" + this.Telefono + "</cbc:Telephone>");
            estructura.AppendLine("		    <cbc:ElectronicMail>" + this.Email + "</cbc:ElectronicMail>");        //  + Environment.NewLine +
            estructura.AppendLine("	    </cac:Contact>");
            estructura.AppendLine("	</cac:Party>");          //  + Environment.NewLine +
            estructura.AppendLine("</cac:AccountingSupplierParty>");

            return estructura.ToString();
        }

        public String taxRepresentativePartyToXML()
        {
            var documentType = string.IsNullOrEmpty(this.DV) == false ? 31 : 13;

            StringBuilder estructura = new StringBuilder();
            estructura.AppendLine("<cac:TaxRepresentativeParty>");          //  + Environment.NewLine +
            estructura.AppendLine(" <cac:PartyIdentification>");   // + Environment.NewLine +
            estructura.AppendLine("     <cbc:ID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.Nit + "</cbc:ID>");
            estructura.AppendLine("	</cac:PartyIdentification>");          //  + Environment.NewLine +
            estructura.AppendLine(" <cac:PartyName>");          //  + Environment.NewLine +
            estructura.AppendLine("		<cbc:Name/>");          //  + Environment.NewLine +
            estructura.AppendLine("	</cac:PartyName>");
            estructura.AppendLine("</cac:TaxRepresentativeParty>");

            return estructura.ToString();
        }
    }
}
