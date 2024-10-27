using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class formulario : Form
    {

        public bool alterado;
        public formulario()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileAbrir.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            alterado = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            corpoBloco
        }
    }
}
