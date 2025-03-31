namespace BoraCotacoes.Contratos
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Numero { get; private set; }
        public int PropostaId { get; private set; }
        public Proposta Proposta { get; private set; }
        public DateTime DataAssinatura { get; private set; }

        private Contrato() { }

        public Contrato(Proposta proposta)
        {
            //if (proposta.Status != StatusProposta.Aprovada)
            //    throw new InvalidOperationException("A proposta precisa estar aprovada para gerar um contrato.");

            Proposta = proposta;
            PropostaId = proposta.Id;
            Numero = GenerateNumeroContrato();
            DataAssinatura = DateTime.UtcNow;
        }

        private string GenerateNumeroContrato()
        {
            return $"CONT-{DateTime.UtcNow:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }
    }

}
