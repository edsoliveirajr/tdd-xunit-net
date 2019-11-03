using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            var valorNegativo = -100;

            Assert.Throws<ArgumentException>(
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
