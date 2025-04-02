using BoraCotacoes.Propostas.Repository;
using CSharpFunctionalExtensions;
using MediatR;

namespace BoraCotacoes.Handlers;

public class CotacaoAprovadaNotificationHandler(ICotacaoRepository repository) : INotificationHandler<CotacaoAprovadaNotification>
{
    public async Task Handle(CotacaoAprovadaNotification notification, CancellationToken cancellationToken)
    {
        Result<Cotacao> result = await repository.FindAsync(notification.CotacaoId);
        result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                //c.CotacaoAprovada();
                repository.Database.Update(c);
                repository.Database.Commit();
            });
    }
}

public record CotacaoAprovadaNotification(int CotacaoId) : INotification;
