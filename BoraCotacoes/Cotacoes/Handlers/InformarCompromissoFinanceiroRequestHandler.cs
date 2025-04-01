using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using MediatR;

namespace BoraCotacoes.Handlers;

public class InformarCompromissoFinanceiroRequestHandler(ICotacaoRepository repository) : IRequestHandler<InformarCompromissoFinanceiroRequest, InformarCompromissoFinanceiroResponse>
{
    public async Task<InformarCompromissoFinanceiroResponse> Handle(InformarCompromissoFinanceiroRequest request, CancellationToken cancellationToken)
    {
        var cotacao = await repository.FindAsync(request.Id);
        cotacao.InformarCompromissoFinanceiro(request.RendaBrutaMensal, request.PrazoPretendido);
        repository.Database.Update(cotacao);
        await repository.Database.CommitAsync();
        return new InformarCompromissoFinanceiroResponse(cotacao.Id, cotacao.Status, cotacao.DataCompromissoFinanceiroInformado);
    }
}

public record InformarCompromissoFinanceiroRequest(int Id, decimal RendaBrutaMensal, int PrazoPretendido) : IRequest<InformarCompromissoFinanceiroResponse>;

public record InformarCompromissoFinanceiroResponse(int Id, CotacaoStatus Status, DateTime DataCompromissoFinanceiroInformado);
