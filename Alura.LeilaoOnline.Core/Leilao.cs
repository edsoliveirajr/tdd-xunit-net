using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }

        public Lance Ganhador { get; private set; }

        public EstadoLeilao Estado { get; private set; }
        public IModalidadeAvaliacao Modalidade { get; }

        public Leilao(string peca, IModalidadeAvaliacao modalidade = null)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            //Modalidade =(modalidade == null ? new MaiorValor(): modalidade);
            Modalidade = modalidade == null ? new MaiorValor() : modalidade;
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                if (Estado == EstadoLeilao.LeilaoAntesDoPregao)
                    throw new InvalidOperationException("Pregão não foi iniciado");

                if (Estado == EstadoLeilao.LeilaoFinalizado)
                    throw new InvalidOperationException("Pregão já foi finalizado");
            }

            Ganhador = Modalidade.Avalia(this);

            Estado = EstadoLeilao.LeilaoFinalizado;
        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) &&
                   (cliente != _ultimoCliente);
        }
    }
}
