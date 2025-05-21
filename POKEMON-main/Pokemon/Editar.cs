using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace Pokemon
{
    public partial class Editar : Form
    {
        private Personagem personagem = new Personagem();
        private string nomeOriginal;
        private int Indice;
        public Editar(Personagem p, int indice)
        {
            InitializeComponent();
            personagem = p;
            nomeOriginal = p.Nome;
            Indice = indice;

            textBox1.Text = personagem.Nome;
            textBox2.Text = personagem.Tipo;
            textBox3.Text = personagem.Raca;
            textBox4.Text = personagem.Nivel.ToString();

            textBox5.Text = personagem.Movimentos.Length > 0 ? personagem.Movimentos[0] : "";
            textBox6.Text = personagem.Movimentos.Length > 1 ? personagem.Movimentos[1] : "";
            textBox7.Text = personagem.Movimentos.Length > 2 ? personagem.Movimentos[2] : "";
            textBox8.Text = personagem.Movimentos.Length > 3 ? personagem.Movimentos[3] : "";
            if (!string.IsNullOrEmpty(personagem.ImagemPath) && File.Exists(personagem.ImagemPath))
            {
                pictureBox1.ImageLocation = personagem.ImagemPath;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else pictureBox1.Image = null;
    
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form Menu = new Menu();
            Menu.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            personagem.Nome = textBox1.Text;
            personagem.Tipo = textBox2.Text;
            personagem.Raca = textBox3.Text;

            if (int.TryParse(textBox4.Text, out int nivel)) personagem.Nivel = nivel;

            personagem.Movimentos = new string[]{textBox5.Text, textBox6.Text, textBox7.Text, textBox8.Text };

            personagem.ImagemPath = pictureBox1.ImageLocation ?? "";

            if (!personagem.ValidarTodosOsCampos())
            {
                MessageBox.Show("Corrija os erros antes de salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AtualizarArquivo(personagem);
        }

        private void AtualizarArquivo(Personagem personagemAtualizado)
        {
            var linhas = File.ReadAllLines("pokemoninfo.txt").ToList();

            if (Indice >= 0 && Indice < linhas.Count)
            {
                linhas[Indice] = $"{personagem.Nome};{personagem.Tipo};{personagem.Raca};{personagem.Nivel};{personagem.Movimentos[0]};{personagem.Movimentos[1]};{personagem.Movimentos[2]};{personagem.Movimentos[3]};{personagem.ImagemPath}";
            }

            File.WriteAllLines("pokemoninfo.txt", linhas);
            label8.Text = "Personagem criado com sucesso";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Selecione uma imagem";
            openFileDialog.Filter = "Imagens (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                if (personagem != null)
                    personagem.ImagemPath = openFileDialog.FileName;
            }
        }
    }
}
