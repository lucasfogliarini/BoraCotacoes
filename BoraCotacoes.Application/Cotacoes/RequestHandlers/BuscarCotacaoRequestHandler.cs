using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.RequestHandlers;

public class BuscarCotacaoRequestHandler(ICotacaoRepository repository) : IRequestHandler<BuscarCotacaoRequest, Result<BuscarCotacaoResponse>>
{
    public async Task<Result<BuscarCotacaoResponse>> Handle(BuscarCotacaoRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .MapTry(c => new BuscarCotacaoResponse(c.Id, c.Numero, c.DataCotacaoSolicitada));
    }
}

public record BuscarCotacaoRequest(int Id) : IRequest<Result<BuscarCotacaoResponse>>;

public record BuscarCotacaoResponse(int Id, string Numero, DateTime DataCotacaoSolicitada);
