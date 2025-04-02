using BoraCotacoes.Cotacoes;
using BoraCotacoes.Cotacoes.DomainEvents;
using BoraCotacoes.Propostas;
using CSharpFunctionalExtensions;

namespace BoraCotacoes
{
    public class Cotacao : AggregateRoot
    {
        public CotacaoStatus Status { get; private set; }
        public string Numero { get; private set; }

        public DateTime DataCotacaoSolicitada { get; private set; }
        public TipoDoBem TipoDoBem { get; private set; }
        public decimal Preco { get; private set; }
        public int ClienteId { get; private set; }

        public DateTime DataRendaSolicitada { get; private set; }
        public int CorretorId { get; private set; }


        public DateTime DataCompromissoFinanceiroInformado { get; private set; }
        public decimal RendaBrutaMensal { get; private set; }
        public int PrazoPretendido { get; private set; }

        public DateTime DataPrestacoesCalculadas { get; private set; }
        public decimal TaxaJuros { get; private set; }
        public int PrazoMaximo { get; private set; }
        public decimal PrestacaoPrazoPretendido { get; private set; }
        public decimal PrestacaoPrazoMaximo { get; private set; }

        public DateTime DataCotacaoAprovada { get; private set; }

        private Cotacao() { }

        public Cotacao(int clienteId, TipoDoBem tipoDoBem, decimal preco)
        {
            DataCotacaoSolicitada = DateTime.UtcNow;
            ChangeStatus(CotacaoStatus.CotacaoSolicitada, DataCotacaoSolicitada);
            Numero = GenerateNumero();
            ClienteId = clienteId;
            TipoDoBem = tipoDoBem;
            Preco = preco;
        }

        public void SolicitarRenda(int corretorId)
        {
            DataRendaSolicitada = DateTime.UtcNow;
            ChangeStatus(CotacaoStatus.RendaSolicitada, DataRendaSolicitada);
            Status = CotacaoStatus.RendaSolicitada;
            CorretorId = corretorId;
        }

        public void InformarCompromissoFinanceiro(decimal rendaBrutaMensal, int prazoPretendido)
        {
            DataCompromissoFinanceiroInformado = DateTime.UtcNow;
            ChangeStatus(CotacaoStatus.CompromissoFinanceiroInformado, DataCompromissoFinanceiroInformado);
            RendaBrutaMensal = rendaBrutaMensal;
            PrazoPretendido = prazoPretendido;
        }

        public Result CalcularPrestacoes(decimal taxaJuros, int prazoMaximo)
        {
            return Result.Success()
                    .Ensure(() => taxaJuros > 0, "A taxa de juros deve ser maior que zero.")
                    .Ensure(()=> prazoMaximo > 0, "O prazo máximo deve ser maior que zero.")
                    .Tap(() =>
                    {
                        DataPrestacoesCalculadas = DateTime.UtcNow;
                        ChangeStatus(CotacaoStatus.PrestacoesCalculadas, DataCotacaoAprovada);
                        TaxaJuros = taxaJuros;
                        PrazoMaximo = prazoMaximo;
                        PrestacaoPrazoPretendido = CalcularPrice(taxaJuros, PrazoPretendido);
                        PrestacaoPrazoMaximo = CalcularPrice(taxaJuros, PrazoMaximo);
                    });
        }

        public void AprovarCotacao()
        {
            DataCotacaoAprovada = DateTime.UtcNow;
            ChangeStatus(CotacaoStatus.CotacaoAprovada, DataCotacaoAprovada);
        }

        private void ChangeStatus(CotacaoStatus newStatus, DateTime changedAt)
        {
            Status = newStatus;
            AddDomainEvent(new CotacaoStatusChangedDomainEvent(Id, Status, changedAt));
        }

        /// <summary>
        /// //Fórmula de amortização price https://chatgpt.com/?q=TabelaPrice
        /// </summary>
        private decimal CalcularPrice(decimal taxaJuros, int prazo) => Preco * taxaJuros / (1 - (decimal)Math.Pow(1 + (double)taxaJuros, -prazo));

        private string GenerateNumero() => $"COT-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";       
    }

}
