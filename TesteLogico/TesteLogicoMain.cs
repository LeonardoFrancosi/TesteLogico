using System;
using System.Collections.Generic;
using System.Linq;

public class Numeros
{
    public List<int> ListaNumeros {get; private set;}
    public List<KeyValuePair<int,int>> ListaConexao {get; set;}

    public Numeros(string input){
        ListaNumeros = input.Split(',').Select(s => int.Parse(s.Trim())).ToList();
        ListaConexao = new List<KeyValuePair<int, int>>();
    }

    public void MostrarNumeros()
    {
        Console.WriteLine("Números na lista: " + string.Join(", ", ListaNumeros));
    }

    public void ConectarNumeros(int a, int b)
    {
        ListaConexao.Add(new KeyValuePair<int, int>(a, b));
    }

    public bool Consulta(int a, int b)
    {
        if (a == b)
        {
           return true;     
        }

        foreach (var conexao in ListaConexao)
        {
            if(conexao.Key == a)
            {
                if (Consulta(conexao.Value, b)){
                    return true;
                }
            }
        }

        return false;
    }

}

public class Program 
{
    private static Exception e;

    static void Main(string[] args)
    {
        Console.WriteLine("Escreva o conjunto de Números separados por vírgula:");
        string input = Console.ReadLine();
        
        try 
        {
            Numeros ListaDeNumeros = new Numeros(input);

            // ListaDeNumeros.MostrarNumeros();

            bool continua = true;
            while (continua)
            {
                Console.WriteLine("Deseja conectar ou consultar? Caso queira parar apenas aperte Enter.");
                string resposta = Console.ReadLine();

                if (string.IsNullOrEmpty(resposta))
                {
                    continua = false;
                }
                else if (resposta.ToLower() == "conectar")
                {
                    Console.WriteLine("Quais números quer conectar? siga o formato exemplo: 1-2, 6-2, 2-4");
                    string NumerosConecta = Console.ReadLine();
                    
                    try
                    {
                        List<string> Conexoes = new List<string>();
                        Conexoes = NumerosConecta.Split(',').ToList();

                        foreach (string conexao in Conexoes)
                        {
                            List<int> Valores = conexao.Split('-').Select(s => int.Parse(s.Trim())).ToList();
                            if (Valores.Count > 2){
                                throw e;
                            }
                            else
                            {
                                ListaDeNumeros.ConectarNumeros(Valores[0], Valores[1]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Sequência inválida.");
                    }


                }
                else if (resposta.ToLower() == "consultar"){
                    Console.WriteLine("Quais números deseja consultar se estão conectados? digite-os separando por vírgula.");
                    string NumerosConsulta = Console.ReadLine();

                    try
                    {
                        List<int> Valores = NumerosConsulta.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                        if (Valores.Count > 2){
                            throw e;
                        }
                        else
                        {
                            bool Existe = ListaDeNumeros.Consulta(Valores[0], Valores[1]) || ListaDeNumeros.Consulta(Valores[1], Valores[0]);
                            if (Existe)
                            {
                                Console.WriteLine($"Existe conexão entre {Valores[0]} e {Valores[1]}!");        
                            }
                            else
                            {
                                Console.WriteLine($"Caminho de conexão entre {Valores[0]} e {Valores[1]} não encontrado.");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Sequência inválida.");
                    }                    
                }
                else { 
                    Console.WriteLine($"\"{resposta}\" não é uma opção inválida.");
                }
            }
            
        }
        catch (Exception)
        {
            Console.WriteLine("Valor inserido não está adequado ao programa, por favor digite uma sequência válida de números separados por vírgula.");
        }
    }
}