using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ProtoBuf;
using Rhino.Queues.Model;

namespace common
{
    public class MessageHandler
    {
        BinaryFormatter formatter = new BinaryFormatter();
        DependencyRegistry registry;

        public MessageHandler(DependencyRegistry registry)
        {
            this.registry = registry;
        }

        public void handler(Message item)
        {
            var payload = parse_payload_from(item);
            this.log().debug("received: {0}", payload);
            registry
                .get_all<Handler>()
                .each(x => x.handle(payload));
        }

        object parse_payload_from(Message item)
        {
            using (var stream = new MemoryStream(item.Data))
            {
                //return formatter.Deserialize(stream);
                return Serializer.NonGeneric.Deserialize(Type.GetType(item.Headers["type"]), stream);
            }
        }
    }
}