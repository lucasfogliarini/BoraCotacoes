namespace BoraCotacoes.Propostas.Repository;

public interface ICotacaoRepository : IAddRepository<Cotacao>
{
    Task<Cotacao?> FindAsync(int id);
}
