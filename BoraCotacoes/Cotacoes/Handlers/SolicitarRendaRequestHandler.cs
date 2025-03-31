using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class SolicitarRendaRequestHandler(ICotacaoRepository repository) : IRequestHandler<SolicitarRendaRequest, SolicitarRendaResponse>
{
    public async Task<SolicitarRendaResponse> Handle(SolicitarRendaRequest request, CancellationToken cancellationToken)
    {
        var cotacao = await repository.FindAsync(request.Id);
        cotacao.SolicitarRenda(request.CorretorId);
        repository.Database.Update(cotacao);
        await repository.Database.CommitAsync();
        return new SolicitarRendaResponse(cotacao.Id, cotacao.Status, cotacao.DataRendaSolicitada);
    }
}

public record SolicitarRendaRequest(int Id, int CorretorId) : IRequest<SolicitarRendaResponse>;

public record SolicitarRendaResponse(int Id, CotacaoStatus Status, DateTime DataRendaSolicitada);
