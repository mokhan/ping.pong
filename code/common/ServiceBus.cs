using System;

namespace common
{
    public interface ServiceBus
    {
        void publish<Message>() where Message : new();
        void publish<Message>(Message item) where Message : new();
        void publish<Message>(Action<Message> configure) where Message : new();
    }
}