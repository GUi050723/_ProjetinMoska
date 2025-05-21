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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace Pokemon
{
    public partial class Cadastro : Form
    {
        private Personagem personagem = new Personagem();
        public Cadastro()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TextBox[] TextBox = new TextBox[] { textBox5, textBox6, textBox7, textBox8 };
            personagem = new Personagem();
            personagem.Nome = textBox1.Text;
            personagem.Tipo = textBox2.Text;
            personagem.Raca = textBox3.Text;

            if (int.TryParse(textBox4.Text, out int nivel))
            {
                personagem.Nivel = nivel;
            } 

            for (int i = 0; i < 4; i++) personagem.Movimentos[i] = TextBox[i].Text;

            personagem.ImagemPath = pictureBox1.ImageLocation ?? "";

            if (!personagem.ValidarTodosOsCampos())
            {
                MessageBox.Show("Corrija os erros antes de cadastrar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            string dadospokemon = $"{personagem.Nome};{personagem.Tipo};{personagem.Raca};{personagem.Nivel};{personagem.Movimentos[0]};{personagem.Movimentos[1]};{personagem.Movimentos[2]};{personagem.Movimentos[3]};{personagem.ImagemPath}\n";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form Menu = new Menu();
            Menu.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
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
