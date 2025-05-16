using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pokemon
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                    TextBox[] TextBox = new TextBox[] {textBox5, textBox6, textBox7, textBox8};
                    Personagem personagem = new Personagem() {
                        personagem.Nome = textBox1.Text;
                        personagem.Tipo = textBox2.Text;
                        personagem.Raca = textBox3.Text;
                    }

                    if (int.TryParse(textBox4.Text, out int nivel)) personagem.Nivel = nivel; else  throw new ArgumentException("O nível deve ser um número entre 1 e 100.");
              
                    for (int i = 0; i < 4; i++) personagem.movimentos[i] = TextBox[i].Text;
                    personagem.imagemPath = "";
                    string dadospokemon = $"{personagem.Nome};{personagem.Tipo};{personagem.Raca};{personagem.Nivel};{personagem.movimentos[0]};{personagem.movimentos[1]};{personagem.movimentos[2]};{personagem.movimentos[3]};{personagem.imagemPath}\n";
                    File.AppendAllText("pokemoninfo.txt", dadospokemon);

                    Clean();
                    label8.Text = "Personagem criado com sucesso";
        }

        private void textBox2_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void textBox3_Keypress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {

        }

        private void Clean() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }

        //imagem
        /*private void btnSelecionarImagem_Click(object sender, EventArgs e)
{
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Title = "Selecione uma imagem do personagem";
        openFileDialog.Filter = "Arquivos de imagem (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
        string imagemSelecionada = openFileDialog.FileName;
        pictureBox1.Image = Image.FromFile(imagemSelecionada);
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        imagemPathSelecionado = imagemSelecionada;
        }*/
    }
}
