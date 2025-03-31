using BoraCotacoes.Propostas.Repository;

namespace BoraCotacoes.Infrastructure.Repositories;

public class CotacaoRepository(BoraCotacoesDbContext boraCotacoesDbContext) : ICotacaoRepository
{
    public IDatabase Database => boraCotacoesDbContext;

    public void Add(Cotacao cotacao)
    {
        boraCotacoesDbContext.Add(cotacao);
    }

    public async Task<Cotacao?> FindAsync(int id)
    {
        return await boraCotacoesDbContext.FindAsync<Cotacao>(id);
    }
}
