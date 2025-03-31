using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class ContratoAssinadoNotificationHandler(IPropostaRepository repository) : INotificationHandler<ContratoAssinadoNotification>
{
    public async Task Handle(ContratoAssinadoNotification notification, CancellationToken cancellationToken)
    {
        Result<Proposta> result = await repository.FindAsync(notification.PropostaId);
        result
            .EnsureNotNull("Proposta não encontrada.")
            .Tap(p =>
            {
                p.ContratoAssinado();
                repository.Database.Update(p);
                repository.Database.Commit();
            });
    }
}

public record ContratoAssinadoNotification(int PropostaId) : INotification;
