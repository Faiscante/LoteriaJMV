using Funcionalidades.Interfaces;
using Funcionalidades.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Funcionalidades
{
    internal class Program
    {
        static List<PessoaJogo> pessoaJogos = new List<PessoaJogo>();

        public enum TipoJogos { megasena = 1, lotofacil, Quina, Lotomania, Timemania, Listar, Encerrar };

        static void Main(string[] args)
        {
            #region CodigoMenu

            CarregarArquivo();

            bool encerrarAplicativo = false;

            Console.WriteLine("---- BEM VINDO A LOTERIA POLO OLYMPIKUS ---- \n ");

            Console.WriteLine("Ola !!, Vamos Fazer o seu registro\ndigite o seu nome: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Quanto dinheiro você teria para apostar ??");
            decimal saldo = decimal.Parse(Console.ReadLine());

            //While para manter o codigo em Loop ate que seja encerrado
            while (!encerrarAplicativo)
            {
                //Opções que vão ser passadas ao usuario para escolha
                Console.WriteLine("\nGerenciador de loteria\nEscolha a modalidade que você quer jogar");
                Console.WriteLine("\n1 - MegaSena" +
                    "\n2 - Lotofácil" +
                    "\n3 - Quina" +
                    "\n4 - Lotomania" +
                    "\n5 - Timemania" +
                    "\n6 - Listar Jogos" +
                    "\n7 - Encerrar Aplicativo");

                try
                {
                    //Realizar a conversão de string passada pelo usuario para inteiro,
                    //e depois faz um conversão explicita para o tipo enum(TipoJogos) Que será atribuido na variavel (escolha)
                    TipoJogos escolha = (TipoJogos)int.Parse(Console.ReadLine());

                    //Verifica se o numero passado está dentro das opções disponiveis
                    //Recebe o numero convertido e executa uma das opções do switch executando o codigo de cada tipo de loteria
                    switch (escolha)
                        {
                            case TipoJogos.megasena:
                                MegaSena(saldo, nome);
                                break;
                            case TipoJogos.lotofacil:
                                LotoFacil(saldo, nome);
                                break;
                            case TipoJogos.Quina:
                                Quina(saldo, nome);
                                break;
                            case TipoJogos.Lotomania:
                                Lotomania(saldo, nome);
                                break;
                            case TipoJogos.Timemania:
                                Timemania(saldo, nome);
                                break;
                            case TipoJogos.Listar:
                                Listagem();
                                break;
                            case TipoJogos.Encerrar:
                                    encerrarAplicativo = true;
                                    break;
                            default:
                                throw new Exception("Opção numerica invalida !!");

                         }

                }
                //Se o usuario passar um caracter que não seja numero, lança uma Exceção e recomeça o processo de escolha
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Pressione Enter para Tentar Novamente");
                    Console.ReadKey();
                    Console.Clear();
                }                             
            }
            #endregion
        }

        #region Codigo MegaSena
        static void MegaSena(Decimal saldo, string nome)
        {
            decimal precoJogo = 5.00m;
            string tipoJogo = "MegaSena";

            //Realiza o calculo de quantos jogos serão realizar recebendo o (saldo) passado pelo usuario e o valor de cada jogo,
            //Depois utiliza o Math.Flooar para converter o resultado em um inteiro, que são a quantidade de jogo gerados
            int QtJogadas = (int)Math.Floor(saldo / precoJogo);

            Console.WriteLine($"O valor da mega-sena atual e de {precoJogo:C} com o saldo de {saldo:C} " +
                              $"Você podera realizar {QtJogadas} Jogos");

            //Recebe a quantidade maxima de numero que poderão ser selecionados
            int[] numerosLoteria = new int[60];

            //Recebe a quantidade de numero que vão ser selecionados
            int[] NumerosJogador = new int[6];

            //Estancia um metodo randomico para a escolha de numeros
            Random numRandom = new Random();

            //Cria um loop que gerá varios jogos, ate a (quantidadeJogos) ser atingida
            for (int jogo = 1; jogo <= QtJogadas; jogo++)
            {
                Console.WriteLine($"{nome} Jogo {jogo}:");

                //Cria um loop for que e utilizado para inicializar o array (numerosLoteria).
                for (int i = 0; i < 60; i++)
                {
                    numerosLoteria[i] = i + 1;
                }

                //Cria um loop que seleciona os 6 numeros aleatorios
                for (int selecionado = 0; selecionado < 6; selecionado++)
                {
                    //atribui a variavel (numeroSorteados), os numeros randomizados, depois ele verifica se o (numeroSorteado), ja está no (seusNumeros),
                    //caso ja tenhas sido escolhido ele vai para o do-while, que repete o processo e escolhe outro numero,
                    //quando acha um novo numero que não foi escolhido ele armazena no seusNumeros.
                    int numeroSorteado;
                    do
                    {
                        numeroSorteado = numerosLoteria[numRandom.Next(60)];
                    } while (Array.IndexOf(NumerosJogador, numeroSorteado) != -1);

                    NumerosJogador[selecionado] = numeroSorteado;
                }

                //Percorre o Array do (seusNumeros) selecionados aleatoriamente e imprime no console
                Console.WriteLine("Seus números da loteria são: ");

                for (int j = 0; j < 6; j++)
                {
                    Console.Write($" - {NumerosJogador[j]}");
                }

                int[] Numeros = new int[6];
                Array.Copy(NumerosJogador, Numeros, 6);

                PessoaJogo pessoaJogo = new PessoaJogo(nome,saldo, tipoJogo, Numeros, QtJogadas);
                pessoaJogos.Add(pessoaJogo);
                Console.ReadLine();
                SalvarArquivo();
            }
            Console.Clear();

        }
        #endregion

        #region Codigo LotoFacil
        static void LotoFacil(Decimal saldo, string nome)
        {
            decimal precoJogo = 3.00m;
            string tipoJogo = "LotoFacil";

            int QtJogadas = (int)Math.Floor(saldo / precoJogo);

            Console.WriteLine($"O valor da LotoFacil atual e de {precoJogo:C} com o saldo de {saldo:C} " +
                              $"Você podera realizar {QtJogadas} Jogos");

            int[] numerosLoteria = new int[20];

            int[] NumerosJogador = new int[15];

            Random numRandom = new Random();

            for (int jogo = 1; jogo <= QtJogadas; jogo++)
            {
                Console.WriteLine($"{nome} Jogo {jogo}:");

                for (int i = 0; i < 20; i++)
                {
                    numerosLoteria[i] = i + 1;
                }

                for (int selecionado = 0; selecionado < 15; selecionado++)
                {
                    int numeroSorteado;
                    do
                    {
                        numeroSorteado = numerosLoteria[numRandom.Next(20)];
                    } while (Array.IndexOf(NumerosJogador, numeroSorteado) != -1);

                    NumerosJogador[selecionado] = numeroSorteado;
                }

                Console.WriteLine("Seus números da loteria são: ");

                for (int j = 0; j < 15; j++)
                {
                    Console.Write($" - {NumerosJogador[j]}");
                }

                int[] Numeros = new int[15];
                Array.Copy(NumerosJogador, Numeros, 15);

                PessoaJogo pessoaJogo = new PessoaJogo(nome, saldo, tipoJogo, Numeros, QtJogadas);
                pessoaJogos.Add(pessoaJogo);
                Console.ReadLine();
                SalvarArquivo();

            }
            Console.Clear();

        }
        #endregion

        #region Codigo Quina
        static void Quina(Decimal saldo, string nome)
        {
            decimal precoJogo = 2.50m;
            string tipoJogo = "Quina";

            int QtJogadas = (int)Math.Floor(saldo / precoJogo);

            Console.WriteLine($"O valor da Quina atual e de {precoJogo:C} com o saldo de {saldo:C} " +
                              $"Você podera realizar {QtJogadas} Jogos");

            int[] numerosLoteria = new int[80];

            int[] NumerosJogador = new int[5];

            Random numRandom = new Random();

            for (int jogo = 1; jogo <= QtJogadas; jogo++)
            {
                Console.WriteLine($"{nome}  Jogo {jogo}:");

                for (int i = 0; i < 80; i++)
                {
                    numerosLoteria[i] = i + 1;
                }

                for (int selecionado = 0; selecionado < 5; selecionado++)
                {
                    int numeroSorteado;
                    do
                    {
                        numeroSorteado = numerosLoteria[numRandom.Next(80)];
                    } while (Array.IndexOf(NumerosJogador, numeroSorteado) != -1);

                    NumerosJogador[selecionado] = numeroSorteado;
                }

                Console.WriteLine("Seus números da loteria são: ");

                for (int j = 0; j < 5; j++)
                {
                    Console.Write($" - {NumerosJogador[j]}");
                }

                int[] Numeros = new int[5];
                Array.Copy(NumerosJogador, Numeros, 5);

                PessoaJogo pessoaJogo = new PessoaJogo(nome, saldo, tipoJogo, Numeros, QtJogadas);
                pessoaJogos.Add(pessoaJogo);
                Console.ReadLine();
                SalvarArquivo();
            }
            Console.Clear();
        }
        #endregion

        #region Codigo LotoMania
        static void Lotomania(Decimal saldo, string nome)
        {
            decimal precoJogo = 3.00m;
            string tipoJogo = "LotoMania";

            int QtJogadas = (int)Math.Floor(saldo / precoJogo);

            Console.WriteLine($"O valor da Lotomania atual e de {precoJogo:C} com o saldo de {saldo:C} " +
                              $"Você podera realizar {QtJogadas} Jogos");

            int[] numerosLoteria = new int[99];

            int[] NumerosJogador = new int[50];

            Random numRandom = new Random();

            for (int jogo = 1; jogo <= QtJogadas; jogo++)
            {
                Console.WriteLine($"{nome}  Jogo {jogo}:");

                for (int i = 0; i < 99; i++)
                {
                    numerosLoteria[i] = i + 1;
                }

                for (int selecionado = 0; selecionado < 50; selecionado++)
                {
                    int numeroSorteado;
                    do
                    {
                        numeroSorteado = numerosLoteria[numRandom.Next(99)];
                    } while (Array.IndexOf(NumerosJogador, numeroSorteado) != -1);

                    NumerosJogador[selecionado] = numeroSorteado;
                }

                Console.WriteLine("Seus números da loteria são: ");

                for (int j = 0; j < 50; j++)
                {
                    Console.Write($" - {NumerosJogador[j]}");
                }
                Console.ReadLine();


                int[] Numeros = new int[50];
                Array.Copy(NumerosJogador, Numeros, 50);

                PessoaJogo pessoaJogo = new PessoaJogo(nome, saldo, tipoJogo, Numeros, QtJogadas);
                pessoaJogos.Add(pessoaJogo);
                Console.ReadLine();
                SalvarArquivo();


            }
            Console.Clear();
        }
        #endregion

        #region Codigo Timemania
        static void Timemania(Decimal saldo, string nome)
        {

            Console.WriteLine("Escreva o nome de um time ??");
            string time = Console.ReadLine();

            decimal precoJogo = 3.50m;
            string tipoJogo = "TomeMania";

            int QtJogadas = (int)Math.Floor(saldo / precoJogo);

            Console.WriteLine($"O valor da Timemania atual e de {precoJogo:C} com o saldo de {saldo:C} " +
                              $"Você podera realizar {QtJogadas} Jogos");

            int[] numerosLoteria = new int[80];

            int[] NumerosJogador = new int[10];

            Random numRandom = new Random();

            for (int jogo = 1; jogo <= QtJogadas; jogo++)
            {
                Console.WriteLine($"{nome}  Jogo {jogo}:");

                for (int i = 0; i < 80; i++)
                {
                    numerosLoteria[i] = i + 1;
                }

                for (int selecionado = 0; selecionado < 10; selecionado++)
                {
                    int numeroSorteado;
                    do
                    {
                        numeroSorteado = numerosLoteria[numRandom.Next(80)];
                    } while (Array.IndexOf(NumerosJogador, numeroSorteado) != -1);

                    NumerosJogador[selecionado] = numeroSorteado;
                }

                Console.WriteLine($"Você escolhe o seu time do coração: {time}\nSeus números da loteria são: ");

                for (int j = 0; j < 10; j++)
                {
                    Console.Write($" - {NumerosJogador[j]}");
                }

                int[] Numeros = new int[10];
                Array.Copy(NumerosJogador, Numeros, 10);

                PessoaJogo pessoaJogo = new PessoaJogo(nome, saldo, tipoJogo, Numeros, QtJogadas,time);
                pessoaJogos.Add(pessoaJogo);
                Console.ReadLine();
                SalvarArquivo();

            }
            Console.Clear();
        }
        #endregion

        static void Listagem()
        {
            Console.WriteLine("Lista de Todos os Jogos:\n");
            int i = 0;
            foreach (IPessoaJogo pessoa in pessoaJogos)
            {
                Console.WriteLine("Id: " + i);
                pessoa.Exibir();
                i++;
                Console.ReadLine();
            }
        }

        static void SalvarArquivo()
        {
            FileStream stream = new FileStream("ListaJogadores.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, pessoaJogos);
            stream.Close();
        }

        static void CarregarArquivo()
        {
            FileStream stream = new FileStream("ListaJogadores.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            try
            {
                pessoaJogos = (List<PessoaJogo>)encoder.Deserialize(stream);

                if (pessoaJogos == null)
                {
                    pessoaJogos = new List<PessoaJogo>();
                }
            }
            catch (Exception ex)
            {
                pessoaJogos = new List<PessoaJogo>();
            }
            stream.Close();
        }       
    }
}

