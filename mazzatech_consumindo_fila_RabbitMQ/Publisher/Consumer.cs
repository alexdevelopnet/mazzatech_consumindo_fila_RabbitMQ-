using Entity;
using Newtonsoft.Json;
using Publisher.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace Publisher
{

    public class Consumer
    {
        public void ReceberMensagens()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "fila_protocolos", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var protocolo = JsonConvert.DeserializeObject<Protocolo>(message);

                    using (var context = new ProtocoloContext())
                    {
                        // Validações
                        bool existeProtocolo = context.Protocolos.Any(p => p.NumeroProtocolo == protocolo.NumeroProtocolo);
                        if (!existeProtocolo)
                        {
                            context.Protocolos.Add(protocolo);
                            context.SaveChanges();
                            Console.WriteLine($"Protocolo {protocolo.NumeroProtocolo} salvo no banco de dados.");
                        }
                        else
                        {
                            Console.WriteLine($"Protocolo duplicado: {protocolo.NumeroProtocolo}");
                        }
                    }
                };
                channel.BasicConsume(queue: "fila_protocolos", autoAck: true, consumer: consumer);
                Console.WriteLine("Aguardando mensagens...");
                Console.ReadLine();
            }
        }
    }

}
