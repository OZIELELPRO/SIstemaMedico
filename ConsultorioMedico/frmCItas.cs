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
    public partial class frmCItas : Form
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        public frmCItas()
        {
            InitializeComponent();
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            dgvDatos.Rows.Clear();
            cboIdCancelacion.Items.Clear();
            string paciente = cboPacientes.Text;
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");

            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT * FROM Citas " +
                      "WHERE CONVERT(date,fecha) = '" + fecha + "'";
            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                dgvDatos.Rows.Add(
                    lector.GetValue(0),
                    lector.GetValue(1),
                    lector.GetValue(2),
                    lector.GetValue(4),
                    lector.GetValue(3)
                );

                if (lector["estado"].ToString() == "R")
                {
                    cboIdCancelacion.Items.Add(lector["idCita"].ToString());
                }
            }

            lector.Close();
            conexion.Close();

            cboHora.Items.Clear();

            for (int hora = 12; hora <= 19; hora++)
            {
                bool ocupado = false;

                for (int i = 0; i < dgvDatos.Rows.Count - 1; i++)
                {
                    if (dgvDatos.Rows[i].Cells[3].Value != null)
                    {
                        TimeSpan horaBD = (TimeSpan)dgvDatos.Rows[i].Cells[3].Value;
                        int horaExtraida = horaBD.Hours;

                        if (horaExtraida == hora && dgvDatos.Rows[i].Cells[4].Value.ToString() == "R")
                        {
                            ocupado = true;
                            break;
                        }
                    }
                }

                if (!ocupado)
                {
                    cboHora.Items.Add(hora + ":00");
                }
            }
        }

        private void frmCItas_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT Nombre FROM Paciente";

            SqlDataReader lector;
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                cboPacientes.Items.Add(lector.GetValue(0).ToString());
            }

            lector.Close();
            conexion.Close();




        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            string paciente = cboPacientes.Text;
            string fecha = dtpFecha.Value.ToString("yyyy-MM-dd");
            string hora = cboHora.Text;
            string idPaciente = txtId.Text;

            conexion.Open();
            comando = conexion.CreateCommand();

            string consultaValidar = $"SELECT COUNT(*) FROM Citas WHERE Fecha = '{fecha}' AND Hora = '{hora}' AND Estado = 'R'";
            comando.CommandText = consultaValidar;
            int existeCita = Convert.ToInt32(comando.ExecuteScalar());

            if (existeCita > 0)
            {
                MessageBox.Show("Cita ocupada. Por favor elija otro horario.");
                conexion.Close();
                return;
            }

            // Insertar la cita
            comando.CommandText = "INSERT INTO Citas (idPaciente, fecha, hora, estado) " +
                                  "VALUES (" + idPaciente + ", '" + fecha + "', '" + hora + "', 'R')";
            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Cita registrada");

            cmdBuscar_Click(null, null);
        }

        private void cboPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtApellidos.Text = "";
            txtTelefono.Text = "";
            txtId.Text = "";
            string paciente = cboPacientes.Text;

            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT idPaciente FROM Paciente WHERE Nombre='" + paciente + "'";
            int idPaciente = Convert.ToInt32(comando.ExecuteScalar());
            txtId.Text = idPaciente.ToString();
            comando.CommandText = "SELECT ApellidoMaterno, ApellidoPaterno, Telefono FROM Paciente WHERE idPaciente='" + idPaciente + "'";
            SqlDataReader lector;
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtApellidos.Text = lector.GetValue(0).ToString() + " " + lector.GetValue(1).ToString();
                txtTelefono.Text = lector.GetValue(2).ToString(); 
            }
            lector.Close();
            conexion.Close();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancelarCita_Click(object sender, EventArgs e)
        {
            if (cboIdCancelacion.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un ID de cita para cancelar.");
                return;
            }


            conexion.Open();
            string query = "UPDATE Citas SET estado = 'C' WHERE idCita = " + cboIdCancelacion.Text;
            comando.CommandText = query;
            comando.ExecuteNonQuery();
            conexion.Close();

            MessageBox.Show("Cita Cancelada, Edo 'C'.");
            cmdBuscar_Click(null, null);

            cboIdCancelacion.Text = "";
        }
            

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}


