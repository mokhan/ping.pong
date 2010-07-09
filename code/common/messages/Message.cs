using System;
using ProtoBuf;

namespace common.messages
{
    [Serializable]
    [ProtoContract]
    public class Message
    {
        [ProtoMember(1)]
        public string source { get; set; }

        [ProtoMember(2)]
        public string message { get; set; }

        public override string ToString()
        {
            return base.ToString() + source;
        }
    }
}