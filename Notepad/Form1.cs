using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class formulario : Form
    {

        public bool alterado = false;
        public formulario()
        {
            InitializeComponent();
            //garante que não traga nada já escrito ao abrir o bloco de notas
            this.Text = "";

            barraDeStatusToolStripMenuItem1.Checked = true;
            this.atualizaPosicao();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //só deixa abrir o arquivo novo, se o meu texto não foi alterado
            if (!alterado)
            {
                abrirArquivo();
            }
            else
            {
                //mensagem que mostra que foi alteado, com um botão de prosseguir ou cancelar o processo, e um ícone de questionmark
                if (MessageBox.Show("O arquivo atual foi alterado.\nDeseja Salvar?", "Bloco de Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.Text != "")
                    {
                        //significa que já existe um arquivo aberto
                        salvarArquivo(this.Text);
                    }
                    else
                    {
                        salvarComo();
                    }

                }
                abrirArquivo();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            alterado = true;
            atualizaPosicao();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (corpoBloco.SelectedText != "")
            {
                corpoBloco.Copy();
            }
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text != "")
            {
                //significa que já existe um arquivo aberto
                salvarArquivo(this.Text);
            }
            else
            {
                salvarComo();
            }
        }

        private void salvarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            salvarComo();
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            corpoBloco.Undo();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            corpoBloco.Redo();
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (corpoBloco.SelectedText != "")
            {
                corpoBloco.Cut();
            }
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                corpoBloco.Paste();
            }
        }

        private void formulario_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (alterado)
            {
                DialogResult result = MessageBox.Show("O arquivo atual foi alterado.\nDeseja Salvar?", "Bloco de Notas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (this.Text != "")
                    {
                        //significa que já existe um arquivo aberto
                        salvarArquivo(this.Text);
                    }
                    else
                    {
                        salvarComo();
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void selecionarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (corpoBloco.Text != "")
            {
                corpoBloco.SelectAll();
            }
            else
            {
                MessageBox.Show("Não há nada para ser selecionado...", "Selecionar tudo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (alterado)
            {
                DialogResult resultado = MessageBox.Show("O arquivo atual foi alterado.\nDeseja Salvar?", "Bloco de Notas", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (this.Text != "")
                    {
                        //significa que já existe um arquivo aberto
                        salvarArquivo(this.Text);
                    }
                    else
                    {
                        salvarComo();
                    }
                }
                else if (resultado == DialogResult.Cancel)
                {
                    return;
                }
            }

            corpoBloco.Clear();
            this.Text = "";
            alterado = false;
        }

        private void quebraDeLinhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quebraDeLinhaToolStripMenuItem.CheckState == CheckState.Checked)
            {
                //desmarca a caixa e tira o wordwrap
                quebraDeLinhaToolStripMenuItem.CheckState = CheckState.Unchecked;
                corpoBloco.WordWrap = false;
            }
            else
            {
                quebraDeLinhaToolStripMenuItem.CheckState = CheckState.Checked;
                corpoBloco.WordWrap = true;
            }
        }

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dialogFonte.ShowDialog() == DialogResult.OK)
            {
                if (corpoBloco.SelectionLength == 0)
                {
                    corpoBloco.SelectAll();
                }
                corpoBloco.SelectionFont = dialogFonte.Font;
            }
        }

        private void barraDeStatusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (barraDeStatusToolStripMenuItem1.Checked == true)
            {
                stripFooter.Hide();
                barraDeStatusToolStripMenuItem1.Checked = false;
            }
            else
            {
                stripFooter.Show();
                barraDeStatusToolStripMenuItem1.Checked = true;
            }
        }

        /*
         * Funções próprias
         */
        private void abrirArquivo()
        {
            //Valida se foi selecionado OK, ao abrir a tela de seleção do arquivo
            if (openFileAbrir.ShowDialog() == DialogResult.OK)
            {
                //seta o título do formulário, igual ao nome do arquivo
                this.Text = openFileAbrir.FileName;
                //abre o arquivo
                using (StreamReader reader = new StreamReader(openFileAbrir.OpenFile()))
                {
                    //digita o texto do arquivo aberto no meu RichTextBox
                    corpoBloco.Text = reader.ReadToEnd();
                    alterado = false;
                }
            }
        }

        private void salvarArquivo(String arquivo)
        {
            //valida de o arquivo tem ou não nome
            if (arquivo != "")
            {
                //cria arquivo no disco
                StreamWriter buffer = new StreamWriter(arquivo);
                buffer.Write(corpoBloco.Text);
                buffer.Close();
                this.Text = arquivo;
                alterado = false;
            }
            else
            {
                MessageBox.Show("Nome de arquivo inválido!", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvarComo()
        {
            if (salvarDialog.ShowDialog() == DialogResult.OK)
            {
                salvarArquivo(salvarDialog.FileName);
            }
        }

        private void atualizaPosicao()
        {
            int linha = corpoBloco.GetLineFromCharIndex(corpoBloco.SelectionStart)+1;
            int coluna = corpoBloco.SelectionStart - corpoBloco.GetFirstCharIndexOfCurrentLine();
            TSLabelPosicao.Text = $"Ln: {linha.ToString()}, Col: {coluna.ToString()}";
        }
    }
}
