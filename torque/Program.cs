using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using torque.Common.Contracts.Repositories;
using torque.Common.Contracts.Services;
using torque.Common.Models;
using torque.Common.Services;
using torque.Postgres;
using torque.Postgres.Contracts.Services;
using torque.Postgres.Repositories;
using torque.Postgres.Services;

namespace torque
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection()
               .AddTransient<IComparisonService, PostgresComparisonService>()
               .AddTransient<IQueryRepository, PostgresQueryRepository>()
               .AddTransient<ICommandRepository, PostgresCommandRepository>()
               .AddTransient<IObjectMappingService, ObjectMappingService>()
               .AddTransient<IEntityComparisonService, BaseEntityComparisonService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var transferService = serviceProvider.GetService<IComparisonService>();
            var rm = Postgres.Resources.DatabaseObjects.ResourceManager;
            var context = new ExecutionContext(args[0], args[1], false, 
                PostgresDefaultComparisonObjects.Objects, rm);
            var diff = await transferService.GenerateComparison(context);
            Console.Write(diff);
        }
    }
}
