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
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPropostaRepository, PropostaRepository>();
        services.AddScoped<ICotacaoRepository, CotacaoRepository>();
    }
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<BoraCotacoesDbContext>(options => options.UseInMemoryDatabase(nameof(BoraCotacoesDbContext)));
    }

    public static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IProducer>(provider =>
            new KafkaProducer("kafka:9092"));
    }
}
