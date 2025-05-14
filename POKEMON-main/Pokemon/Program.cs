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
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Raca { get; set; }
        public int Nivel { get; set; }
        public string[] movimentos = new string[4];
        public string imagemPath;

        // Construtor sem parâmetros
        public Personagem()
        {
        }

        // Construtor com parâmetros
        public Personagem(string nome, string tipo, string raca, int nivel)
        {
            Nome = nome;
            Tipo = tipo;
            Raca = raca;
            Nivel = nivel;
        }

        public string ExibirResumo()
        {
            return $"Nome: {Nome} | Tipo: {Tipo} | Raça: {Raca} | Nível: {Nivel}";
        }

    }
}
