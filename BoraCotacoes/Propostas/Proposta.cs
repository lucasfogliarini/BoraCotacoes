namespace BoraCotacoes
{
    public class Proposta : AggregateRoot
    {
        public int CotacaoId { get; set; }
        public string Numero { get; private set; }
        public decimal ValorSolicitado { get; private set; }
        public int Prazo { get; private set; }
        public decimal TaxaJuros { get; private set; }
        public decimal ValorParcela { get; private set; }
        public StatusProposta Status { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAprovacao { get; private set; }
        public DateTime? DataAssinatura { get; private set; }

        private Proposta() { }

        public Proposta(int cotacaoId)
        {
            Numero = GenerateNumeroProposta();
            CotacaoId = cotacaoId;
            DataCriacao = DateTime.UtcNow;
        }
        public void AprovarFinanceiramente(decimal taxaJuros, decimal valorParcela)
        {
            if (Status != StatusProposta.Solicitada)
                throw new InvalidOperationException("A proposta não pode ser aprovada neste estado.");

            TaxaJuros = taxaJuros;
            ValorParcela = valorParcela;
            Status = StatusProposta.AprovadaFinanceiramente;
        }
        public void Aprovar()
        {
            if (Status != StatusProposta.AprovadaFinanceiramente)
                throw new InvalidOperationException("A proposta não pode ser aprovada neste estado.");

            Status = StatusProposta.AprovadaPeloCliente;
            DataAprovacao = DateTime.UtcNow;
        }
        public void ContratoAssinado()
        {
            if (Status != StatusProposta.AprovadaPeloCliente)
                throw new InvalidOperationException("A proposta não pode ser aprovada neste estado.");

            Status = StatusProposta.ContratoAssinado;
            DataAssinatura = DateTime.UtcNow;
        }

        public void Rejeitar()
        {
            if (Status != StatusProposta.Solicitada)
                throw new InvalidOperationException("A proposta não pode ser rejeitada neste estado.");

            Status = StatusProposta.Rejeitada;
        }

        private string GenerateNumeroProposta()
        {
            return $"PROP-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }
    }

    public enum StatusProposta
    {
        Solicitada,
        AprovadaFinanceiramente,        
        AprovadaPeloCliente,
        ContratoAssinado,
        Rejeitada,
        Cancelada
    }
}
