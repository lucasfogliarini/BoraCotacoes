namespace BoraCotacoes.Propostas.Repository;

public interface ICotacaoRepository : IRepository
{
    Task<Cotacao?> FindAsync(int id);
}
