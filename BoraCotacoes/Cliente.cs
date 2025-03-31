namespace BoraCotacoes
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public List<Proposta> Propostas { get; private set; } = new();

        private Cliente() { }

        public Cliente(string nome, string cpf, DateTime dataNascimento, string email, string telefone)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            Telefone = telefone;
            DataCadastro = DateTime.UtcNow;
        }

        public bool EhMaiorDeIdade()
        {
            var idade = DateTime.UtcNow.Year - DataNascimento.Year;
            if (DataNascimento.Date > DateTime.UtcNow.AddYears(-idade)) idade--;
            return idade >= 18;
        }
    }

}
