using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas;
using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class CotarRequestHandler(ICotacaoRepository repository) : IRequestHandler<CotarRequest, CotarResponse>
{
    public async Task<CotarResponse> Handle(CotarRequest request, CancellationToken cancellationToken)
    {
        var cotacao = new Cotacao(request.ClienteId, request.TipoDoBem, request.PrecoAtualDoBem);
        repository.Database.Add(cotacao);
        await repository.Database.CommitAsync();
        return new CotarResponse(cotacao.Id, cotacao.Numero, cotacao.Status, cotacao.DataCotacaoSolicitada);
    }
}

public record CotarRequest(int ClienteId, TipoDoBem TipoDoBem, decimal PrecoAtualDoBem) : IRequest<CotarResponse>;

public record CotarResponse(int Id, string Numero, CotacaoStatus Status, DateTime DataCotacaoSolicitada);
