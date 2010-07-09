using System;
using System.Collections.Generic;
using System.Transactions;
using Rhino.Queues;
using Rhino.Queues.Model;

namespace common
{
    public class RhinoReceiver : Receiver
    {
        List<Observer<Message>> observers = new List<Observer<Message>>();
        IQueue queue;
        CommandProcessor processor;

        public RhinoReceiver(IQueue queue, CommandProcessor processor)
        {
            this.queue = queue;
            this.processor = processor;
        }

        public void register(Observer<Message> observer)
        {
            observers.Add(observer);
        }

        public void run()
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    var message = queue.Receive();
                    observers.each(observer => observer(message));
                    transaction.Complete();
                }
            }
            catch (Exception e)
            {
                e.add_to_log();
            }
            finally
            {
                processor.add(this);
            }
        }
    }
}