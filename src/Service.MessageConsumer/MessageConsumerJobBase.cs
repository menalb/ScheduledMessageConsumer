using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Service.MessageConsumer
{
    public abstract class MessageConsumerJobBase<TJob> : IJob
    {
        private readonly ILogger<TJob> _logger;
        private readonly MessageConsumer _processor;
        public MessageConsumerJobBase(ILogger<TJob> logger, MessageConsumer processor)
        {
            _processor = processor;
            _logger = logger;
        }

        public async virtual Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Executing...");
            await _processor.Process(
                (ex) => _logger.LogError(ex, string.Empty),
                info => _logger.LogInformation(info)
                );
        }
    }
}