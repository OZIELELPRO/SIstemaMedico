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
    public partial class frmExpediente : Form
    {

        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;


        public frmExpediente()
        {
            InitializeComponent();
        }

        private void frmExpediente_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            dgvCitas.Rows.Clear();
            cboIdCita.Items.Clear();
            conexion.Open();
            comando = conexion.CreateCommand();
            string r = "SELECT * FROM Citas " +
                        "WHERE CONVERT(date,fecha) = '" + fecha + "' " +
                        "AND estado = 'R'";
            comando.CommandText = r; 
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                dgvCitas.Rows.Add(
                    lector.GetValue(0),
                    lector.GetValue(1),
                    lector.GetValue(2),
                    lector.GetValue(4),
                    lector.GetValue(3)
                );

                cboIdCita.Items.Add(lector["idCita"].ToString());
            }

            lector.Close();
            conexion.Close();
        }

        private void cboIdCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCita = Convert.ToInt32(cboIdCita.SelectedItem.ToString());
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT P.idPaciente, P.Nombre, P.ApellidoPaterno, P.telefono " +
                                  "FROM Citas C " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "WHERE C.idCita = " + idCita;
            SqlDataReader lector = comando.ExecuteReader();
            if (lector.Read())
            {
                txtIdPaciente.Text = lector["idPaciente"].ToString();
                txtNombre.Text = lector["Nombre"].ToString() + " " + lector["ApellidoPaterno"].ToString();
                txtTelefono.Text = lector["Telefono"].ToString();
            }
            int IdPaciente = Convert.ToInt32(txtIdPaciente.Text);
            lector.Close();
            conexion.Close();

            CargarExpediente(IdPaciente);

        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            int IdPaciente = Convert.ToInt32(txtIdPaciente.Text);
            conexion.Open();

            string query = "INSERT INTO Expediente " +
                           "(Peso, Estatura, Temperatura, Presion, Sintomas, Diagnostico, IdCita) VALUES (" +
                           Convert.ToDecimal(txtPeso.Text) + "," +
                           Convert.ToDecimal(txtEstatura.Text) + "," +
                           Convert.ToDecimal(txtTemperatura.Text) + "," +
                           "'" + txtPresion.Text + "'," +
                           "'" + txtSintomas.Text + "'," +
                           "'" + txtDiagnostico.Text + "'," +
                           cboIdCita.Text + ")";

            SqlCommand comando = new SqlCommand(query, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Expediente guardado correctamente");
            txtPeso.Text = "";
            txtEstatura.Text = "";
            txtPresion.Text = "";
            txtSintomas.Text = "";
            txtDiagnostico.Text = "";
            txtTemperatura.Text = "";
            CargarExpediente(IdPaciente);
        }





        public void CargarExpediente(int IdPaciente)
        {
            conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT E.* FROM Expediente E " +
                         "INNER JOIN Citas C ON E.IdCita = C.IdCita " +
                         "WHERE C.IdPaciente = " + IdPaciente;
            SqlDataReader lectorExpediente = comando.ExecuteReader();

            dgvExpediente.Rows.Clear();

            while (lectorExpediente.Read())
            {
                dgvExpediente.Rows.Add(
                    lectorExpediente.GetValue(0),
                    lectorExpediente.GetValue(1),
                    lectorExpediente.GetValue(2),
                    lectorExpediente.GetValue(3),
                    lectorExpediente.GetValue(4),
                    lectorExpediente.GetValue(5),
                    lectorExpediente.GetValue(6),
                    lectorExpediente.GetValue(7)
                );
            }

            lectorExpediente.Close();
            conexion.Close();
        }

        private void cmdAtender_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string query = "UPDATE Citas SET estado = 'A' WHERE IdCita = " + cboIdCita.Text;

            SqlCommand comando = new SqlCommand(query, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Cita Atendida, Estado= 'A'");
            dgvExpediente.Rows.Clear();
            cmdBuscar_Click(null, null);

       
        }

    }
}