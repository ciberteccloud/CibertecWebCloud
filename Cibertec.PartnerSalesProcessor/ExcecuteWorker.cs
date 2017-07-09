using Microsoft.WindowsAzure.ServiceRuntime;
using Quartz;
using Topshelf;
using Topshelf.Quartz;

namespace Cibertec.PartnerSalesProcessor
{
    public class ExcecuteWorker: RoleEntryPoint
    {
        public override bool OnStart()
        {
            HostFactory.Run(x =>
            {
                x.Service<ServiceRunner>(sc =>
                {
                    sc.WhenStarted((service, control) => service.Start());
                    sc.WhenStopped((service, control) => service.Stop());
                    sc.ConstructUsing(() => new ServiceRunner());

                    sc.ScheduleQuartzJob
                    (
                        q => q.WithJob(() => JobBuilder.Create<ExcelJobProcessor>().Build())
                        .AddTrigger(() => TriggerBuilder.Create().WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(60).RepeatForever()).Build())
                    );
                });

                x.RunAsLocalSystem()
                    .DependsOnEventLog()
                    .StartAutomatically()
                    .EnableServiceRecovery(rc => rc.RestartService(1));

                x.SetServiceName("Sale Processor");
                x.SetDisplayName("Sale Processor");
                x.SetDescription("Excel file partner sale processor.");
            });

            return base.OnStart();
        }
    }
}
