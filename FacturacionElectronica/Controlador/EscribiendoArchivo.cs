using FacturacionElectronica.DAO;
using FacturacionElectronica.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Configuration;
using FacturacionElectronica.Conexion;
using System.Security.Cryptography.X509Certificates;
using iTextSharp.text.xml;
using System.Xml;
using eFacturacionColombia_V2.Firma;

namespace FacturacionElectronica.Controlador
{
    class EscribiendoArchivo
    {

        FacturasDAO facDAO;
        Factura miFactura;
        List<DetalleFactura> listado;
        Tercero Cliente;
        Adquiriente miAdquiriente;
        ConfiguracionDIAN miConfiguracion;
        ConfiguracionDIANDAO miConfiguracionDAO;
        List<FacturaImpuestos> listaImpu;
        List<FacturaImpuestos> listaRete;
        FacturaImpuestos miImpuestos;
        string currentDir = AppDomain.CurrentDomain.BaseDirectory.ToLower();

        public String Escribir(String NroFactura, string tipoFactura, string resolucion)
        {
            var titulo = "";
            if (tipoFactura == "F") titulo = "Factura";
            else if (tipoFactura == "D") titulo = "Nota debito";
            else titulo = "Nota crédito";
            Alerta alerta = new Alerta();
            alerta.Mostrar("Escribiendo " + titulo, "Generando los archivos de la " + titulo + " " + NroFactura, 4000);
            try
            {
                Configuracion Config;
                Config = Configuracion.Instancia;

                //Declaracion Variables Nombre Factura
                int NumFac = Int32.Parse(NroFactura);
                Utilitarios util = new Utilitarios();
                //Variables de consulta Factura
                miFactura = new Factura();
                miImpuestos = new FacturaImpuestos();
                listaImpu = new List<FacturaImpuestos>();
                listaRete = new List<FacturaImpuestos>();
                listado = new List<DetalleFactura>();
                Cliente = new Tercero();
                miAdquiriente = new Adquiriente();
                facDAO = new FacturasDAO();
                miConfiguracion = new ConfiguracionDIAN();
                miConfiguracionDAO = new ConfiguracionDIANDAO();
                List<double> Totales = new List<double>();
                String Cadena;

                var directorio = "";
                if (tipoFactura == "F")
                {
                    directorio = @"\Facturas\";
                    titulo = "Factura";
                    miFactura = facDAO.selectManDetalles(NroFactura, tipoFactura, resolucion);
                    //if ((miFactura.TipoFactura == "03" || miFactura.TipoFactura == "04") && miFactura.RefFacturaNumero != "" && miFactura.RefFacturaResolucion != "")
                    //{
                    //    miFactura.RefFactura = facDAO.selectManDetalles(miFactura.RefFacturaNumero, tipoFactura, miFactura.RefFacturaResolucion);
                    //}
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

                if (!currentDir.Contains(@"\debug"))
                {
                    var archivoExiste = false;
                    var carpetaWSDian = @"wsdian\debug";
                    var levels = currentDir.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    var tmpDir = "";

                    foreach (var level in levels)
                    {
                        tmpDir += level + @"\";
                        var fileName = tmpDir + carpetaWSDian + @"\" + Config.NombreCertificado;

                        if (File.Exists(fileName))
                        {
                            currentDir = tmpDir + carpetaWSDian;
                            archivoExiste = true;
                            break;
                        }
                    }
                    if (!archivoExiste) throw new Exception("No se encontró el archivo " + Config.NombreCertificado + " en el equipo en la ubicación donde se esta ejecutando. " + tmpDir);
                }

                X509Certificate2 certificado = null;
                var certificadoRuta = currentDir + @"\" + Config.NombreCertificado;
                try
                {
                    certificado = new X509Certificate2(certificadoRuta, Config.ContraseñaCertificado);
                }
                catch (Exception ex)
                {
                    throw new Exception("No fué posible abrir el archivo " + Config.NombreCertificado + " con la contraseña " + Config.ContraseñaCertificado);
                }


                //Validación del certificado del cliente
                if (certificado == null)
                {
                    throw new Exception("No se encontró el archivo " + Config.NombreCertificado + " en el equipo.");
                }

                if (certificado.Subject.ToString().IndexOf(Config.Firma) <= 0)
                {
                    throw new Exception("El certificado no corresponde a la empresa " + Config.Firma);
                }

                if (certificado.NotAfter < DateTime.Today)
                {
                    throw new Exception("El certificado ya ha caducado: Fecha de vencimiento " + certificado.NotAfter.ToString("dd/MM/yyy"));
                }

                listado = facDAO.ListadoProductos(tipoFactura == "F" ? NroFactura : miFactura.Facturanumero, NroFactura, tipoFactura, resolucion, miFactura.TRM);
                miAdquiriente = facDAO.InfoAdquiriente();
                Cliente = facDAO.InfoTercero(miFactura.Cliente);

                miConfiguracion = miConfiguracionDAO.getConfiguracion(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion); //Validar para Notas debito y credito
                listaImpu = facDAO.selectImpuestos(NroFactura, tipoFactura, resolucion, miFactura.TRM);
                listaRete = facDAO.selectRetenciones(NroFactura, tipoFactura, resolucion, miFactura.TRM);
                Totales = facDAO.selectTotales(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion, miFactura.TRM); //Validar para Notas debito y credito                

                bool isViajes = Config.NombreEmpresa.ToLower().Replace(" ", "").Contains("viajesespeciales");
                //validar que si es viajes especiales, entonces numero de orden y fecha de orden no puede estar vacio
                var messageFailedData = string.IsNullOrEmpty(miFactura.Formapago) ? "La forma de pago" :
                    string.IsNullOrEmpty(miFactura.TipoOperacion) ? "El tipo de operación no está configurado" :
                    string.IsNullOrEmpty(miFactura.TipoFactura) ? "El tipo de factura ó nota no está configurado" :
                    string.IsNullOrEmpty(Cliente.Regimen) ? "El regimen del cliente" :
                    Totales.Count > 4 && Totales[4] > 0 &&
                    (string.IsNullOrEmpty(miFactura.RecajaNo) || string.IsNullOrEmpty(miFactura.FechaRC)) ?
                    "El recibo de caja o la fecha del recibo de caja de los anticipos no son válidos" :
                    string.IsNullOrEmpty(miAdquiriente.Regimen) ? "El regimen del facturador" :
                    string.IsNullOrEmpty(Cliente.ResFiscal) ? "La responsabilidad fiscal del cliente" :
                    string.IsNullOrEmpty(miAdquiriente.ResFiscal) ? "La responsabilidad fiscal del facturador" :
                    miFactura.RefFacturaNumero == "" && (miFactura.TipoFactura == "03" || miFactura.TipoFactura == "04") ? "La factura referenciada" :
                    miFactura.RefFacturaResolucion == "" && (miFactura.TipoFactura == "03" || miFactura.TipoFactura == "04") ? "La factura referenciada" :
                    string.IsNullOrEmpty(miFactura.NumeroOrden) && isViajes ? "El número de orden no puede ser vacio" : string.IsNullOrEmpty(miFactura.FechaOrden) && isViajes ? "La fecha de la de orden no puede ser vacia" :
                    string.IsNullOrEmpty(miFactura.Medio) ? "El medio de pago" : listado.Count == 0 ? "Listado productos" : miAdquiriente == null ? "Información adquiriente" :
                    Cliente == null ? "Información cliente" : miConfiguracion == null ? "Información configuración" :
                    string.IsNullOrEmpty(miConfiguracion.TipoAmbiente) ? "tipo de ambiente" : (Totales.Count == 0 || Totales.Count < 4) ? "Información de totales" :
                    string.IsNullOrEmpty(Cliente.Email) ? "El email del cliente no puede estar vacio" : string.IsNullOrEmpty(miAdquiriente.Email) ? "El email de la empresa no puede estar vacio" : "";

                if (messageFailedData != "")
                {
                    alerta.Cerrar();
                    MessageBox.Show("Ocurrió un error : No fué posible traer información de la base de datos para " + messageFailedData, "Escribiendo " + titulo,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return "";
                }

                foreach (FacturaImpuestos Impu in listaImpu)
                {
                    if (Impu.TipoImpuesto == "01")//IVA
                    {
                        Impu.Nombre = "IVA";
                        Impu.BaseImponible = listado.Where(i => i.PIva == Impu.PImpuesto &&
                        i.CodigoIva == Impu.TipoImpuesto).Sum(i => i.Neto);
                    }
                    else if (Impu.TipoImpuesto == "04")//IC
                    {
                        Impu.Nombre = "INC";
                        Impu.BaseImponible = listado.Where(i => i.PIConsumo == Impu.PImpuesto &&
                        i.CodigoIConsumo == Impu.TipoImpuesto).Sum(i => i.Neto);
                    }
                    else if (Impu.TipoImpuesto == "03")//ICA
                    {
                        Impu.Nombre = "ICA";
                        Impu.BaseImponible = 0;
                    }

                    if (Impu.BaseImponible == 0) Impu.BaseImponible = (double)Totales[1];
                }

                foreach (FacturaImpuestos retencion in listaRete)
                {
                    if (retencion.TipoImpuesto == "05")//RETEIVA
                    {
                        retencion.Nombre = "ReteIVA";
                        retencion.BaseImponible = listaImpu.Where(i => i.TipoImpuesto == "01").Sum(i => i.ValorImpuesto);
                    }
                    else if (retencion.TipoImpuesto == "07")//RETEICA
                    {
                        retencion.Nombre = "ReteICA";
                        //retencion.BaseImponible = listado.Where(i => i.VPropina > 0).Sum(i => i.VPropina);
                        //retencion.BaseImponible = listado.Where(i => i.PReteIcasub != retencion.PImpuesto &&
                        //i.CodigoReteIca == retencion.TipoImpuesto).Sum(i => i.VPropina);
                        retencion.BaseImponible = 0;
                        for (int i = 0; i < listado.Count; i++)
                        {
                            if (listado[i].CodigoReteIca == retencion.TipoImpuesto && listado[i].PReteIcasub == retencion.PImpuesto*10)
                                retencion.BaseImponible += listado[i].VPropina;
                        }
                    }
                    else //06 - RETEFUENTE
                    {
                        retencion.Nombre = "ReteFuente";
                        retencion.BaseImponible = listado.Where(i => i.PRete == retencion.PImpuesto &&
                        i.CodigoRete == retencion.TipoImpuesto).Sum(i => i.Neto);
                    }
                }

                //Generación CUFE
                var valCodImp1 = listaImpu.Where(i => i.TipoImpuesto == "01").FirstOrDefault() != null ?
                    (listaImpu.Where(i => i.TipoImpuesto == "01").Sum(i => i.ValorImpuesto)).ToString("F").Replace(",", ".") : "0.00";
                var valCodImp2 = listaImpu.Where(i => i.TipoImpuesto == "04").FirstOrDefault() != null ?
                    listaImpu.Where(i => i.TipoImpuesto == "04").Sum(i => i.ValorImpuesto).ToString("F").Replace(",", ".") : "0.00";
                var valCodImp3 = listaImpu.Where(i => i.TipoImpuesto == "03").FirstOrDefault() != null ?
                    listaImpu.Where(i => i.TipoImpuesto == "03").Sum(i => i.ValorImpuesto).ToString("F").Replace(",", ".") : "0.00";
                var hashCufe = miFactura.CalculateCUFE(miFactura.Resolucion + (tipoFactura == "F" ? miFactura.Facturanumero : miFactura.Id),
                                                       DateTime.Parse(miFactura.Horafin).ToString("yyyy-MM-dd"),
                                                       DateTime.Parse(miFactura.Horafin).ToString("HH:mm:ss") + "-05:00",
                                                       Totales[1].ToString("F").Replace(',', '.'),
                                                       "01", valCodImp1, "04", valCodImp2, "03", valCodImp3,
                                                       (Totales[3]).ToString("F").Replace(',', '.'),
                                                       miAdquiriente.Nit, Cliente.Identificacion,
                                                       miFactura.TipoFactura == "03" || miFactura.TipoFactura == "04" || tipoFactura != "F" ?
                                                       miConfiguracion.Pin : miConfiguracion.ClaveTecnica,
                                                       miConfiguracion.TipoAmbiente);

                if (miFactura.CUFE != hashCufe)
                {
                    //Validar que para facturas de contingencia es CUDE
                    //miFactura.CUFE = hashCufe;
                    //if (tipoFactura == "F" && miFactura.TipoFactura != "03" && miFactura.TipoFactura != "04")
                    //{
                    //    facDAO.ActualizarCUFEFactura(miFactura.Facturanumero, tipoFactura, hashCufe, resolucion);
                    //}
                }

                // Asignando informacion al documento XML
                Cadena = miConfiguracion.toXML(tipoFactura, miFactura, miAdquiriente.Nit, Cliente.Identificacion, Totales[1] + Totales[2]) + Environment.NewLine;
                Cadena = Cadena + miFactura.toXML(tipoFactura, listado.Count, miConfiguracion.TipoAmbiente, resolucion);
                Cadena = Cadena + miAdquiriente.toXML(miConfiguracion.Prefijo) + Environment.NewLine;
                Cadena = Cadena + Cliente.toXML() + Environment.NewLine;
                if (miFactura.TipoOperacion == "11")
                    Cadena = Cadena + miAdquiriente.taxRepresentativePartyToXML() + Environment.NewLine;
                //Metodo de pago
                Cadena = Cadena + miFactura.FormaPagoToXml();

                if (Totales.Count > 4 && Totales[4] > 0)
                {
                    Cadena = Cadena + miFactura.AnticiposToXml(Totales[4], miFactura.TipoMoneda);
                }

                //foreach (FacturaImpuestos Impu in listaImpu)
                //{
                //    Cadena = Cadena + Impu.toXml(miFactura.TipoMoneda) + Environment.NewLine;
                //}

                //foreach (FacturaImpuestos Impu in listaRete)
                //{
                //    Cadena = Cadena + Impu.retencionesToXml(miFactura.TipoMoneda) + Environment.NewLine;
                //}
                Cadena = Cadena + miFactura.ImpuestosToXml(listaImpu, miFactura.TipoMoneda) + Environment.NewLine;
                Cadena = Cadena + miFactura.RetencionesToXml(listaRete, miFactura.TipoMoneda, tipoFactura) + Environment.NewLine;
                Cadena = Cadena + miFactura.TotalToXml(Totales, tipoFactura, miFactura.TipoMoneda,
                    listaImpu.Sum(i => i.BaseImponible), listaRete.Sum(i => i.ValorImpuesto), listado.Sum(i => i.Neto)) + Environment.NewLine;

                foreach (DetalleFactura detalles in listado)
                {
                    Cadena = Cadena + detalles.toXML(tipoFactura, miAdquiriente.Nit, miAdquiriente.DV,
                        miFactura.TipoMoneda, Cliente.DV, Cliente.Identificacion, miFactura.TipoOperacion, miFactura.TipoFactura, listaRete) + Environment.NewLine;
                }

                var NewfileName = "";
                int documentCount = miConfiguracionDAO.ObtenerCantidadDocumentos(tipoFactura, DateTime.Today.Year, resolucion) + 1;
                string bodyName = miAdquiriente.Nit + "000" + DateTime.Today.Year.ToString().Substring(2) + documentCount.ToString().PadLeft(8, '0');
                if (tipoFactura == "F")
                {
                    NewfileName = "fv" + bodyName;
                    Cadena = Cadena + "</Invoice>" + Environment.NewLine;
                }

                if (tipoFactura == "D")
                {
                    NewfileName = "nd" + bodyName;
                    Cadena = Cadena + "</DebitNote>" + Environment.NewLine;
                }

                if (tipoFactura == "C")
                {
                    NewfileName = "nc" + bodyName;
                    Cadena = Cadena + "</CreditNote>" + Environment.NewLine;
                }

                String fileNameXml = currentDir + directorio + NewfileName + ".xml";


                if (File.Exists(fileNameXml))
                {
                    File.Delete(fileNameXml);
                }

                //PROCESO DE FIRMADO
                // crear instancia
                var firma = new FirmaElectronica
                {
                    RolFirmante = RolFirmante.EMISOR,
                    RutaCertificado = certificadoRuta,
                    ClaveCertificado = Config.ContraseñaCertificado
                };

                // usar horario colombiano
                var fecha = DateTime.Now;

                // firmar contenido
                var contenidoXml = Cadena;
                byte[] bytesContenidoFirmado;

                if (tipoFactura == "F")
                {
                    bytesContenidoFirmado = firma.FirmarFactura(contenidoXml, fecha);
                }
                else if (tipoFactura == "D")
                {
                    bytesContenidoFirmado = firma.FirmarNotaDebito(contenidoXml, fecha);
                }
                else
                {
                    bytesContenidoFirmado = firma.FirmarNotaCredito(contenidoXml, fecha);
                }

                //Parche para solucionar tema en firmado
                XmlDocument xmlDocument = new XmlDocument();
                using (var input = new MemoryStream(bytesContenidoFirmado))
                {
                    xmlDocument.Load(input);
                }

                File.WriteAllBytes(fileNameXml, bytesContenidoFirmado);



                //Comprimiendo Archivos
                String nameZip = currentDir + directorio + "z" + bodyName + ".zip";
                if (File.Exists(nameZip))
                {
                    File.Delete(nameZip);
                }


                // 1
                FileInfo sourceFile = new FileInfo(fileNameXml);
                FileStream sourceStream = sourceFile.OpenRead();
                // 2
                FileStream stream = new FileStream(nameZip, FileMode.CreateNew);
                // 3 
                ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
                // 4 
                ZipArchiveEntry entry = archive.CreateEntry(sourceFile.Name);
                // 5
                Stream zipStream = entry.Open();
                // 6
                sourceStream.CopyTo(zipStream);
                // 7
                zipStream.Close();
                sourceStream.Close();
                archive.Dispose();
                stream.Close();

                alerta.Cerrar();

                alerta = new Alerta();
                alerta.Mostrar("Escribiendo " + titulo, "La " + titulo + " " + NroFactura + " fué escrita correctamente", 4000);
                alerta.Cerrar();
                return bodyName;
            }
            catch (Exception ex)
            {
                alerta.Cerrar();
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Escribiendo " + titulo,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "";
            }
        }
    }
}
