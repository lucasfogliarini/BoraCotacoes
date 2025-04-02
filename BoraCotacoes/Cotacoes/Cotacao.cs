using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas;
using CSharpFunctionalExtensions;
using System.Threading.Tasks.Sources;

namespace BoraCotacoes
{
    public class Cotacao
    {
        public int Id { get; set; }
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
            Status = CotacaoStatus.CotacaoSolicitada;
            Numero = GenerateNumero();
            ClienteId = clienteId;
            TipoDoBem = tipoDoBem;
            Preco = preco;
        }

        public void SolicitarRenda(int corretorId)
        {
            DataRendaSolicitada = DateTime.UtcNow;
            Status = CotacaoStatus.RendaSolicitada;
            CorretorId = corretorId;
        }

        public void InformarCompromissoFinanceiro(decimal rendaBrutaMensal, int prazoPretendido)
        {
            DataCompromissoFinanceiroInformado = DateTime.UtcNow;
            Status = CotacaoStatus.CompromissoFinanceiroInformado;
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
                        Status = CotacaoStatus.PrestacoesCalculadas;
                        TaxaJuros = taxaJuros;
                        PrazoMaximo = prazoMaximo;
                        PrestacaoPrazoPretendido = CalcularPrice(taxaJuros, PrazoPretendido);
                        PrestacaoPrazoMaximo = CalcularPrice(taxaJuros, PrazoMaximo);
                    });
        }

        public void AprovarCotacao()
        {
            DataCotacaoAprovada = DateTime.UtcNow;
            Status = CotacaoStatus.CotacaoAprovada;
        }

        /// <summary>
        /// //Fórmula de amortização price https://chatgpt.com/?q=TabelaPrice
        /// </summary>
        private decimal CalcularPrice(decimal taxaJuros, int prazo) => Preco * taxaJuros / (1 - (decimal)Math.Pow(1 + (double)taxaJuros, -prazo));

        private string GenerateNumero() => $"COT-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";       
    }

}
