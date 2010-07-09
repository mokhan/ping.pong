using System;
using System.IO;
using ProtoBuf;
using Rhino.Queues.Model;

namespace common
{
    public class MessageHandler
    {
        DependencyRegistry registry;

        public MessageHandler(DependencyRegistry registry)
        {
            this.registry = registry;
        }

        public void handler(Message item)
        {
            var payload = parse_payload_from(item);
            registry
                .get_all<Handler>()
                .each(x => x.handle(payload));
        }

        object parse_payload_from(Message item)
        {
            using (var stream = new MemoryStream(item.Data))
            {
                return Serializer.NonGeneric.Deserialize(Type.GetType(item.Headers["type"]), stream);
            }
        }
    }
}