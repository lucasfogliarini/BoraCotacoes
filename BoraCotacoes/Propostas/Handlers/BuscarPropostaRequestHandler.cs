using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class BuscarPropostaRequestHandler(IPropostaRepository repository) : IRequestHandler<BuscarPropostaRequest, Result<BuscarPropostaResponse>>
{
    public async Task<Result<BuscarPropostaResponse>> Handle(BuscarPropostaRequest request, CancellationToken cancellationToken)
    {
        Result<Proposta> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Proposta não encontrada.")
            .MapTry(p=> new BuscarPropostaResponse(p.Id, p.Numero, p.Status, p.DataCriacao));
    }
}

public record BuscarPropostaRequest(int Id) : IRequest<Result<BuscarPropostaResponse>>;

public record BuscarPropostaResponse(int Id, string Numero, StatusProposta Status, DateTime DataCriacao);
