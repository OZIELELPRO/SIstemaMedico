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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void catToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pACIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPacientes frmPacientes = new frmPacientes();

            frmPacientes.Show();
        }

        private void cLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes frmClientes = new frmClientes();    
  
            frmClientes.Show();
        }

        private void mEDICOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMedicos frmMedicos = new frmMedicos();
    
            frmMedicos.Show();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
