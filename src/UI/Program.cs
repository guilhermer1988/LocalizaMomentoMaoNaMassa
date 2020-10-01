using System;
using System.Collections.Generic;
using APP;
using Microsoft.Extensions.DependencyInjection;
using Modelos.Interfaces;
using Modelos.Modelos;
using Newtonsoft.Json;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var servicos = new ServiceCollection();
            ConfiguraServicos(servicos);
            var provedorServicos = servicos.BuildServiceProvider();

            var divisores = provedorServicos.GetService<IDivisores>();

            Console.WriteLine("Iniciando a aplicação!");

            var numero = LerNumero();

            var resultado = JsonConvert.DeserializeObject<List<Divisor>>(divisores.EncontrarDivisores(numero));

            Console.WriteLine($"Os divisores do número {numero} são:");

            foreach (var item in resultado)
            {
                string divisor = $"O divisor { item.Numero } ";
                string ehPrimo = item.EhPrimo ? "é primo." : "não é primo.";
                Console.WriteLine(divisor + ehPrimo);
            }
        }

        private static int LerNumero()
        {
            int numero = 0;
            Console.WriteLine("Digite um número inteiro positivo para encontrar seus divisores! (digite 's' para sair)");
            var texto = Console.ReadLine();

            if (texto.Equals("s") || texto.Equals("S"))
            {
                Console.WriteLine("Finalizando a aplicação");
                Environment.Exit(1);
            }

            if (int.TryParse(texto, out numero) && numero > 1)
                return numero;
            else
                return LerNumero();
        }



        public static void ConfiguraServicos(IServiceCollection services)
        {
            services.AddScoped<IDivisores, Divisores>();
        }
    }
}
