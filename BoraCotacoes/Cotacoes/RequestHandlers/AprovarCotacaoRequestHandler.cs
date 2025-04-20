using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class AprovarCotacaoRequestHandler(ICotacaoRepository repository) : IRequestHandler<AprovarCotacaoRequest, Result<AprovarCotacaoResponse>>
{
    public async Task<Result<AprovarCotacaoResponse>> Handle(AprovarCotacaoRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.AprovarCotacao();
                repository.CommitScope.Update(c);
                repository.CommitScope.Commit();
            })
            .MapTry(c => new AprovarCotacaoResponse(c.Id, c.Status, c.DataCotacaoAprovada));
    }
}

public record AprovarCotacaoRequest(int Id) : IRequest<Result<AprovarCotacaoResponse>>;

public record AprovarCotacaoResponse(int Id, CotacaoStatus Status, DateTime DataCotacaoAprovada);
