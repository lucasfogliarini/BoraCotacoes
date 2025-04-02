using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using MediatR;
using BoraCotacoes.Cotacoes.DomainEvents;

namespace BoraCotacoes.Consumers
{
    public class CotacaoAprovadaConsumer(IMediator mediator)
    {
        [FunctionName(nameof(CotacaoAprovadaConsumer))]
        public void Run(
            [KafkaTrigger("%BrokerList%",
                          "cotacao-status-changed",
                          ConsumerGroup = nameof(CotacaoAprovadaConsumer))]
                          CotacaoStatusChangedDomainEvent domainEvent)
        {
            //mediator.Send(notification);
        }
    }
}
