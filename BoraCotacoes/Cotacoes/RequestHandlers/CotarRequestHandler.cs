using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas;
using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class CotarRequestHandler(ICotacaoRepository repository) : IRequestHandler<CotarRequest, CotarResponse>
{
    public async Task<CotarResponse> Handle(CotarRequest request, CancellationToken cancellationToken)
    {
        var cotacao = new Cotacao(request.ClienteId, request.TipoDoBem, request.Preco);
        repository.CommitScope.Add(cotacao);
        await repository.CommitScope.CommitAsync();
        return new CotarResponse(cotacao.Id, cotacao.Numero, cotacao.Status, cotacao.DataCotacaoSolicitada);
    }
}

public record CotarRequest(int ClienteId, TipoDoBem TipoDoBem, decimal Preco) : IRequest<CotarResponse>;

public record CotarResponse(int Id, string Numero, CotacaoStatus Status, DateTime DataCotacaoSolicitada);
