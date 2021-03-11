using FacturacionElectronica.Conexion;
using FacturacionElectronica.Controlador;
using FacturacionElectronica.DAO;
using FacturacionElectronica.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;


namespace FacturacionElectronica.Vista
{
    public partial class ContenedorFacturas : Form
    {
        FacturasDAO ListaFacturas = new FacturasDAO();
        AuditoriaDAO auditaDao = new AuditoriaDAO();
        DataTable Datos;
        EscribiendoArchivo myReader;
        String Encargado;
        String Tablero;
        string ambiente = "";
        public ContenedorFacturas(String Encargado)
        {
            this.Encargado = Encargado;
            InitializeComponent();
            Tablero = "F";
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void LoadManifiestos()
        {
            GridViewFacturas.Columns.Clear();
            Datos = new DataTable();
            Datos = ListaFacturas.selectListFacturas();
            GridViewFacturas.DataSource = Datos;
            // Boton Dian
            DataGridViewButtonColumn btnEnviar = new DataGridViewButtonColumn();
            btnEnviar.FlatStyle = FlatStyle.Standard;
            btnEnviar.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviar.Name = "DIAN";
            btnEnviar.Text = "Enviar";
            btnEnviar.UseColumnTextForButtonValue = true;


            //btnEnviar = Image.FromFile(Environment.CurrentDirectory + @"\Mini.jpg");
            // Align the image and text on the button.
            //button1.ImageAlign = ContentAlignment.MiddleRight;
            //button1.TextAlign = ContentAlignment.MiddleLeft;
            //Boton Cliente
            DataGridViewButtonColumn btnEnviarCliente = new DataGridViewButtonColumn();
            btnEnviarCliente.FlatStyle = FlatStyle.Standard;
            btnEnviarCliente.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviarCliente.Name = "Cliente";
            btnEnviarCliente.Text = "Enviar";
            btnEnviarCliente.UseColumnTextForButtonValue = true;
            int columnIndex = 4;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndex, btnEnviar);
            }
            int columnIndexCliente = 6;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndexCliente, btnEnviarCliente);
            }
            GridViewFacturas.DefaultCellStyle.Font = new Font("Arial", 11);
            GridViewFacturas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 85, 118);
            GridViewFacturas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 11, FontStyle.Bold);
            GridViewFacturas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = GridViewFacturas.Columns[0];
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.Width = 70;
            DataGridViewColumn columnUno = GridViewFacturas.Columns[1];
            columnUno.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.Width = 110;
            DataGridViewColumn columnDos = GridViewFacturas.Columns[2];
            columnDos.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.Width = 345;
            DataGridViewColumn columnTres = GridViewFacturas.Columns[3];
            columnTres.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.Width = 120;
            DataGridViewColumn columnCuatro = GridViewFacturas.Columns[4];
            columnCuatro.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.Width = 100;
            DataGridViewColumn columnCinco = GridViewFacturas.Columns[5];
            columnCinco.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.Width = 120;
            DataGridViewColumn columnSeis = GridViewFacturas.Columns[6];
            columnSeis.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnSeis.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            foreach (DataGridViewRow row in GridViewFacturas.Rows)
            {
                var TipoDocumento = row.Cells["Prefijo"].Value.ToString();
                if (ambiente == "")
                {
                    var configura = new ConfiguracionDIANDAO().getConfiguracion(row.Cells["Consecutivo"].Value.ToString(), "", "F", TipoDocumento);
                    ambiente = configura != null ? configura.TipoAmbiente : "";
                }

                var statusDIAN = row.Cells["StatusDIAN"].Value.ToString();
                var statusCliente = row.Cells["StatusCliente"].Value.ToString();

                if (statusDIAN.Contains("3"))
                {
                    if (ambiente == "2")
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = "Validar";
                        cell.Style.BackColor = Color.FromArgb(255, 150, 62);
                        row.Cells["DIAN"] = cell;
                    }
                    else
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = string.Empty;
                        row.Cells["DIAN"] = cell;
                        cell.ReadOnly = true;
                    }
                }

                if (statusCliente.Contains("3"))
                {
                    var cell = new DataGridViewTextBoxCell();
                    cell.Value = string.Empty;
                    row.Cells["Cliente"] = cell;
                    cell.ReadOnly = true;                    
                }
            }

            GridViewFacturas.Refresh();
        }

        private void ContenedorFacturas_Load(object sender, EventArgs e)
        {
            LoadManifiestos();
        }

        private void GridViewFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String NroFactura;
            String TipoDocumento;

            FacturasDAO facDAO = new FacturasDAO();
            Adquiriente miAdquiriente = new Adquiriente();
            miAdquiriente = facDAO.InfoAdquiriente();
            string currentDir = AppDomain.CurrentDomain.BaseDirectory.ToLower();

            if (e.ColumnIndex == 4)
            {
                TipoDocumento = Datos.Rows[e.RowIndex][0].ToString();
                if (Datos.Rows[e.RowIndex][3].ToString().Contains("0"))
                {
                    myReader = new EscribiendoArchivo();
                    NroFactura = Datos.Rows[e.RowIndex][1].ToString();

                    String Respuesta = myReader.Escribir(NroFactura, Tablero, TipoDocumento);
                    if (Respuesta != "")
                    {
                        SendQuote sendQuote = new SendQuote();
                        var respuestaDian = sendQuote.EnvioFacturaElectronicaSOAP(Respuesta, NroFactura, miAdquiriente.Nit, Tablero, TipoDocumento);

                        if (respuestaDian)
                        {
                            auditaDao.RegistrarAuditoria(Encargado, TipoDocumento, NroFactura, 1);
                            if (Tablero.Equals("F")) LoadManifiestos();
                            else if (Tablero.Equals("D")) LoadNotasDebito();
                            else LoadNotasCredito();
                        }
                    }
                }
                else if (ambiente == "2")
                {
                    //Se consultará el estado
                    NroFactura = Datos.Rows[e.RowIndex][1].ToString();
                    SendQuote sendQuote = new SendQuote();
                    sendQuote.ConsultarEstado(NroFactura, Tablero, TipoDocumento);
                }
            }
            else
            {
                if (e.ColumnIndex == 6)
                {
                    if (Datos.Rows[e.RowIndex][4].ToString().Contains("0"))
                    {
                        Alerta alerta = new Alerta();
                        alerta.Mostrar("Enviando cliente", "Se estan enviando los archivos al cliente", 4000);

                        try
                        {
                            var miConfiguracionDAO = new ConfiguracionDIANDAO();
                            List<string> adjuntos = new List<string>();
                            NroFactura = Datos.Rows[e.RowIndex][1].ToString();
                            var resolucion = Datos.Rows[e.RowIndex][0].ToString();
                            int NumFac = Int32.Parse(NroFactura);
                            TipoDocumento = Datos.Rows[e.RowIndex][0].ToString();

                            var carpeta = "";
                            var directorio = "";
                            if (Tablero == "F")
                            {
                                directorio = @"Facturas\";
                            }
                            else if (Tablero == "D")
                            {
                                directorio = @"Debitos\";
                            }
                            else
                            {
                                directorio = @"Creditos\";
                            }

                            string bodyName = miConfiguracionDAO.ObtenerNombreArchivo(Tablero, NroFactura, resolucion);

                            if (bodyName == "")
                            {
                                alerta.Cerrar();
                                MessageBox.Show("Ocurrio un error al enviar el correo: no se encontró el archivo para la factura " + NroFactura, "Envio archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            String zipBodyName = bodyName;

                            if (Tablero == "F")
                            {
                                carpeta = "facturas";
                                bodyName = "ad" + bodyName;
                            }
                            else if (Tablero == "D")
                            {
                                carpeta = "debitos";
                                bodyName = "ad" + bodyName;
                            }
                            else
                            {
                                carpeta = "creditos";
                                bodyName = "ad" + bodyName;
                            }

                            Utilitarios util = new Utilitarios();

                            String nameZip = currentDir + directorio + "fv" + zipBodyName + ".zip";

                            if (File.Exists(nameZip))
                            {
                                File.Delete(nameZip);
                            }

                            // 1
                            FileInfo sourceFileXml = new FileInfo(currentDir + directorio + bodyName + ".xml");
                            FileInfo sourceFilePdf = new FileInfo(currentDir + directorio + bodyName + ".pdf");

                            FileStream sourceStreamXml = sourceFileXml.OpenRead();
                            // 2
                            FileStream stream = new FileStream(nameZip, FileMode.CreateNew);
                            // 3 
                            ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create);
                            // 4 
                            ZipArchiveEntry entry = archive.CreateEntry(sourceFileXml.Name);
                            // 5
                            Stream zipStreamXml = entry.Open();
                            // 6
                            sourceStreamXml.CopyTo(zipStreamXml);
                            // 7
                            zipStreamXml.Close();
                            sourceStreamXml.Close();
                            archive.Dispose();
                            stream.Close();

                            FileStream sourceStreamPdf = sourceFilePdf.OpenRead();
                            // 2
                            stream = new FileStream(nameZip, FileMode.Open);
                            // 3 
                            archive = new ZipArchive(stream, ZipArchiveMode.Update);
                            // 4 
                            ZipArchiveEntry entryPdf = archive.CreateEntry(sourceFilePdf.Name);
                            // 5
                            Stream zipStreamPdf = entryPdf.Open();
                            // 6
                            sourceStreamPdf.CopyTo(zipStreamPdf);
                            // 7
                            zipStreamPdf.Close();
                            sourceStreamPdf.Close();
                            archive.Dispose();
                            stream.Close();

                            adjuntos.Add(directorio + "fv" + zipBodyName + ".zip");
                           

                            Correos mi = new Correos();
                            CorreosDAO miDao = new CorreosDAO();
                            mi = miDao.getConfiguracion(Encargado);

                            if (string.IsNullOrEmpty(mi.Servidor) || string.IsNullOrEmpty(mi.Puerto) || string.IsNullOrEmpty(mi.Password)
                               || string.IsNullOrEmpty(mi.CorreoEmail))
                            {
                                alerta.Cerrar();
                                MessageBox.Show("Valide los siguientes campos \n" +
                                                 (string.IsNullOrEmpty(mi.Servidor) ? "Servidor \n" : string.IsNullOrEmpty(mi.Puerto) ? "Puerto \n"
                                                 : string.IsNullOrEmpty(mi.Password) ? "Contraseña \n" : string.IsNullOrEmpty(mi.CorreoEmail) ? "Dirección de correo \n"
                                                 : ""), "Ocurrio un error al enviar el correo: la configuración de envío no es correcta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            List<String> configuracionDestino = miDao.getDestino(NroFactura, Tablero, resolucion);
                            var resultado = mi.SendEmail(adjuntos, configuracionDestino[0], configuracionDestino[1], NroFactura, Tablero, resolucion);
                            alerta.Cerrar();
                            if (resultado == "")
                            {
                                auditaDao.RegistrarAuditoria(Encargado, TipoDocumento, NroFactura, 2);
                                MessageBox.Show("Archivos enviados correctamente al cliente", "Envio archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                auditaDao.RegistrarAuditoria(Encargado, TipoDocumento, NroFactura, 1);
                                if (Tablero.Equals("F")) LoadManifiestos();
                                else if (Tablero.Equals("D")) LoadNotasDebito();
                                else LoadNotasCredito();
                            }
                            else
                            {
                                MessageBox.Show("Ocurrio un error al enviar el correo: " + resultado, "Envio archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            alerta.Cerrar();
                            MessageBox.Show("Ocurrió un error : " + ex.Message, "Envio archivos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnManual_Click(object sender, EventArgs e)
        {
            try
            {
                string currentDir = Environment.CurrentDirectory;
                string path = currentDir + "/ManualUsuario.pdf";
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error : " + ex.Message, "Abriendo Manual",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNotasCredito()
        {
            GridViewFacturas.Columns.Clear();

            Datos = new DataTable();
            Datos = ListaFacturas.selectListNCredito();
            GridViewFacturas.DataSource = Datos;
            // Boton Dian
            DataGridViewButtonColumn btnEnviar = new DataGridViewButtonColumn();
            btnEnviar.FlatStyle = FlatStyle.Standard;
            btnEnviar.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviar.Name = "DIAN";
            btnEnviar.Text = "Enviar";
            btnEnviar.UseColumnTextForButtonValue = true;
            //Boton Cliente
            DataGridViewButtonColumn btnEnviarCliente = new DataGridViewButtonColumn();
            btnEnviarCliente.FlatStyle = FlatStyle.Standard;
            btnEnviarCliente.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviarCliente.Name = "Cliente";
            btnEnviarCliente.Text = "Enviar";
            btnEnviarCliente.UseColumnTextForButtonValue = true;
            int columnIndex = 4;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndex, btnEnviar);
            }
            int columnIndexCliente = 6;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndexCliente, btnEnviarCliente);
            }
            GridViewFacturas.DefaultCellStyle.Font = new Font("Arial", 11);
            DataGridViewColumn column = GridViewFacturas.Columns[0];
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.Width = 70;
            DataGridViewColumn columnUno = GridViewFacturas.Columns[1];
            columnUno.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.Width = 110;
            DataGridViewColumn columnDos = GridViewFacturas.Columns[2];
            columnDos.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.Width = 345;
            DataGridViewColumn columnTres = GridViewFacturas.Columns[3];
            columnTres.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.Width = 120;
            DataGridViewColumn columnCuatro = GridViewFacturas.Columns[4];
            columnCuatro.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.Width = 100;
            DataGridViewColumn columnCinco = GridViewFacturas.Columns[5];
            columnCinco.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.Width = 120;
            DataGridViewColumn columnSeis = GridViewFacturas.Columns[6];
            columnSeis.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnSeis.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            foreach (DataGridViewRow row in GridViewFacturas.Rows)
            {
                var TipoDocumento = row.Cells["Prefijo"].Value.ToString();
                if (ambiente == "")
                {
                    FacturasDAO facDAO = new FacturasDAO();
                    Factura miFactura = new Factura();

                    miFactura = facDAO.selectManDetallesCredito(row.Cells["Consecutivo"].Value.ToString(), "C", TipoDocumento);

                    var configura = new ConfiguracionDIANDAO().getConfiguracion(row.Cells["Consecutivo"].Value.ToString(), miFactura.Facturanumero, "C", TipoDocumento);
                    ambiente = configura != null ? configura.TipoAmbiente : "";
                }

                var statusDIAN = row.Cells["StatusDIAN"].Value.ToString();
                var statusCliente = row.Cells["StatusCliente"].Value.ToString();

                if (statusDIAN.Contains("3"))
                {
                    if (ambiente == "2")
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = "Validar";
                        cell.Style.BackColor = Color.FromArgb(255, 150, 62);
                        row.Cells["DIAN"] = cell;
                    }
                    else
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = string.Empty;
                        row.Cells["DIAN"] = cell;
                        cell.ReadOnly = true;
                    }
                }

                if (statusCliente.Contains("3"))
                {
                    var cell = new DataGridViewTextBoxCell();
                    cell.Value = string.Empty;
                    row.Cells["Cliente"] = cell;
                    cell.ReadOnly = true;
                }
            }

            GridViewFacturas.Refresh();

        }

        private void BtnNotasCredito_Click(object sender, EventArgs e)
        {
            Tablero = "C";
            LoadNotasCredito();
        }


        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            Tablero = "F";
            LoadManifiestos();
        }

        private void LoadNotasDebito()
        {
            GridViewFacturas.Columns.Clear();

            Datos = new DataTable();
            Datos = ListaFacturas.selectListNDebito();
            GridViewFacturas.DataSource = Datos;
            // Boton Dian
            DataGridViewButtonColumn btnEnviar = new DataGridViewButtonColumn();
            btnEnviar.FlatStyle = FlatStyle.Standard;
            btnEnviar.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviar.Name = "DIAN";
            btnEnviar.Text = "Enviar";
            btnEnviar.UseColumnTextForButtonValue = true;
            //Boton Cliente
            DataGridViewButtonColumn btnEnviarCliente = new DataGridViewButtonColumn();
            btnEnviarCliente.FlatStyle = FlatStyle.Standard;
            btnEnviarCliente.CellTemplate.Style.BackColor = Color.FromArgb(255, 150, 62);
            btnEnviarCliente.Name = "Cliente";
            btnEnviarCliente.Text = "Enviar";
            btnEnviarCliente.UseColumnTextForButtonValue = true;
            int columnIndex = 4;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndex, btnEnviar);
            }
            int columnIndexCliente = 6;
            if (GridViewFacturas.Columns["Enviar"] == null)
            {
                GridViewFacturas.Columns.Insert(columnIndexCliente, btnEnviarCliente);
            }
            GridViewFacturas.DefaultCellStyle.Font = new Font("Arial", 11);
            DataGridViewColumn column = GridViewFacturas.Columns[0];
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.Width = 70;
            DataGridViewColumn columnUno = GridViewFacturas.Columns[1];
            columnUno.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnUno.Width = 110;
            DataGridViewColumn columnDos = GridViewFacturas.Columns[2];
            columnDos.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnDos.Width = 345;
            DataGridViewColumn columnTres = GridViewFacturas.Columns[3];
            columnTres.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnTres.Width = 120;
            DataGridViewColumn columnCuatro = GridViewFacturas.Columns[4];
            columnCuatro.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCuatro.Width = 100;
            DataGridViewColumn columnCinco = GridViewFacturas.Columns[5];
            columnCinco.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnCinco.Width = 120;
            DataGridViewColumn columnSeis = GridViewFacturas.Columns[6];
            columnSeis.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnSeis.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewRow row in GridViewFacturas.Rows)
            {
                var TipoDocumento = row.Cells["Prefijo"].Value.ToString();
                if (ambiente == "")
                {
                    FacturasDAO facDAO = new FacturasDAO();
                    Factura miFactura = new Factura();

                    miFactura = facDAO.selectManDetallesDebito(row.Cells["Consecutivo"].Value.ToString(), "D", TipoDocumento);

                    var configura = new ConfiguracionDIANDAO().getConfiguracion(row.Cells["Consecutivo"].Value.ToString(), miFactura.Facturanumero, "D", TipoDocumento);
                    ambiente = configura != null ? configura.TipoAmbiente : "";
                }

                var statusDIAN = row.Cells["StatusDIAN"].Value.ToString();
                var statusCliente = row.Cells["StatusCliente"].Value.ToString();

                if (statusDIAN.Contains("3"))
                {
                    if (ambiente == "2")
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = "Validar";
                        cell.Style.BackColor = Color.FromArgb(255, 150, 62);
                        row.Cells["DIAN"] = cell;
                    }
                    else
                    {
                        var cell = new DataGridViewTextBoxCell();
                        cell.Value = string.Empty;
                        row.Cells["DIAN"] = cell;
                        cell.ReadOnly = true;
                    }
                }

                if (statusCliente.Contains("3"))
                {
                    var cell = new DataGridViewTextBoxCell();
                    cell.Value = string.Empty;
                    row.Cells["CLIENTE"] = cell;
                    cell.ReadOnly = true;
                }
            }

            GridViewFacturas.Refresh();
        }

        private void BtnNotasDebito_Click(object sender, EventArgs e)
        {
            Tablero = "D";
            LoadNotasDebito();
        }


        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (Tablero.Equals("F"))
            {
                LoadManifiestos();
            }
            else if (Tablero.Equals("D"))
            {
                LoadNotasDebito();
            }
            else
            {
                LoadNotasCredito();
            }
        }

        private void GridViewFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
