using Funcionalidades.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Funcionalidades.Models
{
    [Serializable]
    internal class PessoaJogo : IPessoaJogo
    {
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public string TipoJogo {  get; set; }

        public string Time { get; set; } = null;
        public int QtJogadas {  get; set; }
        public int[] NumerosJogador { get; set; }
        public DateTime DataJogo {  get; set; }

        public PessoaJogo(string nome, decimal saldo, string TipoJogo, int[] NumerosJogados, int QtJogadas)
        {
            this.Nome = nome;
            this.Saldo = saldo;
            this.TipoJogo = TipoJogo;
            this.NumerosJogador = NumerosJogados;
            this.QtJogadas = QtJogadas;
        }

        public PessoaJogo(string nome, decimal saldo, string TipoJogo, int[] NumerosJogados, int QtJogadas, string Time)
        {
            this.Nome = nome;
            this.Saldo = saldo;
            this.TipoJogo = TipoJogo;
            this.NumerosJogador = NumerosJogados;
            this.QtJogadas = QtJogadas;
            this.Time = Time;
        }

        public void Exibir()
        {
            string numerosJogadorString = string.Join(", ", NumerosJogador);

            DataJogo = DateTime.Now;
            Console.WriteLine($"\nNome do jogador: {Nome}" +
                $"\nCom o saldo de: {Saldo}" +
                $"\nQuantidade de jogos feitos: {QtJogadas}" +
                $"\nTipo de Jogo: {TipoJogo}" +
                $"\nNumeros Sorteados: {numerosJogadorString}" );

            if(Time != null)
            {
                Console.WriteLine($"Time do coração: {Time}");
            }

        }
    }
}


