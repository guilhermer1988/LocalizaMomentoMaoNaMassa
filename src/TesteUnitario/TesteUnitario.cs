using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Modelos.Interfaces;
using APP;
using Newtonsoft.Json;
using System.Collections.Generic;
using Modelos.Modelos;

namespace TesteUnitario
{
    public class TesteUnitario
    {
        private IDivisores divisores;

        public TesteUnitario()
        {
            var servicos = new ServiceCollection();
            ConfiguraServicos(servicos);
            var provedorServico = servicos.BuildServiceProvider();
            divisores = provedorServico.GetService<IDivisores>();
        }

        private void ConfiguraServicos(ServiceCollection services)
        {
            services.AddScoped<IDivisores, Divisores>();
        }

        [Fact]
        public void EncontrarDoisDivisoresDeSete()
        {
            var resultado = JsonConvert.DeserializeObject<List<Divisor>>(divisores.EncontrarDivisores(7));
            Assert.Equal<int>(2, resultado.Count);
        }

        [Fact]
        public void NumeroSeteEhPrimo()
        {
            Assert.True(divisores.VerificarPrimo(7));
        }

        [Fact]
        public void NumeroSeisNaoEhPrimo()
        {
            Assert.False(divisores.VerificarPrimo(6));
        }
    }
}
