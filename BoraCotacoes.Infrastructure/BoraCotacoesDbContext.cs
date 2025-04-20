using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BoraCotacoes.Infrastructure;

public class BoraCotacoesDbContext(DbContextOptions options, IMediator mediator) : DbContext(options), IDatabase
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        return result;
    }

    public new void Add(object entity)
    {
        base.Add(entity);
    }

    public new void Update(object entity)
    {
        base.Update(entity);
    }

    public int Commit(CancellationToken cancellationToken = default)
    {
        return CommitAsync().Result;
    }
}
