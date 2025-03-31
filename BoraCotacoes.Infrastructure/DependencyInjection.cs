using Microsoft.EntityFrameworkCore;
using BoraCotacoes.Infrastructure;
using BoraCotacoes.Propostas.Repository;
using BoraCotacoes.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPropostaRepository, PropostasRepository>();
        services.AddScoped<ICotacaoRepository, CotacaoRepository>();
    }
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<BoraCotacoesDbContext>(options => options.UseInMemoryDatabase(nameof(BoraCotacoesDbContext)));
    }
}
