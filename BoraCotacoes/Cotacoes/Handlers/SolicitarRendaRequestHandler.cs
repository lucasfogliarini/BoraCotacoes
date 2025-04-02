using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class SolicitarRendaRequestHandler(ICotacaoRepository repository) : IRequestHandler<SolicitarRendaRequest, Result<SolicitarRendaResponse>>
{
    public async Task<Result<SolicitarRendaResponse>> Handle(SolicitarRendaRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.SolicitarRenda(request.CorretorId);
                repository.Database.Update(c);
                repository.Database.Commit();
            })
            .MapTry(c => new SolicitarRendaResponse(c.Id, c.Status, c.DataRendaSolicitada));
    }
}

public record SolicitarRendaRequest(int Id, int CorretorId) : IRequest<Result<SolicitarRendaResponse>>;

public record SolicitarRendaResponse(int Id, CotacaoStatus Status, DateTime DataRendaSolicitada);
