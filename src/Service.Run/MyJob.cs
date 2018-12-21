using Microsoft.Extensions.Logging;
using Service.Consumer;

namespace Service.Run.Jobs
{
    public class ProcessMyJob : MessageConsumerJobBase<ProcessMyJob>
    {
        public ProcessMyJob(ILogger<ProcessMyJob> logger, MessageConsumer processor) : base(logger, processor)
        {
        }
    }
}