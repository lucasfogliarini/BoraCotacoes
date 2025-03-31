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
}
