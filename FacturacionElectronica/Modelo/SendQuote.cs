using FacturacionElectronica.DAO;
using FacturacionElectronica.ServicioFacturacionDian;
using System;
using System.IO;
using System.Windows.Forms;

using Microsoft.Web.Services3.Security.Tokens;
using Microsoft.Web.Services3.Security.Utility;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.Web.Services.Protocols;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using FacturacionElectronica.Controlador;
//using FE.ConsultaWS;
using System.ServiceModel.Security;
using FacturacionElectronica.ClasesWSE;
using System.Runtime.Serialization.Formatters;
using System.ServiceModel.Security.Tokens;
using FacturacionElectronica.Conexion;
using System.Threading;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace FacturacionElectronica.Modelo
{
    class SendQuote
    {
        readonly string currentDir = AppDomain.CurrentDomain.BaseDirectory;


        public void ConsultarEstado(string NroFactura, string tipoFactura, string resolucion)
        {
            ConfiguracionDIAN miConfiguracion = new ConfiguracionDIAN();
            ConfiguracionDIANDAO miConfiguracionDAO = new ConfiguracionDIANDAO();

            var titulo = "";

            if (tipoFactura == "F") titulo = "Factura";
            else if (tipoFactura == "D") titulo = "Nota debito";
            else titulo = "Nota crédito";

            Alerta alerta = new Alerta();
            alerta.Mostrar("Facturacion Dian", "Consultando estado de la " + titulo + " " + NroFactura, 4000);

            FacturasDAO facDAO = new FacturasDAO();
            Factura miFactura = new Factura();

            if (tipoFactura == "F")
            {
                titulo = "Factura";
                miFactura = facDAO.selectManDetalles(NroFactura, tipoFactura, resolucion);
            }
            else if (tipoFactura == "D")
            {
                titulo = "Nota debito";
                miFactura = facDAO.selectManDetallesDebito(NroFactura, tipoFactura, resolucion);
            }
            else
            {
                titulo = "Nota crédito";
                miFactura = facDAO.selectManDetallesCredito(NroFactura, tipoFactura, resolucion);
            }

            miConfiguracion = miConfiguracionDAO.getConfiguracion(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion);
            try
            {
                Configuracion Config;
                Config = Configuracion.Instancia;

                var urlDian = new ConfiguracionDIANDAO().ObtenerUrlDian(Config.Firma);
                if (urlDian == "")
                {
                    alerta.Cerrar();
                    MessageBox.Show("La url de la Dian no está configurada", "Consultando " + titulo,
                      MessageBoxButtons.OK, MessageBoxIcon.None);

                    return;
                }

                X509Certificate2 certificado = null;
                var certificadoRuta = currentDir + @"\" + Config.NombreCertificado;
                try
                {
                    certificado = new X509Certificate2(certificadoRuta, Config.ContraseñaCertificado);
                }
                catch (Exception ex)
                {
                    //throw new Exception("No fue posible abrir el archivo " + Config.NombreCertificado + " con la contraseña " + Config.ContraseñaCertificado);
                    alerta.Cerrar();
                    MessageBox.Show("Ocurrió un error consultando el estado de la " + titulo + ": " + ("No fué posible abrir el archivo " + Config.NombreCertificado + " con la contraseña " + Config.ContraseñaCertificado), "Consultando " + titulo,
                           MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                WebRequestHandler handler = new WebRequestHandler();
                handler.ClientCertificates.Add(certificado);

                var clientService = new ServicioFacturacionDian.WcfDianCustomerServicesClient("WSHttpBinding_IWcfDianCustomerServices");
                clientService.Endpoint.Address = new EndpointAddress(urlDian);

                clientService.ClientCredentials.ClientCertificate.Certificate = certificado;

                //Enviar set de pruebas cuando tipoAmbiente es 2
                if (miConfiguracion.TipoAmbiente == "2")
                {
                    if (miFactura.RespuestaDian == "")
                    {
                        MessageBox.Show("El documento no obtuvo respuesta de la Dian", "Consultando " + titulo + " " + NroFactura,
                                            MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                    var respuestaDianFactura = JsonConvert.DeserializeObject<UploadDocumentResponse>(miFactura.RespuestaDian.Replace("$_$", ":").Replace("#_#", " - ").Replace("_", "-"));

                    var respuestaDian = new DianResponse();

                    var respuestaDianArr = clientService.GetStatusZip(respuestaDianFactura.ZipKey);

                    if (respuestaDianArr.Length > 0)
                    {
                        respuestaDian = respuestaDianArr[0];
                    }

                    alerta.Cerrar();
                    var mensaje = respuestaDian.ErrorMessage != null && respuestaDian.ErrorMessage.Length > 0 ? string.Join("\n", respuestaDian.ErrorMessage) : "";
                    mensaje += !string.IsNullOrEmpty(respuestaDian.StatusMessage) ? "\n" + respuestaDian.StatusMessage : "";
                    mensaje += !string.IsNullOrEmpty(respuestaDian.StatusDescription) ? "\n" + respuestaDian.StatusDescription : "";

                    if (mensaje == "")
                    {
                        mensaje = "No se obtuvo respuesta";
                    }

                    MessageBox.Show("Respuesta envío set de pruebas:\n " + mensaje, "Consultando " + titulo + " " + NroFactura,
                    MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception ex)
            {
                alerta.Cerrar();
                MessageBox.Show("Ocurrió un error consultado el estado de la " + titulo + ": " + ex.Message + ": " + ex.InnerException, "Consultando " + titulo,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool EnvioFacturaElectronicaSOAP(string NombreArchivo, string NroFactura, string nit, string tipoFactura, string resolucion)
        {
            ConfiguracionDIAN miConfiguracion = new ConfiguracionDIAN();
            ConfiguracionDIANDAO miConfiguracionDAO = new ConfiguracionDIANDAO();

            var titulo = "";

            if (tipoFactura == "F") titulo = "Factura";
            else if (tipoFactura == "D") titulo = "Nota debito";
            else titulo = "Nota crédito";

            Alerta alerta = new Alerta();
            alerta.Mostrar("Facturacion Dian", "Enviando " + titulo + " " + NroFactura + " a la DIAN", 4000);
            FacturasDAO facDAO = new FacturasDAO();
            Factura miFactura = new Factura();

            var directorio = "";

            if (tipoFactura == "F")
            {
                directorio = @"\Facturas\";
                titulo = "Factura";
                miFactura = facDAO.selectManDetalles(NroFactura, tipoFactura, resolucion);
            }
            else if (tipoFactura == "D")
            {
                directorio = @"\Debitos\";
                titulo = "Nota debito";
                miFactura = facDAO.selectManDetallesDebito(NroFactura, tipoFactura, resolucion);
            }
            else
            {
                directorio = @"\Creditos\";
                titulo = "Nota crédito";
                miFactura = facDAO.selectManDetallesCredito(NroFactura, tipoFactura, resolucion);
            }

            miConfiguracion = miConfiguracionDAO.getConfiguracion(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion);
            try
            {
                Configuracion Config;
                Config = Configuracion.Instancia;

                var urlDian = new ConfiguracionDIANDAO().ObtenerUrlDian(Config.Firma);
                if (urlDian == "")
                {
                    alerta.Cerrar();
                    MessageBox.Show("La url de la Dian no está configurada", "Enviando " + titulo,
                      MessageBoxButtons.OK, MessageBoxIcon.None);

                    return false;
                }

                X509Certificate2 certificado = null;
                var certificadoRuta = currentDir + @"\" + Config.NombreCertificado;
                try
                {
                    certificado = new X509Certificate2(certificadoRuta, Config.ContraseñaCertificado);
                }
                catch (Exception ex)
                {
                    //throw new Exception("No fue posible abrir el archivo " + Config.NombreCertificado + " con la contraseña " + Config.ContraseñaCertificado);
                    alerta.Cerrar();
                    MessageBox.Show("Ocurrió un error enviando la " + titulo + " a la DIAN: " + ("No fué posible abrir el archivo " + Config.NombreCertificado + " con la contraseña " + Config.ContraseñaCertificado), "Enviando " + titulo,
                           MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                WebRequestHandler handler = new WebRequestHandler();
                handler.ClientCertificates.Add(certificado);

                var clientService = new ServicioFacturacionDian.WcfDianCustomerServicesClient("WSHttpBinding_IWcfDianCustomerServices");
                clientService.Endpoint.Address = new EndpointAddress(urlDian);

                //clientService.ClientCredentials.ClientCertificate.SetCertificate(
                //    StoreLocation.LocalMachine,
                //    StoreName.My,
                //    X509FindType.FindBySubjectName,
                //    Config.NombreEmpresa);

                clientService.ClientCredentials.ClientCertificate.Certificate = certificado;

                //Enviar set de pruebas cuando tipoAmbiente es 2
                if (miConfiguracion.TipoAmbiente == "2")
                {
                    if (string.IsNullOrEmpty(Config.UserDian))
                    {
                        MessageBox.Show("Error al enviar la " + titulo + " " + NroFactura + " al set de pruebas\nRazón: El campo UserDian no puede ser vacio en el archivo de configuración", "Enviando " + titulo + " (Set de pruebas)",
                                         MessageBoxButtons.OK, MessageBoxIcon.Hand);

                        return false;
                    }

                    var respuestaDianPruebas = clientService.SendTestSetAsync("z" + NombreArchivo, File.ReadAllBytes(currentDir + directorio + "z" + NombreArchivo + ".zip"), Config.UserDian);

                    string respuestaString = new JavaScriptSerializer().Serialize(respuestaDianPruebas);
                    //if (tipoFactura == "F")
                    //{
                    //    facDAO.ActualizarRespuestaSetPruebas(miFactura.Facturanumero, tipoFactura, respuestaString);
                    //}
                    //else
                    //{
                    //    facDAO.ActualizarRespuestaSetPruebas(miFactura.Id, tipoFactura, respuestaString);
                    //}
                    if (tipoFactura == "F")
                    {
                        facDAO.ActualizarEstadoFactura(miFactura.Facturanumero, tipoFactura, NombreArchivo, respuestaString, 3, resolucion);
                    }
                    else
                    {
                        facDAO.ActualizarEstadoFactura(miFactura.Id, tipoFactura, NombreArchivo, respuestaString, 3, resolucion);
                    }

                    var mensaje = "";


                    if (respuestaDianPruebas.ErrorMessageList != null && respuestaDianPruebas.ErrorMessageList.Length > 0)
                    {
                        foreach (var r in respuestaDianPruebas.ErrorMessageList)
                        {
                            mensaje += r.ProcessedMessage != "" ? r.ProcessedMessage + "\n" : "";
                        }
                    }

                    if (mensaje == "")
                    {
                        mensaje = "Documento enviado al set de pruebas";
                    }
                    else
                    {
                        mensaje = "Respuesta set de pruebas " + titulo + " " + NroFactura + "\n" + mensaje;
                    }


                    MessageBox.Show(mensaje, "Enviando " + titulo + " " + NroFactura + " al set de pruebas",
                            MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    //alerta.Cerrar();
                    var respuestaDian = clientService.SendBillSync("z" + NombreArchivo, File.ReadAllBytes(currentDir + directorio + "z" + NombreArchivo + ".zip"));

                    var allResponseJson = new JavaScriptSerializer().Serialize(respuestaDian);
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(allResponseJson);
                    var allResponseBase64 = System.Convert.ToBase64String(plainTextBytes);

                    alerta.Cerrar();
                    var tmpMensaje = respuestaDian.ErrorMessage != null && respuestaDian.ErrorMessage.Length > 0 ? string.Join(", ", respuestaDian.ErrorMessage) : "";
                    if (respuestaDian.StatusCode == "00" || tmpMensaje.ToLower().Replace(" ", "").Contains("procesadoanteriormente"))
                    {
                        var mensaje = respuestaDian.ErrorMessage != null && respuestaDian.ErrorMessage.Length > 0 ? string.Join("\n", respuestaDian.ErrorMessage) : "";
                        mensaje += !string.IsNullOrEmpty(respuestaDian.StatusMessage) ? "\n" + respuestaDian.StatusMessage : "";
                        mensaje += !string.IsNullOrEmpty(respuestaDian.StatusDescription) ? "\n" + respuestaDian.StatusDescription : "";

                        if (mensaje == "")
                        {
                            mensaje = tipoFactura + " " + NroFactura + " en proceso de verificación";
                        }

                        var responseUpdate = false;

                        if (tipoFactura == "F")
                        {
                            responseUpdate = facDAO.ActualizarEstadoFactura(miFactura.Facturanumero, tipoFactura, NombreArchivo, allResponseBase64, 3, resolucion);
                        }
                        else
                        {
                            responseUpdate = facDAO.ActualizarEstadoFactura(miFactura.Id, tipoFactura, NombreArchivo, allResponseBase64, 3, resolucion);
                        }

                        if (responseUpdate)
                        {
                            var adName = "ad" + NombreArchivo + ".xml";
                            //leer del archivo de la factura xml
                            var xmlFactura = "";
                            if (tipoFactura == "F")
                            {
                                NombreArchivo = "fv" + NombreArchivo;
                            }
                            else if (tipoFactura == "D")
                            {
                                NombreArchivo = "nd" + NombreArchivo;
                            }
                            else
                            {
                                NombreArchivo = "nc" + NombreArchivo;
                            }
                            xmlFactura = File.ReadAllText(currentDir + directorio + NombreArchivo + ".xml");

                            //Datos adquiriente y cliente
                            var miAdquiriente = facDAO.InfoAdquiriente();
                            var cliente = facDAO.InfoTercero(miFactura.Cliente);

                            //xml application Response
                            string base64StringAppResponse = Convert.ToBase64String(respuestaDian.XmlBase64Bytes, 0, respuestaDian.XmlBase64Bytes.Length);
                            var base64EncodedBytesAppResponse = System.Convert.FromBase64String(base64StringAppResponse);
                            var xmlAppResponse = System.Text.Encoding.UTF8.GetString(base64EncodedBytesAppResponse);


                            //Armar el xml del attachedDocument y guardarlo
                            var allPathAd = currentDir + directorio + adName;
                            var xmlAd = getADXml(xmlFactura, xmlAppResponse, tipoFactura, miFactura, miAdquiriente, cliente, NombreArchivo, resolucion);
                            File.WriteAllText(allPathAd, xmlAd);

                            MessageBox.Show("Documento enviado exitosamente\nRespuesta: " + mensaje, "Enviando " + titulo,
                            MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }
                    else
                    {
                        var mensaje = "";

                        if (respuestaDian.ErrorMessage != null)
                        {
                            mensaje = string.Join("\n", respuestaDian.ErrorMessage);
                            if (mensaje != "" && mensaje != "\n")
                            {
                                MessageBox.Show("Error al enviar la " + titulo + " " + NroFactura + " a la DIAN\nRazón: " + mensaje, "Enviando " + titulo,
                                             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }

                        if (!string.IsNullOrEmpty(respuestaDian.StatusDescription) && (mensaje == "" || mensaje == "\n"))
                        {
                            mensaje = respuestaDian.StatusDescription;
                            MessageBox.Show("Error al enviar la " + titulo + " " + NroFactura + " a la DIAN\nRazón: " + mensaje, "Enviando " + titulo,
                                             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }

                        if (mensaje == "" || mensaje == "\n")
                        {
                            mensaje = "No fué posible obtener respuesta";

                            MessageBox.Show("Error al enviar la " + titulo + " " + NroFactura + " a la DIAN\nRazón: No fué posible obtener respuesta", "Enviando " + titulo,
                                             MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }

                        if (tipoFactura == "F")
                        {
                            facDAO.ActualizarEstadoFactura(miFactura.Facturanumero, tipoFactura, "", allResponseBase64, 0, resolucion);
                        }
                        else
                        {
                            facDAO.ActualizarEstadoFactura(miFactura.Id, tipoFactura, "", allResponseBase64, 0, resolucion);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                alerta.Cerrar();
                MessageBox.Show("Ocurrió un error enviando la " + titulo + " a la DIAN: " + ex.Message + ": " + ex.InnerException, "Enviando " + titulo,
                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public string getADXml(string xmlFactura, string xmlResponse, string tipoFactura, Factura factura, Adquiriente adquiriente,
            Tercero cliente, string nombreArchivo, string resolucion)
        {
            var tipoDocumentoAdquiriente = string.IsNullOrEmpty(adquiriente.DV) == false ? 31 : 13;
            var tipoDocumentoCliente = string.IsNullOrEmpty(cliente.DV) == false ? 31 : 13;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlResponse);
            XmlNode root = xmlDoc.DocumentElement;

            string fechaRespuesta = "";
            string horaRespuesta = "";

            //Change the price on the books.
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.LocalName == "IssueDate")
                {
                    fechaRespuesta = node.InnerText;
                }
                if (node.LocalName == "IssueTime")
                {
                    horaRespuesta = node.InnerText;
                }
            }

            StringBuilder estructura = new StringBuilder();
            estructura.AppendLine("<?xml version='1.0' encoding='utf-8' standalone='no'?>");
            estructura.AppendLine("<AttachedDocument xmlns='urn:oasis:names:specification:ubl:schema:xsd:AttachedDocument-2' xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' xmlns:ccts='urn:un:unece:uncefact:data:specification:CoreComponentTypeSchemaModule:2' xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' xmlns:xades='http://uri.etsi.org/01903/v1.3.2#' xmlns:xades141='http://uri.etsi.org/01903/v1.4.1#'>");
            estructura.AppendLine("	<cbc:UBLVersionID>UBL 2.1</cbc:UBLVersionID>");
            estructura.AppendLine("	<cbc:CustomizationID>Documentos adjuntos</cbc:CustomizationID>");
            estructura.AppendLine("	<cbc:ProfileID>Factura Electrónica de Venta</cbc:ProfileID>");
            estructura.AppendLine("	<cbc:ProfileExecutionID>1</cbc:ProfileExecutionID>");
            estructura.AppendLine("	<cbc:ID>" + nombreArchivo + "</cbc:ID > ");
            estructura.AppendLine("	<cbc:IssueDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</cbc:IssueDate>");
            estructura.AppendLine("	<cbc:IssueTime>" + DateTime.Now.ToString("HH:mm:ss") + "-05:00</cbc:IssueTime>");
            estructura.AppendLine("	<cbc:DocumentType>Contenedor de Factura Electrónica</cbc:DocumentType>");
            estructura.AppendLine("	<cbc:ParentDocumentID>" + resolucion + factura.Facturanumero + "</cbc:ParentDocumentID>");
            estructura.AppendLine("	<cac:SenderParty>");
            estructura.AppendLine("		<cac:PartyTaxScheme>");
            estructura.AppendLine("			<cbc:RegistrationName>" + adquiriente.NombreUsuario + "</cbc:RegistrationName>");
            estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeID='" + adquiriente.DV + "' schemeName='" + tipoDocumentoAdquiriente + "'>" + adquiriente.Nit + "</cbc:CompanyID>");
            estructura.AppendLine("			<cac:TaxScheme>");
            estructura.AppendLine("				<cbc:ID>01</cbc:ID>");
            estructura.AppendLine("				<cbc:Name>IVA</cbc:Name>");
            estructura.AppendLine("			</cac:TaxScheme>");
            estructura.AppendLine("		</cac:PartyTaxScheme>");
            estructura.AppendLine("	</cac:SenderParty>");
            estructura.AppendLine("	<cac:ReceiverParty>");
            estructura.AppendLine("		<cac:PartyTaxScheme>");
            estructura.AppendLine("			<cbc:RegistrationName>" + cliente.Nombre + "</cbc:RegistrationName>");
            estructura.AppendLine("			<cbc:CompanyID schemeAgencyID='195' schemeID='" + cliente.DV + "' schemeName='" + tipoDocumentoCliente + "'>" + cliente.Identificacion + "</cbc:CompanyID>");
            estructura.AppendLine("			<cac:TaxScheme>");
            estructura.AppendLine("				<cbc:ID>01</cbc:ID>");
            estructura.AppendLine("				<cbc:Name>IVA</cbc:Name>");
            estructura.AppendLine("			</cac:TaxScheme>");
            estructura.AppendLine("		</cac:PartyTaxScheme>");
            estructura.AppendLine("	</cac:ReceiverParty>");
            estructura.AppendLine("	<cac:Attachment>");
            estructura.AppendLine("		<cac:ExternalReference>");
            estructura.AppendLine("			<cbc:MimeCode>text/xml</cbc:MimeCode>");
            estructura.AppendLine("			<cbc:EncodingCode>UTF-8</cbc:EncodingCode>");
            estructura.AppendLine("			<cbc:Description>");
            estructura.AppendLine("				<![CDATA[" + xmlFactura + "]]>");
            estructura.AppendLine("			</cbc:Description>");
            estructura.AppendLine("		</cac:ExternalReference>");
            estructura.AppendLine("	</cac:Attachment>");
            estructura.AppendLine("	<cac:ParentDocumentLineReference>");
            estructura.AppendLine("		<cbc:LineID>1</cbc:LineID>");
            estructura.AppendLine("		<cac:DocumentReference>");
            estructura.AppendLine("			<cbc:ID>" + resolucion + factura.Facturanumero + "</cbc:ID>");
            estructura.AppendLine((tipoFactura == "F" ? "<cbc:UUID schemeName='CUFE-SHA384'>" : "<cbc:UUID schemeName='CUFE-SHA386'>") + factura.CUFE + "</cbc:UUID>");
            estructura.AppendLine("			<cbc:IssueDate>" + fechaRespuesta + "</cbc:IssueDate>");
            estructura.AppendLine("			<cbc:DocumentType>ApplicationResponse</cbc:DocumentType>");
            estructura.AppendLine("			<cac:Attachment>");
            estructura.AppendLine("				<cac:ExternalReference>");
            estructura.AppendLine("					<cbc:MimeCode>text/xml</cbc:MimeCode>");
            estructura.AppendLine("					<cbc:EncodingCode>UTF-8</cbc:EncodingCode>");
            estructura.AppendLine("					<cbc:Description>");
            estructura.AppendLine("						<![CDATA[" + xmlResponse + "]]>");
            estructura.AppendLine("					</cbc:Description>");
            estructura.AppendLine("				</cac:ExternalReference>");
            estructura.AppendLine("			</cac:Attachment>");
            estructura.AppendLine("			<cac:ResultOfVerification>");
            estructura.AppendLine("				<cbc:ValidatorID>Unidad Especial Dirección de Impuestos y Aduanas Nacionales</cbc:ValidatorID>");
            estructura.AppendLine("				<cbc:ValidationResultCode>02</cbc:ValidationResultCode>");
            estructura.AppendLine("				<cbc:ValidationDate>" + fechaRespuesta + "</cbc:ValidationDate>");
            estructura.AppendLine("				<cbc:ValidationTime>" + horaRespuesta + "</cbc:ValidationTime>");
            estructura.AppendLine("			</cac:ResultOfVerification>");
            estructura.AppendLine("		</cac:DocumentReference>");
            estructura.AppendLine("	</cac:ParentDocumentLineReference>");
            estructura.AppendLine("</AttachedDocument>");
            return estructura.ToString(); ;
        }

    }
}
