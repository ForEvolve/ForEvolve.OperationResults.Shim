using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
#if NET5_0
using Microsoft.Extensions.Hosting.Internal;
#else
using Microsoft.AspNetCore.Hosting.Internal;
#endif
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