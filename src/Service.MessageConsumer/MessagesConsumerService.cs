using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Service.MessageConsumer
{
     public class MessagesConsumerService<TJobService> : IHostedService, IDisposable where TJobService : IJob
    {
        private readonly IScheduler _scheduler;
        private readonly SchedulerConfiguration _configuration;
        private readonly ILogger<MessagesConsumerService<TJobService>> _logger;

        public MessagesConsumerService(IScheduler scheduler, IOptions<SchedulerConfiguration> configuration, ILogger<MessagesConsumerService<TJobService>> logger)
        {
            _scheduler = scheduler;
            _configuration = configuration.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stating service...");

            await ScheduleJob();

            await _scheduler.Start();
        }

        public async Task ScheduleJob()
        {
            IJobDetail job = JobBuilder.Create<TJobService>()
                .WithIdentity($"{typeof(TJobService).Name}Job", $"{typeof(TJobService).Name}Group")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{typeof(TJobService).Name}Trigger", $"{typeof(TJobService).Name}Group")
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x
                .OnDaysOfTheWeek(BuildDays(_configuration.RunsFromMondayToFriday))
                .StartingDailyAt(_configuration.StartingAt.AsTimeOfDay())
                .EndingDailyAt(_configuration.EndingAt.AsTimeOfDay())
                .WithIntervalInSeconds(_configuration.IntervalInSeconds))
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }

        private IReadOnlyCollection<DayOfWeek> BuildDays(bool runsFromMondayToFriday) =>
            runsFromMondayToFriday ?
            DailyTimeIntervalScheduleBuilder.MondayThroughFriday :
            DailyTimeIntervalScheduleBuilder.AllDaysOfTheWeek;

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");

            return Task.CompletedTask;
        }

        public void Dispose() { }
    }
}