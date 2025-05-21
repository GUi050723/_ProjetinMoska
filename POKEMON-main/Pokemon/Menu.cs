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
using System.Web;

namespace Pokemon
{

    public partial class Menu : Form
    {
        private List<PersonagemComIndice> personagensComIndices = new List<PersonagemComIndice>();
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
            if (indiceSelecionado >= 0 && indiceSelecionado < personagensComIndices.Count)
            {
                Personagem selecionado = personagensComIndices[indiceSelecionado].Personagem;
                int indiceArquivo = personagensComIndices[indiceSelecionado].IndiceArquivo;
                Form Editar = new Editar(selecionado, indiceArquivo);
                Editar.Show();
                this.Hide();
            }
        }

        public Menu()
        {
            InitializeComponent();
            this.Load += Evento_CarregarpersonagensComIndices;
            lstResultados.MouseDown += LstResultados_MouseDown;
        }

        private void AtualizarLista(List<PersonagemComIndice> lista)
        {
            lstResultados.Items.Clear();
            foreach (var p in lista)
            {
                lstResultados.Items.Add(p); 
            }
        }

        private void CarregarpersonagensComIndices()
        {
            personagensComIndices.Clear();
            if (File.Exists("pokemoninfo.txt"))
            {
                var linhas = File.ReadAllLines("pokemoninfo.txt");
                for (int i = 0; i < linhas.Length; i++)
                {
                    var partes = linhas[i].Split(';');
                    if (partes.Length >= 9)
                    {
                        string nome = partes[0];
                        string tipo = partes[1];
                        string raca = partes[2];
                        int nivel = int.Parse(partes[3]);
                        string imagemPath = partes[8];
                        string[] movimentos = new string[] { partes[4], partes[5], partes[6], partes[7] };
                        Personagem p = new Personagem(nome, tipo, raca, nivel, imagemPath, movimentos);
                        personagensComIndices.Add(new PersonagemComIndice(p, i));
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

        private void Evento_CarregarpersonagensComIndices(object sender, EventArgs e)
        {
            CarregarpersonagensComIndices();
            AtualizarLista(personagensComIndices);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string termo = txtBusca.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(termo))
            {
                AtualizarLista(personagensComIndices); 
                return;
            }

            var resultados = personagensComIndices.Where(p =>
            {
                string nome = p.Personagem.Nome.ToLower();
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

    public class PersonagemComIndice
    {
        public Personagem Personagem { get; set; }
        public int IndiceArquivo { get; set; }

        public PersonagemComIndice(Personagem personagem, int indice)
        {
            Personagem = personagem;
            IndiceArquivo = indice;
        }

        public override string ToString()
        {
            return Personagem.ExibirResumo();
        }
    }
}