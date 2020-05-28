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
               .AddTransient<IConstraintMappingService, ConstraintMappingService>()
               .AddTransient<IEntityComparisonService, BaseEntityComparisonService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var transferService = serviceProvider.GetService<IComparisonService>();
            var rm = Postgres.Resources.DatabaseObjects.ResourceManager;
            var context = new ExecutionContext("Server=localhost;Database=postgres;User ID=postgres;Password=admin", "Server=localhost;Database=salesdw;User ID=postgres;Password=admin", false, 
                PostgresDefaultComparisonObjects.Objects, rm);
            var diff = await transferService.GenerateComparison(context);
            Console.Write(diff);
            Console.Read();
        }
    }
}
