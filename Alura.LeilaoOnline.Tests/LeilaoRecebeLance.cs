using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado()
        {

            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.TerminaPregao();

            leilao.RecebeLance(fulano, 1000);

            var valorEsperado = 2;
            Assert.Equal(valorEsperado, leilao.Lances.Count());
        }

        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //Arrange - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(new double[] { 800, 900, 1000, 1200 })]
        public void RetornaMaiorValorDadoleilaoComPeloMenosUmLance(double[] ofertas)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i%2==0))
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }

            leilao.TerminaPregao();

            var qtdeEsperada = 4;
            var qtdeObtida = leilao.Lances.Count();
            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
