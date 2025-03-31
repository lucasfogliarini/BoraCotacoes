namespace BoraCotacoes
{
    public class Cotacao
    {
        public int Id { get; set; }
        public string Numero { get; private set; }
        public decimal ValorSolicitado { get; private set; }
        public int Prazo { get; private set; }
        public decimal TaxaJuros { get; private set; }
        public decimal ValorParcela { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int ClienteId { get; private set; }

        private Cotacao() { }

        public Cotacao(decimal valorSolicitado, int prazo, decimal taxaJuros)
        {
            Numero = GenerateNumero();
            ValorSolicitado = valorSolicitado;
            Prazo = prazo;
            TaxaJuros = taxaJuros;
            ValorParcela = CalcularValorParcela();
            DataCriacao = DateTime.UtcNow;
        }

        public Cotacao(int clienteId, decimal valorSolicitado)
        {
            Numero = GenerateNumero();
            ClienteId = clienteId;
            ValorSolicitado = valorSolicitado;
            DataCriacao = DateTime.UtcNow;
        }

        private string GenerateNumero()
        {
            return $"COT-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }

        private decimal CalcularValorParcela()
        {
            // Fórmula simples de amortização
            var jurosMensal = TaxaJuros / 100;
            return ValorSolicitado * jurosMensal / (1 - (decimal)Math.Pow(1 + (double)jurosMensal, -Prazo));
        }
    }

}
