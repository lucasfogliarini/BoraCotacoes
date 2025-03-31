using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class BuscarCotacaoRequestHandler(ICotacaoRepository repository) : IRequestHandler<BuscarCotacaoRequest, Result<BuscarCotacaoResponse>>
{
    public async Task<Result<BuscarCotacaoResponse>> Handle(BuscarCotacaoRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .MapTry(p=> new BuscarCotacaoResponse(p.Id, p.Numero, p.DataCriacao));
    }
}

public record BuscarCotacaoRequest(int Id) : IRequest<Result<BuscarCotacaoResponse>>;

public record BuscarCotacaoResponse(int Id, string Numero, DateTime DataCriacao);
