using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("fulano", leilao);
            var maria = new Interessada("maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            Console.WriteLine(leilao.Ganhador.Valor);
            Console.ReadKey();
        }
    }
}
