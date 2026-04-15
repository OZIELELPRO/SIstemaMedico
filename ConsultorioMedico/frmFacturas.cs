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
                cboNombreCliente.Items.Add(lector[0].ToString());
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
            if (cboCita.SelectedIndex == -1)
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
            string idCita = cboCita.SelectedItem.ToString();
            string idCobro = "";


        

            conexion.Open();

            comando.CommandText = "SELECT idCobro FROM Cobros WHERE idCita = " + idCita;
            idCobro = comando.ExecuteScalar().ToString();

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
          
            cboNombreCliente.Enabled = false;
            cboCita.SelectedIndex = -1;
            cboCita.Items.Clear();
            cboCita.Enabled = false;
           
            dtpFecha.Enabled = false;
            cmdGrabar.Enabled = false;
            cmdBuscar.Enabled = false;
            cmdNuevo.Enabled = true;
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
            cmdNuevo.Enabled = false;
            cmdBuscar.Enabled = true;

        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            cboCita.Items.Clear();

            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT C.idCita, Co.idCobro, P.Nombre, P.ApellidoPaterno + ' ' + P.ApellidoMaterno, P.Telefono, C.hora " +
                                  "FROM Citas C " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "INNER JOIN Cobros Co ON C.idCita = Co.idCita " +
                                  "WHERE CONVERT(date, C.fecha) = '" + fecha + "' AND C.estado = 'P'";

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
                cboCita.Items.Add(lector.GetValue(0));
            }

            lector.Close();
            conexion.Close();
            cboNombreCliente.Enabled = true;
            cboCita.Enabled = true;
            cmdGrabar.Enabled = true;
        }
    }
    }

