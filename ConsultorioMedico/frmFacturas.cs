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
    public partial class FACTURAS : Form
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        public FACTURAS()
        {
            InitializeComponent();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            cboHora.Enabled = true;
            cboHora.Items.Clear();
            cboHora.Text = "";

            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");

            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT hora FROM Citas WHERE CONVERT(date, fecha) = '" + fecha + "' AND estado = 'P'";

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                cboHora.Items.Add(lector["hora"].ToString());
            }

            lector.Close();
            conexion.Close();
       
        }

        private void cboHora_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();

            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string hora = cboHora.Text;

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT C.idCita, Co.idCobro, P.Nombre, P.ApellidoPaterno + ' ' + P.ApellidoMaterno, P.Telefono, C.hora " +
                                  "FROM Citas C " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "INNER JOIN Cobros Co ON C.idCita = Co.idCita " +
                                  "WHERE CONVERT(date, C.fecha) = '" + fecha + "' AND C.hora = '" + hora + "' AND C.estado = 'P'";

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                dgvDatos.Rows.Add(
                    lector.GetValue(0),
                    lector.GetValue(1),
                    lector.GetValue(2),
                    lector.GetValue(3),
                    lector.GetValue(4),
                    lector.GetValue(5)
                );
            }

            lector.Close();
            conexion.Close();
            cboNombreCliente.Enabled = true;
        }

        private void FACTURAS_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT nombreContri FROM Cliente";

            SqlDataReader lector = comando.ExecuteReader();

            cboNombreCliente.Items.Clear();

            while (lector.Read())
            {
                cboNombreCliente.Items.Add(lector["nombreContri"].ToString());
            }

            lector.Close();
            conexion.Close();

        }

        private void cboNombreCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombre = cboNombreCliente.Text;

            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT idCliente, RFC, domFiscal, email FROM Cliente WHERE nombreContri = '" + nombre + "'";

            SqlDataReader lector = comando.ExecuteReader();

            if (lector.Read())
            {
                txtIdCliente.Text = lector["idCliente"].ToString();
                txtRfc.Text = lector["RFC"].ToString();
                txtDomFis.Text = lector["domFiscal"].ToString();
                txtEmail.Text = lector["email"].ToString();
            }

            lector.Close();
            conexion.Close();
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una cita a facturar.");
                return;
            }

            if (string.IsNullOrEmpty(txtIdCliente.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente primero.");
                return;
            }

            string idCliente = txtIdCliente.Text;
            string idCita = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            string idCobro = dgvDatos.CurrentRow.Cells[1].Value.ToString();

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "INSERT INTO Factura (idCobros, idCliente) VALUES (" + idCobro + ", " + idCliente + ")";
            comando.ExecuteNonQuery();

            comando.CommandText = "UPDATE Citas SET estado = 'f' WHERE idCita = " + idCita;
            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Factura generada exitosamente.");

            cboNombreCliente.SelectedIndex = -1;
            txtIdCliente.Clear();
            txtRfc.Clear();
            txtDomFis.Clear();
            txtEmail.Clear();
            dgvDatos.Rows.Clear();
            cboHora.Items.Clear();
            cboHora.Text = "";
            cboHora.Enabled = false;
            cboNombreCliente.Enabled = false;
            dtpFecha.Enabled = false;
            cmdGrabar.Enabled = false;
            cmdNuevo.Enabled = true;
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
            cmdGrabar.Enabled = true;
            cmdNuevo.Enabled = false;
        }
    }
    }

