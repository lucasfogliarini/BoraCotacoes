using BoraCotacoes.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoraCotacoes.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CotacoesController(IMediator mediator, ILogger<CotacoesController> logger) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<BuscarCotacaoResponse> Buscar(int id)
    {
        var response = await mediator.Send(new BuscarCotacaoRequest(id));
        return response.Value;
    }

    [HttpPost]
    public async Task<CotarResponse> Cotar(CotarRequest request)
    {
        var response = await mediator.Send(request);
        return response;
    }

    [HttpPost("{id}/SolicitarRenda")]
    public async Task<SolicitarRendaResponse> SolicitarRenda(int id, SolicitarRendaRequest request)
    {
        var response = await mediator.Send(new SolicitarRendaRequest(id, request.CorretorId));
        return response;
    }

    [HttpPost("{id}/InformarCompromissoFinanceiro")]
    public async Task<InformarCompromissoFinanceiroResponse> InformarCompromissoFinanceiro(int id, InformarCompromissoFinanceiroRequest request)
    {
        var response = await mediator.Send(new InformarCompromissoFinanceiroRequest(id, request.RendaBrutaMensal, request.PrestacaoEstimada));
        return response;
    }
}
