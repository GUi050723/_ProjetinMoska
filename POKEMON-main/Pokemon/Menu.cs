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
using System.Reflection.Emit;

namespace Pokemon
{

    public partial class Menu : Form
    {
        private List<Personagem> personagens = new List<Personagem> {};
        private int indiceSelecionado = -1;

        private void LstResultados_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = lstResultados.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    lstResultados.SelectedIndex = index;
                    indiceSelecionado = index;

                    contextMenuStrip1.Show(lstResultados, e.Location);
                }
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (indiceSelecionado >= 0 && indiceSelecionado < personagens.Count)
            {
                Personagem selecionado = personagens[indiceSelecionado];
                Form editar = new Editar(selecionado);
                editar.Show();
                this.Hide();
            }
        }

        public Menu()
        {
            InitializeComponent();
            this.Load += Evento_CarregarPersonagens;
            lstResultados.MouseDown += LstResultados_MouseDown;
        }

        private void AtualizarLista(List<Personagem> lista)
        {
            lstResultados.Items.Clear();
            foreach (var p in lista)
            {
                lstResultados.Items.Add(p.ExibirResumo());
            }
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

        private void Evento_CarregarPersonagens(object sender, EventArgs e)
        {
            CarregarPersonagens();
            AtualizarLista(personagens);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string termo = txtBusca.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(termo))
            {
                AtualizarLista(personagens); // Se limpar o campo, mostra tudo
                return;
            }

            var resultados = personagens.Where(p =>
            {
                string nome = p.Nome.ToLower();
                if (nome.Length < termo.Length)
                    return false;

                for (int i = 0; i < termo.Length; i++)
                {
                    if (nome[i] != termo[i])
                        return false;
                }

                return true;
            }).ToList();

            AtualizarLista(resultados);

            if (resultados.Count == 0)
            {
                MessageBox.Show("Nenhum personagem encontrado.", "Busca", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void editarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            editarToolStripMenuItem_Click(sender, e);
        }
    }
}