using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using MediatR;
using BoraCotacoes.Handlers;

namespace BoraCotacoes.Consumers
{
    public class CotacaoAprovadaConsumer(IMediator mediator)
    {
        [FunctionName(nameof(CotacaoAprovadaConsumer))]
        public void Run(
            [KafkaTrigger("%BrokerList%",
                          "cotacao-aprovada",
                          ConsumerGroup = nameof(CotacaoAprovadaConsumer))]
                          CotacaoAprovadaNotification notification)
        {
            mediator.Send(notification);
        }
    }
}
