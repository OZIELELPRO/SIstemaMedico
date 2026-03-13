using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsultorioMedico.SistemaMedicoDataSet1TableAdapters;

namespace ConsultorioMedico
{
    public partial class frmMedicos : Form
    {
        public frmMedicos()
        {
            InitializeComponent();
        }

        private void frmMedicos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaMedicoDataSet3.Medico' Puede moverla o quitarla según sea necesario.
            this.medicoTableAdapter.Fill(this.sistemaMedicoDataSet3.Medico);

        }

        private void cmdAnterior_Click(object sender, EventArgs e)
        {
            medicoBindingSource.MovePrevious();
        }

        private void cmdSiguiente_Click(object sender, EventArgs e)
        {
            medicoBindingSource.MoveNext();
        }

        private void cmdUltimo_Click(object sender, EventArgs e)
        {
            medicoBindingSource.MoveLast();
        }

        private void cmdPrimero_Click(object sender, EventArgs e)
        {
            medicoBindingSource.MoveFirst();
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            txtIdMedico.Enabled = false;
            txtNombre.Enabled = true;
            txtCedula.Enabled = true;
            txtDomicilio.Enabled = true;
            mskTelefono.Enabled = true;
            medicoBindingSource.AddNew();
            cmdNuevo.Enabled = false;
            cmdGrabar.Enabled = true;
            cdmModificar.Enabled = false;
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            this.Validate();
            medicoBindingSource.EndEdit();
            medicoTableAdapter.Update(sistemaMedicoDataSet3.Medico);
            MessageBox.Show("Registro guardado correctamente.");
            txtIdMedico.Enabled = false;
            txtNombre.Enabled = false;
            txtCedula.Enabled = false;
            txtDomicilio.Enabled = false;
            mskTelefono.Enabled = false;
            cmdNuevo.Enabled = true;
            cmdGrabar.Enabled = false;
            cdmModificar.Enabled = true;
        }

        private void cdmModificar_Click(object sender, EventArgs e)
        {
            txtIdMedico.Enabled = false;
            txtNombre.Enabled = true;
            txtCedula.Enabled = true;
            txtDomicilio.Enabled = true;
            mskTelefono.Enabled = true;
            cmdGrabar.Enabled = true;
            cdmModificar.Enabled = false;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
