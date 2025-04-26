using Microsoft.EntityFrameworkCore;
using BoraCotacoes.Infrastructure;
using BoraCotacoes.Propostas.Repository;
using BoraCotacoes.Infrastructure.Repositories;
using BoraCotacoes;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();
        services.AddProducer();
    }
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPropostaRepository, PropostaRepository>();
        services.AddScoped<ICotacaoRepository, CotacaoRepository>();
    }
    private static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<BoraCotacoesDbContext>(options => options.UseInMemoryDatabase(nameof(BoraCotacoesDbContext)));
    }
    private static void AddOpenTelemetry(this IServiceCollection services)
    {
    }

    private static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IProducer>(provider =>
            new KafkaProducer("kafka:9092"));
    }
}
