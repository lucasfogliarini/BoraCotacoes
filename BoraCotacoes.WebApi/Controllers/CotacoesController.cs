using BoraCotacoes.Handlers;
using CSharpFunctionalExtensions;
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
        var result = await mediator.Send(new BuscarCotacaoRequest(id));
        return result.Value;
    }

    [HttpPost]
    public async Task<CotarResponse> Cotar(CotarRequest request)
    {
        var result = await mediator.Send(request);
        return result;
    }

    [HttpPost("{id}/SolicitarRenda")]
    public async Task<SolicitarRendaResponse> SolicitarRenda(int id, SolicitarRendaRequest request)
    {
        var result = await mediator.Send(new SolicitarRendaRequest(id, request.CorretorId));
        return result.Value;
    }

    [HttpPost("{id}/InformarCompromissoFinanceiro")]
    public async Task<InformarCompromissoFinanceiroResponse> InformarCompromissoFinanceiro(int id, InformarCompromissoFinanceiroRequest request)
    {
        var result = await mediator.Send(new InformarCompromissoFinanceiroRequest(id, request.RendaBrutaMensal, request.PrazoPretendido));
        return result.Value;
    }

    [HttpPost("{id}/CalcularPrestacoes")]
    public async Task<CalcularPrestacoesResponse> CalcularPrestacoes(int id, CalcularPrestacoesRequest request)
    {
        var result = await mediator.Send(new CalcularPrestacoesRequest(id, request.TaxaJuros, request.PrazoMaximo));
        return result.Value;
    }
}
