using CSharpFunctionalExtensions;

namespace BoraCotacoes
{
    public abstract class AggregateRoot : Entity<int>
    {
        private List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= [];
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
