using BoraCotacoes.Propostas.Repository;

namespace BoraCotacoes.Infrastructure.Repositories;

public class PropostasRepository(BoraCotacoesDbContext BoraCotacoesDbContext) : IPropostaRepository
{
    public ICommitScope CommitScope => BoraCotacoesDbContext;

    public void Add(Proposta proposta)
    {
        BoraCotacoesDbContext.Add(proposta);
    }

    public async Task<Proposta?> FindAsync(int id)
    {
        return await BoraCotacoesDbContext.FindAsync<Proposta>(id);
    }
}
