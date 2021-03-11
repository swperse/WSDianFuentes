using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Modelo
{
    class Tercero
    {
        String _Identificacion;
        String _Nombre;
        String _NombreS;
        String _Apellido;
        String _Direccion;
        String _Pais;
        String _PaisCodAlfa;
        String _Departamento;
        String _Municipio;
        String _Ciudad;
        String _Clase;
        String _CodigoCiudad;
        String _CodigoDepto;
        String _Email;
        String _Telefono;
        String _DV;
        String _Regimen;
        String _ResFiscal;
        bool _Exterior;
        String _Tipopersona;

        public String Identificacion
        {
            get
            {
                return _Identificacion;
            }

            set
            {
                _Identificacion = value;
            }
        }

        public String Nombre
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

        public String Apellido
        {
            get
            {
                return _Apellido;
            }

            set
            {
                _Apellido = value;
            }
        }

        public String Direccion
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

        public String NombreS
        {
            get
            {
                return _NombreS;
            }

            set
            {
                _NombreS = value;
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

        public string Municipio
        {
            get
            {
                return _Municipio;
            }

            set
            {
                _Municipio = value;
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

        public bool Exterior
        {
            get
            {
                return _Exterior;
            }

            set
            {
                _Exterior = value;
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

        public String toXML()
        {
            string documentType = !string.IsNullOrEmpty(this.DV) ? 31.ToString() : this.Clase.ToString();

            // String estructura = "";
            StringBuilder estructura = new StringBuilder();
            estructura.AppendLine("<cac:AccountingCustomerParty>");          //  + Environment.NewLine +
            estructura.AppendLine("	<cbc:AdditionalAccountID>" + this.Tipopersona  + "</cbc:AdditionalAccountID>");          //  + Environment.NewLine +
            if (Tipopersona == "2")
            {
                estructura.AppendLine(" <cac:Party>");
                estructura.AppendLine("     <cac:PartyIdentification>");
                estructura.AppendLine("         <cbc:ID schemeName = '13'>222222222222</cbc:ID>");
                estructura.AppendLine("     </cac:PartyIdentification>");
                estructura.AppendLine("     <cac:PartyTaxScheme>");
                estructura.AppendLine("         <cbc:RegistrationName>consumidor final</cbc:RegistrationName>");
                estructura.AppendLine("         <cbc:CompanyID schemeAgencyID ='195' schemeAgencyName ='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeName ='13'>222222222222</cbc:CompanyID>");
                estructura.AppendLine("         <cbc:TaxLevelCode listName='" + this.Regimen + "'>" + this.ResFiscal + "</cbc:TaxLevelCode>");
                estructura.AppendLine("         <cac:TaxScheme>");
                estructura.AppendLine("             <cbc:ID >ZY</cbc:ID>");
                estructura.AppendLine("             <cbc:Name >No causa</cbc:Name>");
                estructura.AppendLine("         </cac:TaxScheme>");
                estructura.AppendLine("     </cac:PartyTaxScheme>");
                estructura.AppendLine("</cac:Party>");
            }
            else
            {
                estructura.AppendLine("	<cac:Party>");
                estructura.AppendLine("		<cac:PartyName>");          //  + Environment.NewLine +
                estructura.AppendLine("			<cbc:Name>" + this.Nombre.Replace("&", "&amp;") + "</cbc:Name>");          //  + Environment.NewLine +
                estructura.AppendLine("		</cac:PartyName>");          //  + Environment.NewLine +
                if (!this.Exterior)
                {
                    estructura.AppendLine("	<cac:PhysicalLocation>");          //  + Environment.NewLine +
                    estructura.AppendLine("		<cac:Address>");          //  + Environment.NewLine +
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
                    estructura.AppendLine("		</cac:Address>");          //  + Environment.NewLine +
                    estructura.AppendLine("	</cac:PhysicalLocation>");          //  + Environment.NewLine +
                }
                estructura.AppendLine("	<cac:PartyTaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("				<cbc:RegistrationName>" + this.Nombre.Replace("&", "&amp;") + "</cbc:RegistrationName>");
                estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.Identificacion + "</cbc:CompanyID>");
                estructura.AppendLine("		<cbc:TaxLevelCode listName='" + this.Regimen + "'>" + this.ResFiscal + "</cbc:TaxLevelCode>");          //  + Environment.NewLine +
                estructura.AppendLine("			<cac:RegistrationAddress>");          //  + Environment.NewLine +
                estructura.AppendLine("				<cbc:ID>" + this.CodigoCiudad + "</cbc:ID>");
                estructura.AppendLine("				<cbc:CityName>" + this.Ciudad + "</cbc:CityName>");   // + Environment.NewLine +
                estructura.AppendLine("				<cbc:CountrySubentity>" + this.Departamento + "</cbc:CountrySubentity>");
                estructura.AppendLine("				<cbc:CountrySubentityCode>" + this.CodigoDepto + "</cbc:CountrySubentityCode>");
                //estructura.AppendLine("				<cbc:PostalZone>000111</cbc:PostalZone>");
                estructura.AppendLine("				<cac:AddressLine>");          //  + Environment.NewLine +
                estructura.AppendLine("					<cbc:Line>" + this.Direccion + "</cbc:Line>");   // + Environment.NewLine +//Confirmar Dirección
                estructura.AppendLine("				</cac:AddressLine>");          //  + Environment.NewLine +
                estructura.AppendLine("				<cac:Country>");          //  + Environment.NewLine +
                estructura.AppendLine("					<cbc:IdentificationCode>" + this.PaisCodAlfa + "</cbc:IdentificationCode>");          //  + Environment.NewLine +//Validar PAIS
                estructura.AppendLine("					<cbc:Name languageID='es'>" + this.Pais + "</cbc:Name>");
                estructura.AppendLine("				</cac:Country>");          //  + Environment.NewLine +
                estructura.AppendLine("			</cac:RegistrationAddress>");          //  + Environment.NewLine +
                estructura.AppendLine("		    <cac:TaxScheme>");
                //estructura.AppendLine("		        <cbc:ID>ZZ</cbc:ID>");
                //estructura.AppendLine("		        <cbc:Name>No aplica</cbc:Name>");
                estructura.AppendLine("		        <cbc:ID>01</cbc:ID>");
                estructura.AppendLine("		        <cbc:Name>IVA</cbc:Name>");
                estructura.AppendLine("		    </cac:TaxScheme>");
                estructura.AppendLine("	</cac:PartyTaxScheme>");          //  + Environment.NewLine +
                estructura.AppendLine("		<cac:PartyLegalEntity>");          //  + Environment.NewLine +
                estructura.AppendLine("			<cbc:RegistrationName>" + "PJ - " + this.Identificacion + " - " + this.Nombre.Replace("&", "&amp;") + "</cbc:RegistrationName>");          //  + Environment.NewLine +
                estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeAgencyName='CO, DIAN (Dirección de Impuestos y Aduanas Nacionales)' schemeID='" + this.DV + "' schemeName='" + documentType + "'>" + this.Identificacion + " </cbc:CompanyID>");
                estructura.AppendLine("		</cac:PartyLegalEntity>");
                estructura.AppendLine("	<cac:Contact>");          //  + Environment.NewLine +
                estructura.AppendLine("		<cbc:Name>" + this.Nombre.Replace("&", "&amp;") + "</cbc:Name>");
                estructura.AppendLine("		<cbc:Telephone>" + this.Telefono + "</cbc:Telephone>");
                estructura.AppendLine("		<cbc:ElectronicMail>" + this.Email + "</cbc:ElectronicMail>");        //  + Environment.NewLine +
                estructura.AppendLine("	</cac:Contact>");
                estructura.AppendLine("	</cac:Party>");          //  + Environment.NewLine +
            }
            estructura.AppendLine("</cac:AccountingCustomerParty>");

            return estructura.ToString();
        }
    }
}
