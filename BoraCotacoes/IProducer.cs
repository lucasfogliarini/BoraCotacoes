namespace BoraCotacoes;

public interface IProducer 
{
    Task ProduceAsync<T>(string topic, T message);
}

