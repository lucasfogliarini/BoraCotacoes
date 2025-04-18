﻿using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BoraCotacoes.Handlers;

public class GerarPropostaRequestHandler(ICotacaoRepository repository, ILogger<GerarPropostaRequestHandler> logger) : IRequestHandler<GerarPropostaRequest, Result<GerarPropostaResponse>>
{
    public async Task<Result<GerarPropostaResponse>> Handle(GerarPropostaRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Gerando proposta para cotacao {cotacaoId}", request.CotacaoId);
        Result<Cotacao> result = await repository.FindAsync(request.CotacaoId);
        var p = new Proposta(request.CotacaoId);
        repository.Database.Add(p);
        await repository.Database.CommitAsync();
        return new GerarPropostaResponse(p.Id, p.Numero, p.Status);
    }
}

public record GerarPropostaRequest(int CotacaoId) : IRequest<Result<GerarPropostaResponse>>;

public record GerarPropostaResponse(int PropostaId, string Numero, StatusProposta Status);
