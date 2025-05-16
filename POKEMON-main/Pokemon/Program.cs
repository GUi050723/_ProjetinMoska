using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Pokemon
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
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

        public Personagem() {} 

        public Personagem(string nome, string tipo, string raca, int nivel, string imagemPath, string[] movimentos)
        {
            nome = nome;
            tipo = tipo;
            raca = raca;
            nivel = nivel;
            imagemPath = imagemPath;
            this.movimentos = movimentos;
        }

       public string Nome
    {
        get => nome;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome não pode ser vazio.");
            nome = value;
        }
    }

    public string Tipo
    {
        get => tipo;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tipo não pode ser vazio.");
            tipo = value;
        }
    }

    public string Raca
    {
        get => raca;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Raça não pode ser vazia.");
            raca = value;
        }
    }


    public int Nivel
    {
        get => nivel;
        set
        {
            if (value < 1 || value > 100)
                throw new ArgumentOutOfRangeException("Nível deve estar entre 1 e 100.");
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
        return $"Nome: {Nome}\n" +
               $"Tipo: {Tipo}\n" +
               $"Raça: {Raca}\n" +
               $"Nível: {Nivel}\n" +
               $"Movimentos: {string.Join(", ", Movimentos)}\n" +
               $"Imagem: {ImagemPath}";
        }
    }
}
