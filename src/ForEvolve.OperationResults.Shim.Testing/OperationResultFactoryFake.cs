using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ForEvolve.AspNetCore
{
    public class OperationResultFactoryFake : IOperationResultFactory
    {
        public IOperationResultFactory Instance { get; }

        public OperationResultFactoryFake()
        {
            var services = new ServiceCollection();
            services
                .AddSingleton<IHostingEnvironment, HostingEnvironment>()
                .AddForEvolveOperationResults();

            Instance = services
                .BuildServiceProvider()
                .GetService<IOperationResultFactory>();
        }

        public IOperationResult Create()
        {
            return Instance.Create();
        }

        public IOperationResult<TResult> Create<TResult>()
        {
            return Instance.Create<TResult>();
        }

        public IOperationResult<TResult> Create<TResult>(TResult result)
        {
            return Instance.Create(result);
        }
    }
}