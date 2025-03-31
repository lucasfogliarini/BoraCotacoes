using MediatR;

namespace BoraCotacoes.Handlers;

public class PropostaCriadaNotificationHandler : INotificationHandler<PropostaCriadaNotification>
{
    public Task Handle(PropostaCriadaNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record PropostaCriadaNotification(int propostaId) : INotification { }
