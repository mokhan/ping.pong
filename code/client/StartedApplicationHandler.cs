using common;
using common.messages;

namespace client
{
    public class StartedApplicationHandler : AbstractHandler<StartedApplication>
    {
        public override void handle(StartedApplication item)
        {
            "received {0}".log(item.message);
        }
    }
}