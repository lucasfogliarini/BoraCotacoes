using MediatR;
using Microsoft.Extensions.Logging;

namespace BoraCotacoes.Cotacoes.DomainEvents;

public class CotacaoStatusChangedDomainEventHandler(IProducer producer, ILogger<CotacaoStatusChangedDomainEventHandler> logger) : INotificationHandler<CotacaoStatusChangedDomainEvent>
{
    public async Task Handle(CotacaoStatusChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("{id}, {status}, {changedAt}", domainEvent.Id, domainEvent.Status, domainEvent.ChangedAt);
        await producer.ProduceAsync("cotacao-status-changed", domainEvent);
    }
}


