using CochainAPI.Data.Services.Interfaces;
using Quartz;

namespace CochainAPI.Jobs
{
    public class TokenProcessor : IJob
    {
        private readonly IProductLifeCycleService _lifeCycleService;

        public TokenProcessor(IProductLifeCycleService lifeCycleService)
        {
            _lifeCycleService = lifeCycleService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.CompletedTask;
        }
    }
}
