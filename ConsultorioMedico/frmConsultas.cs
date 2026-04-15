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
    public partial class frmConsultas : Form
    {

        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();
        public frmConsultas()
        {
            InitializeComponent();
        }

        private void frmConsultas_Load(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Data Source=ROLI124O\\OZIEL;Initial Catalog=SistemaMedico;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";


            dgvDatos.Columns.Clear();


            dgvDatos.Columns.Add("id1", "IDReceta");
            dgvDatos.Columns.Add("id2", "IDConsecutivo");
            dgvDatos.Columns.Add("id3", "IDCita");
            dgvDatos.Columns.Add("id4", "IDExpediente");
            dgvDatos.Columns.Add("hor", "Fecha");
            dgvDatos.Columns.Add("nom", "Hora");
            dgvDatos.Columns.Add("edo", "Estado");
            dgvDatos.Columns.Add("ape", "NomPaciente");
            dgvDatos.Columns.Add("dir", "ApellidoPa");
            dgvDatos.Columns.Add("tel", "Medicamento");
            dgvDatos.Columns.Add("cel", "Indicaciones");


            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cmdOkIndividual_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "") return;

            dgvDatos.Rows.Clear();
            conexion.Open();
            comando = conexion.CreateCommand();

            comando.CommandText = "SELECT R.idReceta, D.idConsecutivo, C.idCita, E.idExpediente, C.fecha, C.hora, C.estado, P.Nombre, P.ApellidoPaterno, D.medicamentos, D.indicaciones " +
                                  "FROM Receta R " +
                                  "INNER JOIN Det_Receta D ON R.idReceta = D.idReceta " +
                                  "INNER JOIN Expediente E ON R.idExpediente = E.idExpediente " +
                                  "INNER JOIN Citas C ON E.idCita = C.idCita " +
                                  "INNER JOIN Paciente P ON C.idPaciente = P.idPaciente " +
                                  "WHERE R.idReceta = " + txtId.Text;

            SqlDataReader lector = comando.ExecuteReader();

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
                    lector.GetValue(7),
                    lector.GetValue(8),
                    lector.GetValue(9),
                    lector.GetValue(10)
                );
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


