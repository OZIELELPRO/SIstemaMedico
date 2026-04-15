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

       

        private void frmCobros_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        }

       

       

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            if (cboCita.SelectedIndex != -1 && cboMetodo.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                    conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "INSERT INTO Cobros (idCita, tipoPago, monto) VALUES (" +
                    cboCita.SelectedItem.ToString() + ", '" +
                    cboMetodo.SelectedItem.ToString() + "', " +
                    txtCantidad.Text + ")";

                comando.ExecuteNonQuery();

                comando.CommandText = "UPDATE Citas SET estado = 'P' WHERE idCita = " + cboCita.SelectedItem.ToString();

                comando.ExecuteNonQuery();
                conexion.Close();

                MessageBox.Show("Datos guardados correctamente");

                cboCita.Items.Clear();
                cboMetodo.SelectedIndex = -1;
                txtCantidad.Clear();
                cmdGrabar.Enabled = false;
                dgvDatos.Rows.Clear();

            }
            else
            {
                MessageBox.Show("Debe seleccionar una cita, un método de pago e ingresar un monto");

            }

            cmdGrabar.Enabled = false;
            button1.Enabled = true;
            cmdBuscar.Enabled = false;
            cboCita.Enabled = false;    
            cboMetodo.Enabled = false;
            txtCantidad.Enabled = false;
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

            comando.CommandText = "SELECT C.idCita, P.Nombre, P.ApellidoPaterno + ' ' + P.ApellidoMaterno, P.Telefono, C.hora, C.estado " +
                                  "FROM Citas C INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "WHERE CONVERT(date, C.fecha) = '" + fecha + "' AND C.estado = 'M'";

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

            cmdGrabar.Enabled = true;
            cboCita.Enabled = true;
            cboMetodo.Enabled = true;
            txtCantidad.Enabled = true;
        }

        private void cboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtpFecha.Enabled = true;
            button1.Enabled = false;
            cmdBuscar.Enabled = true;
        }
    }
}
