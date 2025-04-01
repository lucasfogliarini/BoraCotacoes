using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas;
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

        private string GenerateNumero() => $"COT-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";

        //private decimal CalcularValorParcela()
        //{
        //    Fórmula simples de amortização
        //    var jurosMensal = TaxaJuros / 100;
        //    return PrecoDoBem * jurosMensal / (1 - (decimal)Math.Pow(1 + (double)jurosMensal, -Prazo));
        //}
    }

}
