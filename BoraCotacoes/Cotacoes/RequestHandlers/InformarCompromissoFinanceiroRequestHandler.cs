using BoraCotacoes.Cotacoes;
using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class InformarCompromissoFinanceiroRequestHandler(ICotacaoRepository repository) : IRequestHandler<InformarCompromissoFinanceiroRequest, Result<InformarCompromissoFinanceiroResponse>>
{
    public async Task<Result<InformarCompromissoFinanceiroResponse>> Handle(InformarCompromissoFinanceiroRequest request, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(request.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.InformarCompromissoFinanceiro(request.RendaBrutaMensal, request.PrazoPretendido);
                repository.CommitScope.Update(c);
                repository.CommitScope.Commit();
            })
            .MapTry(c => new InformarCompromissoFinanceiroResponse(c.Id, c.Status, c.DataCompromissoFinanceiroInformado));
    }
}

public record InformarCompromissoFinanceiroRequest(int Id, decimal RendaBrutaMensal, int PrazoPretendido) : IRequest<Result<InformarCompromissoFinanceiroResponse>>;

public record InformarCompromissoFinanceiroResponse(int Id, CotacaoStatus Status, DateTime DataCompromissoFinanceiroInformado);
