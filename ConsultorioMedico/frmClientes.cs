using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsultorioMedico.SistemaMedicoDataSetTableAdapters;

namespace ConsultorioMedico
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'sistemaMedicoDataSet1.Cliente' Puede moverla o quitarla según sea necesario.
            this.clienteTableAdapter.Fill(this.sistemaMedicoDataSet1.Cliente);
           
        }

        private void cmdAnterior_Click(object sender, EventArgs e)
        {
            clienteBindingSource1.MovePrevious();
        }

        private void cmdSiguiente_Click(object sender, EventArgs e)
        {
            clienteBindingSource1.MoveNext();
        }

        private void cmdUltimo_Click(object sender, EventArgs e)
        {
            clienteBindingSource1.MoveLast();
        }

        private void cmdPrimero_Click(object sender, EventArgs e)
        {
            clienteBindingSource1.MoveFirst();
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            txtIdCliente.Enabled = false;
            txtRFC.Enabled = true;
            txtNombreContri.Enabled = true;
            txtDomFiscal.Enabled = true;
            txtEmail.Enabled = true;
            clienteBindingSource1.AddNew();
            cmdNuevo.Enabled = false;
            cmdGrabar.Enabled = true;
            cdmModificar.Enabled = false;
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            this.Validate();
            clienteBindingSource1.EndEdit();
            clienteTableAdapter.Update(sistemaMedicoDataSet1.Cliente);
            MessageBox.Show("Registro guardado correctamente.");
            txtIdCliente.Enabled = false;
            txtNombreContri.Enabled = false;
            txtRFC.Enabled = false;
            txtDomFiscal.Enabled = false;
            txtEmail.Enabled = false;
            cmdNuevo.Enabled = true;
            cmdGrabar.Enabled = false;
            cdmModificar.Enabled = true;
        }

        private void cdmModificar_Click(object sender, EventArgs e)
        {
            txtIdCliente.Enabled = false;
            txtRFC.Enabled = true;
            txtNombreContri.Enabled = true;
            txtDomFiscal.Enabled = true;
            txtEmail.Enabled = true;
            cmdGrabar.Enabled = true;
            cdmModificar.Enabled = false;
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            clienteBindingSource1.Filter =
        $"nombreContri LIKE '%{txtBusqueda.Text}%'";
        }
    }
}
