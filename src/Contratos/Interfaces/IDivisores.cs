using Modelos.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Interfaces
{
    public interface IDivisores
    {
        string EncontrarDivisores(int numero);

        bool VerificarPrimo(int numero);
    }
}
