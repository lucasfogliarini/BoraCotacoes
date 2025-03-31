using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

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
                repository.Database.Update(p);
                repository.Database.Commit();
            })
            .MapTry(p => new AprovarPropostaResponse(p.Id, p.Numero, p.Status, p.DataAprovacao.Value));
    }
}

public record AprovarPropostaRequest(int Id) : IRequest<Result<AprovarPropostaResponse>>;

public record AprovarPropostaResponse(int Id, string Numero, StatusProposta Status, DateTime DataAprovacao);
