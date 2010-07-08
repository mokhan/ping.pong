using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Transactions;
using ProtoBuf;
using Rhino.Queues;

namespace common
{
    public class RhinoPublisher : ServiceBus
    {
        BinaryFormatter formatter = new BinaryFormatter();
        readonly int port;
        string destination_queue;
        IQueueManager sender;

        public RhinoPublisher(string destination_queue, int port, IQueueManager manager)
        {
            this.port = port;
            this.destination_queue = destination_queue;
            sender = manager;
        }

        public void publish<T>() where T : new()
        {
            publish(new T());
        }

        public void publish<T>(T item) where T : new()
        {
            using (var transaction = new TransactionScope())
            {
                var destination = "rhino.queues://localhost:{0}/{1}".format(port, destination_queue);
                this.log().debug("sending {0} to {1}", item, destination);
                sender.Send(new Uri(destination), create_payload_from(item));
                transaction.Complete();
            }
        }

        MessagePayload create_payload_from<T>(T item)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, item);
                //formatter.Serialize(stream, item);

                var payload = new MessagePayload {Data = stream.ToArray()};
                payload.Headers["type"] = typeof (T).FullName;
                return payload;
            }
        }

        public void publish<T>(Action<T> configure) where T : new()
        {
            var item = new T();
            configure(item);
            publish(item);
        }
    }
}