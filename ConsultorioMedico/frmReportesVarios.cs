using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Configuration;

namespace ConsultorioMedico
{
    public partial class frmReportesVarios : Form
    {
        public frmReportesVarios()
        {
            InitializeComponent();
        }

        private void frmReportesVarios_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        // --- REPORTE EN UN PERIODO (Botón Derecha) ---
        private void button2_Click(object sender, EventArgs e)
        {
            if (cboIdCita.Text == "RECETAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteRecetasPorPeriodo", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaDesde", dtpDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@FechaHasta", dtpHasta.Value.Date);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteGeneralRecetas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();

                        if (Data.Tables[0].Rows.Count == 0) MessageBox.Show("No se encontraron recetas.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
            else if (cboIdCita.Text == "COBROS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteCobrosPorPeriodo", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        // IMPORTANTE: Verifica que el archivo se llame ReporteCobros.rdlc
                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteCobros.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();

                        if (Data.Tables[0].Rows.Count == 0) MessageBox.Show("No se encontraron cobros.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "FACTURAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteFacturasPorPeriodo", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        // IMPORTANTE: Verifica que el archivo se llame ReporteCobros.rdlc
                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteFacturas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();

                        if (Data.Tables[0].Rows.Count == 0) MessageBox.Show("No se encontraron Facturas.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "EXPEDIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteExpedientesPorPeriodo", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Desde", dtpDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@Hasta", dtpHasta.Value.Date);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        // IMPORTANTE: Verifica que el archivo se llame ReporteCobros.rdlc
                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteExpedientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();

                        if (Data.Tables[0].Rows.Count == 0) MessageBox.Show("No se encontraron Expedientes.");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        // --- REPORTE GENERAL (Botón Centro) ---
        private void cmdOkGeneral_Click_1(object sender, EventArgs e)
        {
            if (cboIdCita.Text == "RECETAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteRecetasGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteGeneralRecetas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
            else if (cboIdCita.Text == "COBROS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteCobrosGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteCobros.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "FACTURAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteFacturasGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteFacturas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "EXPEDIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteExpedientesGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteExpedientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
            else if (cboIdCita.Text == "PACIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReportePacientesGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReportePacientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
            else if (cboIdCita.Text == "CLIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteClientesGeneral", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteClientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        // --- REPORTE INDIVIDUAL (Botón Centro Izquierda) ---
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdBuscar.Text))
            {
                MessageBox.Show("Por favor, ingrese el Id a buscar.");
                return;
            }

            if (cboIdCita.Text == "RECETAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteRecetasIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idReceta", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró la receta."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteGeneralRecetas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
            else if (cboIdCita.Text == "COBROS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteCobrosIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCobro", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró el cobro."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteCobros.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "FACTURAS")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteFacturasIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idFactura", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró la factura."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteFacturas.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "EXPEDIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteExpedientesIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idExpediente", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró el cobro."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteExpedientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "CLIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReporteClientesIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró el cliente."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReporteClientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }

            else if (cboIdCita.Text == "PACIENTES")
            {
                try
                {
                    using (SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
                    {
                        Conexion.Open();
                        SqlCommand cmd = new SqlCommand("ReportePacientesIndividual", Conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idPaciente", txtIdBuscar.Text);

                        SqlDataAdapter Adaptador = new SqlDataAdapter(cmd);
                        DataSet Data = new DataSet();
                        Adaptador.Fill(Data);

                        if (Data.Tables[0].Rows.Count == 0) { MessageBox.Show("No se encontró el paciente."); return; }

                        Data.DataSetName = "DataSet1";
                        ReportDataSource Reportes = new ReportDataSource("DataSet1", Data.Tables[0]);
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(Reportes);

                        string rutaRelativa = System.IO.Path.Combine(Application.StartupPath, "Reportes", "ReportePacientes.rdlc");
                        reportViewer1.LocalReport.ReportPath = rutaRelativa;
                        reportViewer1.RefreshReport();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void cboIdCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIdCita.Text == "CLIENTES" || cboIdCita.Text == "PACIENTES")
            {
                groupBox2.Visible = false;
            }
            else
            {
                groupBox2.Visible = true;
            }
        }
    }
}
