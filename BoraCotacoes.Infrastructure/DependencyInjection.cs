using Microsoft.EntityFrameworkCore;
using BoraCotacoes.Infrastructure;
using BoraCotacoes.Propostas.Repository;
using BoraCotacoes.Infrastructure.Repositories;
using BoraCotacoes;
using System.Reflection;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();
        services.AddOpenTelemetryExporter();
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

    private static void AddOpenTelemetryExporter(this IServiceCollection services)
    {
        var assemblyName = Assembly.GetEntryAssembly()?.GetName();
        var serviceName = assemblyName?.Name ?? "Unknown Service Name";
        var serviceVersion = assemblyName?.Version?.ToString() ?? "Unknown Version";

        services.AddOpenTelemetry()
            .WithTracing(tracerBuilder =>
            {
                tracerBuilder
                    //.AddSource(serviceName)
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter();
            })
            .WithMetrics(meterBuilder =>
            {
                meterBuilder
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddRuntimeInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddOtlpExporter();
            })
            .WithLogging(loggingBuilder =>
            {
                loggingBuilder
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddOtlpExporter();
            });
    }

    private static void AddProducer(this IServiceCollection services)
    {
        services.AddScoped<IProducer>(provider =>
            new KafkaProducer("kafka:9092"));
    }
}
