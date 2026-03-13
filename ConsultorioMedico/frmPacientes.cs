using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsultorioMedico
{
    public partial class frmPacientes : Form
    {
        public frmPacientes()
        {
            InitializeComponent();
        }

        private void frmPacientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaMedicoDataSet.Paciente' Puede moverla o quitarla según sea necesario.
            this.pacienteTableAdapter.Fill(this.sistemaMedicoDataSet.Paciente);

        }

        private void cmdAnterior_Click(object sender, EventArgs e)
        {
            pacienteBindingSource.MovePrevious();
        }

        private void cmdSiguiente_Click(object sender, EventArgs e)
        {
            pacienteBindingSource.MoveNext();
        }

        private void cmdUltimo_Click(object sender, EventArgs e)
        {
            pacienteBindingSource.MoveLast();
        }

        private void cmdPrimero_Click(object sender, EventArgs e)
        {
            pacienteBindingSource.MoveFirst();
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            txtIdPaciente.Enabled = false;
            txtNombre.Enabled = true;
            txtApellidoP.Enabled = true;
            txtApellidoM.Enabled = true;
            cboGenero.Enabled = true;
            cboSangre.Enabled = true;
            dtpFechaN.Enabled = true;
            mskTelefono.Enabled = true;
            txtAlergias.Enabled = true;
            txtEnfermedades.Enabled = true;
            pacienteBindingSource.AddNew();
            cmdNuevo.Enabled = false;
            cmdGrabar.Enabled = true;
            cmdModificar.Enabled = false;
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            
            this.Validate();
            pacienteBindingSource.EndEdit();
            pacienteTableAdapter.Update(sistemaMedicoDataSet.Paciente);
            MessageBox.Show("Registro guardado correctamente.");
            txtIdPaciente.Enabled = false;
            txtNombre.Enabled = false;
            txtApellidoP.Enabled = false;
            txtApellidoM.Enabled = false;
            cboGenero.Enabled = false;
            dtpFechaN.Enabled = false;
            mskTelefono.Enabled = false;
            txtAlergias.Enabled = false;
            txtEnfermedades.Enabled = false;
            cmdNuevo.Enabled = true;
            cmdGrabar.Enabled = false;
            cmdModificar.Enabled = true;
            cboSangre.Enabled = false;
      
        }

        private void cmdModificar_Click(object sender, EventArgs e)
        {
            txtIdPaciente.Enabled = false;
            txtNombre.Enabled = true;
            txtApellidoP.Enabled = true;
            txtApellidoM.Enabled = true;
            cboGenero.Enabled = true;
            dtpFechaN.Enabled = true;
            mskTelefono.Enabled = true;
            txtAlergias.Enabled = true;
            txtEnfermedades.Enabled = true;
            cboSangre.Enabled = true;
            cmdGrabar.Enabled = true;
            cmdModificar.Enabled = false;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pacienteBindingSource.Filter =
        $"Nombre LIKE '%{textBox1.Text}%'";
        }
    }
}
