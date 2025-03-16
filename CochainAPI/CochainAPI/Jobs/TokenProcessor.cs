using Quartz;

namespace CochainAPI.Jobs
{
    public class TokenProcessor : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
