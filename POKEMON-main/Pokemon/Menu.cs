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

    public partial class Menu : Form
    {
      //  private List<Personagem> personagens = new List<Personagem>();
        private List<Personagem> personagens = new List<Personagem>
{
    //new Personagem("Ash", "Treinador", "Humano", 10, "caminho/para/imagem1.jpg"),
    //new Personagem("Pikachu", "Elétrico", "Pokémon", 25, "caminho/para/imagem2.jpg")
};


        public Menu()
        {
            InitializeComponent();
            //  personagens.Add(new Personagem("Pikachu"));
            }
            private void Menu_Load(object sender, EventArgs e)
        {
            CarregarPersonagens();
        }
        

        private void CarregarPersonagens()
        {
            personagens.Clear(); // limpa para evitar duplicados
            if (File.Exists("pokemoninfo.txt"))
            {
                var linhas = File.ReadAllLines("pokemoninfo.txt");
                foreach (var linha in linhas)
                {
                    var partes = linha.Split(';');
                    if (partes.Length >= 9)
                    {
                        string nome = partes[0];
                        string tipo = partes[1];
                        string raca = partes[2];
                        int nivel = int.Parse(partes[3]);

                        Personagem p = new Personagem(nome, tipo, raca, nivel);

                        // movimentos
                        for (int i = 0; i < 4; i++)
                        {
                            p.movimentos[i] = partes[4 + i];
                        }

                        // imagem
                        p.imagemPath = partes[8];

                        personagens.Add(p);
                    }
                }
            }
        }

  
        

        private void button1_Click(object sender, EventArgs e)
        {
            Form Form1 = new Cadastro();
            Form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form Form1 = new Editar();
            Form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CarregarPersonagens();

            {
                string termo = txtBusca.Text.Trim().ToLower();

                var resultados = personagens
                    .Where(p => p.Nome.ToLower().Contains(termo))
                    .ToList();

                lstResultados.Items.Clear();

                if (resultados.Count > 0)
                {
                    foreach (var p in resultados)
                    {
                        lstResultados.Items.Add(p.ExibirResumo());
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum personagem encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstResultados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

