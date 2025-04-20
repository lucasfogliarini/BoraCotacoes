using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using MediatR;
using BoraCotacoes.Cotacoes.DomainEvents;
using BoraCotacoes.RequestHandlers;
using BoraCotacoes.Cotacoes;

namespace BoraCotacoes.Consumers
{
    public class CotacaoStatusChangedConsumer(IMediator mediator)
    {
        [FunctionName(nameof(CotacaoStatusChangedConsumer))]
        public void Run(
            [KafkaTrigger("%BrokerList%",
                          "cotacao-status-changed",
                          ConsumerGroup = nameof(CotacaoStatusChangedConsumer))]
                          CotacaoStatusChangedDomainEvent domainEvent)
        {
            if(domainEvent.Status != CotacaoStatus.CotacaoAprovada)
                return;
            var gerarProposta = new GerarPropostaRequest(domainEvent.Id);
            mediator.Send(gerarProposta);
        }
    }
}
