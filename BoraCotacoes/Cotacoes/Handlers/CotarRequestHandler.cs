using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class CotarRequestHandler(ICotacaoRepository repository) : IRequestHandler<CotarRequest, CotarResponse>
{
    public async Task<CotarResponse> Handle(CotarRequest request, CancellationToken cancellationToken)
    {
        var cotacao = new Cotacao(request.ClienteId, request.ValorSolicitado);
        repository.Database.Add(cotacao);
        await repository.Database.CommitAsync();
        return new CotarResponse(cotacao.Id, cotacao.Numero, cotacao.DataCriacao);
    }
}

public record CotarRequest(int ClienteId, decimal ValorSolicitado, int Prazo) : IRequest<CotarResponse>;

public record CotarResponse(int Id, string Numero, DateTime DataCriacao);
