using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class CalcularPrestacoesRequestHandler(ICotacaoRepository repository) : IRequestHandler<CalcularPrestacoesRequest, CalcularPrestacoesResponse>
{
    public async Task<CalcularPrestacoesResponse> Handle(CalcularPrestacoesRequest request, CancellationToken cancellationToken)
    {
        var cotacao = await repository.FindAsync(request.Id);
        cotacao.CalcularPrestacoes(request.TaxaJuros, request.PrazoMaximo);
        repository.Database.Update(cotacao);
        await repository.Database.CommitAsync();
        return new CalcularPrestacoesResponse(cotacao.Id, cotacao.Status, cotacao.DataPrestacoesCalculadas, cotacao.PrestacaoPrazoPretendido, cotacao.PrestacaoPrazoMaximo);
    }
}

public record CalcularPrestacoesRequest(int Id, decimal TaxaJuros, int PrazoMaximo) : IRequest<CalcularPrestacoesResponse>;

public record CalcularPrestacoesResponse(int Id, CotacaoStatus Status, DateTime DataPrestacoesCalculadas, decimal PrestacaoPrazoPretendido, decimal PrestacaoPrazoMaximo);
