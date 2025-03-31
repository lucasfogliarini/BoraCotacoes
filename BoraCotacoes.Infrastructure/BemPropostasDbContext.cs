using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BoraCotacoes.Infrastructure;

public class BoraCotacoesDbContext(DbContextOptions options) : DbContext(options), IDatabase
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
    }

    public async Task<int> CommitAsync()
    {
        return await SaveChangesAsync();
    }

    public new void Add(object entity)
    {
        base.Add(entity);
    }

    public new void Update(object entity)
    {
        base.Update(entity);
    }

    public int Commit()
    {
        return SaveChanges();
    }
}
