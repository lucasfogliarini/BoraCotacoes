using MediatR;

namespace BoraCotacoes.Cotacoes.DomainEvents;

public class CotacaoStatusChangedDomainEventHandler(IProducer producer) : INotificationHandler<CotacaoStatusChangedDomainEvent>
{
    public async Task Handle(CotacaoStatusChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await producer.ProduceAsync("cotacao-status-changed", domainEvent);
    }
}

public record CotacaoStatusChangedDomainEvent(int id, CotacaoStatus Status, DateTime ChangedAt) : IDomainEvent { }
