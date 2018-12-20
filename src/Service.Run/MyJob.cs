using Microsoft.Extensions.Logging;
using Service.MessageConsumer;

namespace Service.Run.Jobs
{
    public class ProcessMyJob : MessageConsumerJobBase<ProcessMyJob>
    {
        public ProcessMyJob(ILogger<ProcessMyJob> logger, MessageConsumer.MessageConsumer processor) : base(logger, processor)
        {
        }
    }
}