using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.RequestHandlers;

public class AprovarPropostaRequestHandler(IPropostaRepository repository) : IRequestHandler<AprovarPropostaRequest, Result<AprovarPropostaResponse>>
{
    public async Task<Result<AprovarPropostaResponse>> Handle(AprovarPropostaRequest request, CancellationToken cancellationToken)
    {
        Result<Proposta> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Proposta não encontrada.")
            .Tap(p =>
            {
                p.Aprovar();
                repository.CommitScope.Commit();
            })
            .MapTry(p => new AprovarPropostaResponse(p.Id, p.Numero, p.Status, p.DataAprovacao.Value));
    }
}

public record AprovarPropostaRequest(int Id) : IRequest<Result<AprovarPropostaResponse>>;

public record AprovarPropostaResponse(int Id, string Numero, StatusProposta Status, DateTime DataAprovacao);
