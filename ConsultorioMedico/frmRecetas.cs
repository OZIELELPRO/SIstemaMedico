using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ConsultorioMedico
{
    public partial class frmRecetas : Form
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        SqlDataReader lector;

        public frmRecetas()
        {
            InitializeComponent();
        }

        private void frmRecetas_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText =
                "SELECT DISTINCT P.idPaciente, " +
                "P.Nombre + ' ' + P.ApellidoPaterno AS NombreCompleto " +
                "FROM Paciente P " +
                "INNER JOIN Citas C ON P.idPaciente = C.idPaciente " +
                "WHERE C.estado = 'A';";

            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                cboNombrePac.Items.Add(lector.GetValue(1).ToString());
            }
            lector.Close();
            conexion.Close();
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            string nombreSeleccionado = cboNombrePac.SelectedItem.ToString();

            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText =
                "SELECT TOP 1 " +
                "P.idPaciente, P.Nombre, P.ApellidoPaterno, P.telefono, P.fechaNac, " +
                "P.genero, P.tipoSangre, P.enfermedadCroni, P.alergias, " +
                "C.idCita, C.fecha AS FechaCita, " +
                "E.idExpediente, E.peso, E.estatura, E.temperatura, E.presion, E.sintomas, E.diagnostico " +
                "FROM Paciente P " +
                "INNER JOIN Citas C ON P.idPaciente = C.idPaciente " +
                "INNER JOIN Expediente E ON C.idCita = E.idCita " +
                "WHERE P.Nombre + ' ' + P.ApellidoPaterno = '" + nombreSeleccionado + "' " +
                "AND C.estado = 'A' " +
                "ORDER BY E.idExpediente DESC";

            lector = comando.ExecuteReader();

            if (lector.Read())
            {
                txtIdPac.Text = lector.GetValue(0).ToString();
                txtNombre.Text = lector.GetValue(1).ToString() + " " + lector.GetValue(2).ToString();
                txtTelefono.Text = lector.GetValue(3).ToString();
                txtFechaNac.Text = lector.GetValue(4).ToString();
                txtGenero.Text = lector.GetValue(5).ToString();
                txtTipoSan.Text = lector.GetValue(6).ToString();
                txtEnfermedades.Text = lector.GetValue(7).ToString();
                txtAlergias.Text = lector.GetValue(8).ToString();
                txtIdCita.Text = lector.GetValue(9).ToString();
                txtFechaCita.Text = lector.GetValue(10).ToString();
                txtIdExpe.Text = lector.GetValue(11).ToString();
                txtPeso.Text = lector.GetValue(12).ToString();
                txtEstatura.Text = lector.GetValue(13).ToString();
                txtTemperatura.Text = lector.GetValue(14).ToString();
                txtPresion.Text = lector.GetValue(15).ToString();
                txtSintomas.Text = lector.GetValue(16).ToString();
                txtDiagnostico.Text = lector.GetValue(17).ToString();
            }

            lector.Close();
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMedicamento.Text) ||
                string.IsNullOrWhiteSpace(txtIndicaciones.Text))
            {
                MessageBox.Show("Completa los campos");
                return;
            }

            dgvDatos.Rows.Add(txtMedicamento.Text, txtIndicaciones.Text);

            txtMedicamento.Clear();
            txtIndicaciones.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText =
                "INSERT INTO Receta (idExpediente) " +
                "VALUES (" + txtIdExpe.Text + "); " +
                "SELECT SCOPE_IDENTITY();";

            int idReceta = Convert.ToInt32(comando.ExecuteScalar());

            int consecutivo = 1;
            int i = 0;
            while (i < dgvDatos.Rows.Count)
            {
                if (dgvDatos.Rows[i].Cells[0].Value != null)
                {
                    string medicamento = dgvDatos.Rows[i].Cells[0].Value.ToString();
                    string indicaciones = dgvDatos.Rows[i].Cells[1].Value.ToString();

                    comando = conexion.CreateCommand();
                    comando.CommandText =
                        "INSERT INTO Det_Receta (idReceta, idConsecutivo, medicamentos, indicaciones) " +
                        "VALUES (" + idReceta + ", " + consecutivo + ", '" + medicamento + "', '" + indicaciones + "')";

                    comando.ExecuteNonQuery();
                    consecutivo++;
                }
                i++;
            }

            int idCita = Convert.ToInt32(txtIdCita.Text);
            comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE Citas SET Estado = 'M' WHERE idCita = " + idCita;
            comando.ExecuteNonQuery();

            MessageBox.Show("Receta guardada correctamente");

            dgvDatos.Rows.Clear();
            cboNombrePac.SelectedIndex = -1;
            txtIdPac.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtFechaNac.Clear();
            txtGenero.Clear();
            txtTipoSan.Clear();
            txtEnfermedades.Clear();
            txtAlergias.Clear();
            txtIdCita.Clear();
            txtFechaCita.Clear();
            txtIdExpe.Clear();
            txtPeso.Clear();
            txtEstatura.Clear();
            txtTemperatura.Clear();
            txtPresion.Clear();
            txtSintomas.Clear();
            txtDiagnostico.Clear();
            txtMedicamento.Clear();
            txtIndicaciones.Clear();

            conexion.Close();
        }
    }
}
