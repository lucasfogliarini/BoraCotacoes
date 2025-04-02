using BoraCotacoes.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BoraCotacoes.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PropostasController(IMediator mediator, ILogger<PropostasController> logger) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<BuscarPropostaResponse> Buscar(int id)
    {
        var response = await mediator.Send(new BuscarPropostaRequest(id));
        return response.Value;
    }

    [HttpPost("aprovar")]
    public async Task<AprovarPropostaResponse> Aprovar(AprovarPropostaRequest request)
    {
        var response = await mediator.Send(request);
        return response.Value;
    }
}
