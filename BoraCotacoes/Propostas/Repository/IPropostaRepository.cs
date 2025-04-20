namespace BoraCotacoes.Propostas.Repository;

public interface IPropostaRepository : IAddRepository<Proposta>
{
    Task<Proposta?> FindAsync(int id);
}
