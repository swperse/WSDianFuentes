using FacturacionElectronica.DAO;
using FacturacionElectronica.Modelo;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.factories;

namespace FacturacionElectronica.Controlador
{
    class ConstructorPDF
    {
        FacturasDAO facDAO;
        Factura miFactura;
        List<DetalleFactura> listado;
        Tercero Cliente;
        Adquiriente miAdquiriente;
        ConfiguracionDIAN miConfiguracion;
        ConfiguracionDIANDAO miConfiguracionDAO;
        List<FacturaImpuestos> listaImpu;
        FacturaImpuestos miImpuestos;
        string currentDir = Environment.CurrentDirectory;

        public void Construir(String NroFactura, string tipoFactura, string resolucion)
        {
            var titulo = "";
            //Estilos Documentos
            BaseColor FontColour = new BaseColor(31, 97, 141);
            Font blackListTextFont = FontFactory.GetFont("Arial", 12, FontColour);

            miFactura = new Factura();

            var directorio = "";

            if (tipoFactura == "F")
            {
                directorio = @"\Facturas\";
                titulo = "Factura de venta";
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


            //Informacion Necesaria
            miImpuestos = new FacturaImpuestos();
            listaImpu = new List<FacturaImpuestos>();
            listado = new List<DetalleFactura>();
            Cliente = new Tercero();
            miAdquiriente = new Adquiriente();
            facDAO = new FacturasDAO();
            miConfiguracion = new ConfiguracionDIAN();
            miConfiguracionDAO = new ConfiguracionDIANDAO();
            List<double> Totales = new List<double>();
            /*try
            {*/
            miFactura = facDAO.selectManDetalles(NroFactura, tipoFactura, resolucion);
            listado = facDAO.ListadoProductos(NroFactura, NroFactura, tipoFactura, resolucion, miFactura.TRM); //Validar para notas debito y credito
            miAdquiriente = facDAO.InfoAdquiriente(); //Validar para notas debito y credito
            Cliente = facDAO.InfoTercero(miFactura.Cliente);
            miConfiguracion = miConfiguracionDAO.getConfiguracion(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion); //Validar para notas debito y credito
            listaImpu = facDAO.selectImpuestos(NroFactura, tipoFactura, resolucion, miFactura.TRM); //Validar para notas debito y credito
            Totales = facDAO.selectTotales(NroFactura, tipoFactura == "F" ? "" : miFactura.Facturanumero, tipoFactura, resolucion, miFactura.TRM); //Validar para notas debito y credito


            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(currentDir + directorio + "Ejemplo.pdf", FileMode.Create));
            doc.Open();
            PdfPTable FacInfoBasica = new PdfPTable(2);
            FacInfoBasica.WidthPercentage = 50.0f;
            FacInfoBasica.HorizontalAlignment = Element.ALIGN_RIGHT;
            PdfPCell cellTipoFac = new PdfPCell(new Phrase(titulo));
            cellTipoFac.Colspan = 2;
            cellTipoFac.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            FacInfoBasica.AddCell(new Paragraph("NIT:", blackListTextFont));
            FacInfoBasica.AddCell("Col 2 Row 1");
            FacInfoBasica.AddCell(cellTipoFac);
            FacInfoBasica.AddCell(new Paragraph("No.", blackListTextFont));
            FacInfoBasica.AddCell(miFactura.Facturanumero);
            DateTime myDate = DateTime.Parse(miFactura.Fechafac);
            String Fecha = myDate.ToString("yyyy-MM-dd");
            String Hora = myDate.ToString("HH:MM:ss");
            FacInfoBasica.AddCell(new Paragraph("Fecha y Hora", blackListTextFont));
            FacInfoBasica.AddCell(Fecha + " " + Hora);
            FacInfoBasica.AddCell(new Paragraph("Tipo Moneda", blackListTextFont));
            FacInfoBasica.AddCell("COP");
            //Agregacion de la Factura
            doc.Add(FacInfoBasica);
            doc.Add(new Paragraph(Environment.NewLine));
            //Informacion de Productos
            PdfPTable DetalleFac = new PdfPTable(7);
            DetalleFac.TotalWidth = 550f;
            DetalleFac.LockedWidth = true;
            DetalleFac.AddCell("Cantidad");
            DetalleFac.AddCell("Codigo Referencia");
            PdfPCell cellTipoDescItemT = new PdfPCell(new Phrase("Descripcion"));
            cellTipoDescItemT.Colspan = 2;
            DetalleFac.AddCell(cellTipoDescItemT);
            DetalleFac.AddCell("Precio Unit");
            DetalleFac.AddCell("Descuento");
            DetalleFac.AddCell("Total");
            foreach (DetalleFactura detalles in listado)
            {
                DetalleFac.AddCell(detalles.Cantidad.ToString());
                DetalleFac.AddCell(detalles.Referencia);
                PdfPCell cellTipoDescItem = new PdfPCell(new Phrase(detalles.Nombreref));
                cellTipoDescItem.Colspan = 2;
                DetalleFac.AddCell(cellTipoDescItem);
                DetalleFac.AddCell(detalles.Precio.ToString());
                DetalleFac.AddCell(detalles.Descuento.ToString());
                DetalleFac.AddCell(detalles.Neto.ToString());
            }
            doc.Add(DetalleFac);
            doc.Close();
        }
    }
}
