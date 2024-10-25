using Entity;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

public class PublisherPrincipal
{
    public void EnviarMensagens()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "fila_protocolos", durable: false, exclusive: false, autoDelete: false, arguments: null);

            for (int i = 1; i <= 10; i++)
            {
                var protocolo = new Protocolo
                {
                    NumeroProtocolo = $"PROTOCOLO-{i}",
                    NumeroVia = 1,
                    Cpf = "12345678900",
                    Rg = "MG1234567",
                    Nome = "Nome Exemplo",
                    NomeMae = "Mãe Exemplo",
                    NomePai = "Pai Exemplo",
                    FotoBase64 = "stringBase64DeExemplo"
                };

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(protocolo));

                channel.BasicPublish(exchange: "", routingKey: "fila_protocolos", basicProperties: null, body: body);
                Console.WriteLine($"[x] Protocolo enviado: {protocolo.NumeroProtocolo}");
            }
        }
    }
}
