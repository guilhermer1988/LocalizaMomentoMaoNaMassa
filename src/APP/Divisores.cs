using Modelos.Interfaces;
using Modelos.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace APP
{
    public class Divisores : IDivisores
    {
        /// <summary>
        /// Baseado na peneira de Eratótenes o método começa do divisor 2 e se o número for divisível ele 
        /// adiciona o divisor e o resultado à lista de divisores e remove ambos da lista de supostos divisores além 
        /// de remover todos os números maiores que o resultado e ao final ele adiciona o divisor 1 e o próprio número
        /// para verificar se o divisor é primo uso da recursividade e se o resultado do primo for 2 eu tenho certeza que o número é primo
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public string EncontrarDivisores(int numero)
        {
            List<int> supostosDivisores = Enumerable.Range(2, numero - 2).ToList();

            List<Divisor> divisores = new List<Divisor>();

            while (supostosDivisores.Count != 0)
            {
                if (numero % supostosDivisores[0] == 0)
                {
                    var divisor = supostosDivisores[0];
                    var resultado = numero / divisor;

                    var divisorA = new Divisor()
                    {
                        Numero = divisor,
                        EhPrimo = VerificarPrimo(divisor)
                    };

                    var divisorB = new Divisor()
                    {
                        Numero = resultado,
                        EhPrimo = VerificarPrimo(resultado)
                    };

                    if (!divisores.Any(x => x.Numero == divisorA.Numero))
                        divisores.Add(divisorA);

                    if (!divisores.Any(x => x.Numero == divisorB.Numero))
                        divisores.Add(divisorB);

                    var indexFinal = supostosDivisores.IndexOf(divisorB.Numero);
                    supostosDivisores.RemoveRange(indexFinal, supostosDivisores.Count - indexFinal);
                }
                if (supostosDivisores.Count > 0)
                    supostosDivisores.RemoveAt(0);
            }

            divisores.Add(new Divisor()
            {
                Numero = numero,
                EhPrimo = divisores.Count == 0
            });

            divisores.Add(new Divisor()
            {
                Numero = 1,
                EhPrimo = false
            });

            return JsonConvert.SerializeObject(divisores.OrderBy(x => x.Numero).ToList());
        }

        public bool VerificarPrimo(int numero)
        {
            List<int> supostosDivisores = Enumerable.Range(2, numero).ToList();

            foreach (var item in supostosDivisores)
            {
                if (numero % item == 0 && numero != item)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
