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

namespace ConsultorioMedico
{
    public partial class frmConsultaFacturas : Form
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();

        public frmConsultaFacturas()
        {
            InitializeComponent();
        }

        private void frmConsultaFacturas_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            dgvDatos.Columns.Clear();
            dgvDatos.Columns.Add("idF", "IdFactura");
            dgvDatos.Columns.Add("idC", "IdCobro");
            dgvDatos.Columns.Add("idCt", "IdCita");
            dgvDatos.Columns.Add("nom", "NomPaciente");
            dgvDatos.Columns.Add("fec", "Fecha");
            dgvDatos.Columns.Add("hor", "Hora");
            dgvDatos.Columns.Add("con", "Contribuyente");
            dgvDatos.Columns.Add("mon", "Monto");

            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cmdOkIndividual_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;

            dgvDatos.Rows.Clear();
            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT F.idFactura, Co.idCobro, C.idCita, P.Nombre, C.fecha, C.hora, Cl.nombreContri, Co.monto " +
                                  "FROM Factura F " +
                                  "INNER JOIN Cobros Co ON F.idCobros = Co.idCobro " +
                                  "INNER JOIN Citas C ON Co.idCita = C.idCita " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "INNER JOIN Cliente Cl ON F.idCliente = Cl.idCliente " +
                                  "WHERE F.idFactura = " + txtId.Text;

            SqlDataReader lector = comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    dgvDatos.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3),
                        lector.GetValue(4),
                        lector.GetValue(5),
                        lector.GetValue(6),
                        lector.GetValue(7)
                    );
                }
            }
            else
            {
                MessageBox.Show("No se encontró la factura con ese ID.");
            }

            lector.Close();
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string desde = dtpDesde.Value.ToString("yyyy-MM-dd");
            string hasta = dtpHasta.Value.ToString("yyyy-MM-dd");

            dgvDatos.Rows.Clear();
            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT F.idFactura, Co.idCobro, C.idCita, P.Nombre, C.fecha, C.hora, Cl.nombreContri, Co.monto " +
                                  "FROM Factura F " +
                                  "INNER JOIN Cobros Co ON F.idCobros = Co.idCobro " +
                                  "INNER JOIN Citas C ON Co.idCita = C.idCita " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "INNER JOIN Cliente Cl ON F.idCliente = Cl.idCliente " +
                                  "WHERE C.fecha BETWEEN '" + desde + "' AND '" + hasta + "'";

            SqlDataReader lector = comando.ExecuteReader();

            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    dgvDatos.Rows.Add(
                        lector.GetValue(0),
                        lector.GetValue(1),
                        lector.GetValue(2),
                        lector.GetValue(3),
                        lector.GetValue(4),
                        lector.GetValue(5),
                        lector.GetValue(6),
                        lector.GetValue(7)
                    );
                }
            }
            else
            {
                MessageBox.Show("No se encontraron facturas en el periodo seleccionado.");
            }

            lector.Close();
            conexion.Close();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}