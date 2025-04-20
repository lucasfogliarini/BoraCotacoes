using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.RequestHandlers;

public class CalcularPrestacoesRequestHandler(ICotacaoRepository repository) : IRequestHandler<CalcularPrestacoesRequest, Result<CalcularPrestacoesResponse>>
{
    public async Task<Result<CalcularPrestacoesResponse>> Handle(CalcularPrestacoesRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.CalcularPrestacoes(request.TaxaJuros, request.PrazoMaximo);
                repository.CommitScope.Update(c);
                repository.CommitScope.Commit();
            })
            .MapTry(c => new CalcularPrestacoesResponse(c.Id, c.Status, c.DataPrestacoesCalculadas, c.PrestacaoPrazoPretendido, c.PrestacaoPrazoMaximo));
    }
}

public record CalcularPrestacoesRequest(int Id, decimal TaxaJuros, int PrazoMaximo) : IRequest<Result<CalcularPrestacoesResponse>>;

public record CalcularPrestacoesResponse(int Id, CotacaoStatus Status, DateTime DataPrestacoesCalculadas, decimal PrestacaoPrazoPretendido, decimal PrestacaoPrazoMaximo);
