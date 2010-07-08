using common;
using common.messages;

namespace server
{
    public class StartedApplicationHandler : AbstractHandler<StartedApplication>
    {
        public override void handle(StartedApplication item)
        {
            "received {0}".log(item.message);
        }
    }
}