using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Fact]
        public void LeilaoSemLances()
        {
            var leilao = new Leilao("Van Gogh");

            leilao.IniciaPregao();
            leilao.TerminaPregao();

            double valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadopregaoNaoIniciado()
        {
            var leilao = new Leilao("Van Gogh");

            Assert.Throws<InvalidOperationException>(
                () => leilao.TerminaPregao()
            );
        }

        [Theory]
        [InlineData(1200, 1250, new double[]{800, 1150, 1400, 1250})]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino, double valorEsperado, double[] ofertas)
        {
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2 == 0))
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);
        }
    }
}
