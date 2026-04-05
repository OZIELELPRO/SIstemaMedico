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
    public partial class frmCobros : Form
    {

        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        public frmCobros()
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
            comando.CommandText = "SELECT hora FROM Citas WHERE CONVERT(date, fecha) = '" + fecha + "' AND estado = 'M'";

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                cboHora.Items.Add(lector["hora"].ToString());
            }

            lector.Close();
            conexion.Close();
        }

        private void frmCobros_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        }

        private void cboHora_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();

            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string hora = cboHora.Text;

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT C.idCita, P.Nombre, P.ApellidoPaterno + ' ' + P.ApellidoMaterno, P.Telefono, C.hora " +
                                  "FROM Citas C INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "WHERE CONVERT(date, C.fecha) = '" + fecha + "' AND C.hora = '" + hora + "' AND C.estado = 'M'";

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                dgvDatos.Rows.Add(
                    lector.GetValue(0),
                    lector.GetValue(1),
                    lector.GetValue(2),
                    lector.GetValue(3),
                    lector.GetValue(4)
                );
            }

            lector.Close();
            conexion.Close();
            cboMetodo.Enabled = true;
            txtCantidad.Enabled = true;
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
            cmdGrabar.Enabled = true;
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una cita a pagar.");
                return;
            }

            if (cboMetodo.SelectedIndex == -1 || string.IsNullOrEmpty(cboMetodo.Text))
            {
                MessageBox.Show("Debe seleccionar un método de pago.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Debe ingresar el monto a cobrar.");
                return;
            }

            string idCita = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            string tipoPago = cboMetodo.Text;
            string monto = txtCantidad.Text;

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "INSERT INTO Cobros (idCita, tipoPago, monto) VALUES (" + idCita + ", '" + tipoPago + "', " + monto + ")";
            comando.ExecuteNonQuery();

            comando.CommandText = "UPDATE Citas SET estado = 'P' WHERE idCita = " + idCita;
            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Cobro registrado y cita actualizada.");

            txtCantidad.Clear();
            cboMetodo.SelectedIndex = -1;
            dgvDatos.Rows.Clear();
            cboHora.Items.Clear();
            cmdGrabar.Enabled = false;  
            cmdNuevo.Enabled = true;
            dtpFecha.Enabled = false;
            cboHora.Enabled = false;
            cboMetodo.Enabled = false;  
            txtCantidad.Enabled = false;

        }
    }
}
