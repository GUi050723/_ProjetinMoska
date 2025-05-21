using System;
using System.Windows.Forms;

namespace Pokemon
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
        }
    }

    public class Personagem
    {
        private string nome;
        private string tipo;
        private string raca;
        private int nivel;
        private string[] movimentos = new string[4];
        private string imagemPath;
        private bool validando = false;
        private bool inicializando = true;
        private bool valido = true;

        public Personagem() => inicializando = false;

        public Personagem(string nome, string tipo, string raca, int nivel, string imagemPath, string[] movimentos)
        {
            inicializando = true;
            Nome = nome;
            Tipo = tipo;
            Raca = raca;
            Nivel = nivel;
            ImagemPath = imagemPath;
            Movimentos = movimentos;
            inicializando = false;
        }

        public string Nome
        {
            get => nome;
            set
            {
                if (validando && string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Nome não pode ser vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                    return;
                }
                nome = value;
            }
        }

        public string Tipo
        {
            get => tipo;
            set
            {
                if (validando && string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Tipo não pode ser vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                    return;
                }
                tipo = value;
            }
        }

        public string Raca
        {
            get => raca;
            set
            {
                if (validando && string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Raça não pode ser vazia.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                    return;
                }
                raca = value;
            }
        }

        public int Nivel
        {
            get => nivel;
            set
            {
                if (validando && (value < 1 || value > 100))
                {
                    MessageBox.Show("Nível deve estar entre 1 e 100.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                    return;
                }
                nivel = value;
            }
        }

        public string ImagemPath
        {
            get => imagemPath;
            set => imagemPath = value ?? "";
        }

        public string[] Movimentos
        {
            get => movimentos;
            set => movimentos = value ?? new string[4];
        }

        public string ExibirResumo()
        {
            return $"Nome: {Nome} \n" +
                   $"Tipo: {Tipo} \n" +
                   $"Raça: {Raca} \n" +
                   $"Nível: {Nivel} \n" +
                   $"Movimentos: {string.Join(", ", Movimentos)} \n" +
                   $"Imagem: {ImagemPath}";
        }

        public bool ValidarTodosOsCampos()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                MessageBox.Show("Nome não pode ser vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Tipo))
            {
                MessageBox.Show("Tipo não pode ser vazio.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Raca))
            {
                MessageBox.Show("Raça não pode ser vazia.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (Nivel < 1 || Nivel > 100)
            {
                MessageBox.Show("Nível deve estar entre 1 e 100.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }

}
