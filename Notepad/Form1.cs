﻿using System;
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
                if(MessageBox.Show("O arquivo atual foi alterado.\nDeseja Salvar?", "Bloco de Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(this.Text != "")
                    {
                        //significa que já existe um arquivo aberto
                        salvarArquivo(this.Text);
                    }
                    else
                    {
                        salvarComo();
                    }
                    
                }
                else
                {
                    abrirArquivo();
                }
            }
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

    }
}
