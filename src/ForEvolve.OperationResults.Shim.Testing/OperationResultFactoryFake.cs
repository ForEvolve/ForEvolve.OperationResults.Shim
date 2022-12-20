using ForEvolve.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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

        private class HostingEnvironment : IHostingEnvironment
        {
            public string EnvironmentName { get; set; }
            public string ApplicationName { get; set; }
            public string WebRootPath { get; set; }
            public IFileProvider WebRootFileProvider { get; set; }
            public string ContentRootPath { get; set; }
            public IFileProvider ContentRootFileProvider { get; set; }
        }
    }
}