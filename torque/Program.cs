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
    public class Program
    {
        public static ILoggingService loggingService;

        public static async Task Main(string[] args)
        {
            loggingService ??= new ConsoleLoggingService();

            if (args.Length == 1 && args[0] == "help")
            {
                ShowHelpText();
            }
            else if (args.Length == 4 && (args[0] == "sync" || args[0] == "compare"))
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
                var context = new ExecutionContext(args[2], args[3], args[0] == "sync",
                    PostgresDefaultComparisonObjects.Objects, rm);
                var diff = await transferService.GenerateComparison(context);
                loggingService.Log(diff);
            }
            else if (args.Length == 4 && args[0] == "deploy")
            {
                var serviceCollection = new ServiceCollection()
                    .AddTransient<ICommandRepository, PostgresCommandRepository>();

                var serviceProvider = serviceCollection.BuildServiceProvider();
                var commandRepository = serviceProvider.GetService<ICommandRepository>();
                await commandRepository.ExecuteQuery(args[2], args[3]);
            }
            else
            {
                loggingService.Log("Please choose a valid command:");
                ShowHelpText();
            }
        }

        private static void ShowHelpText()
        {
            loggingService.Log("torque Database Comparison & Synchronization Tool");
            loggingService.Log(Environment.NewLine);
            loggingService.Log("Available Commands:");
            loggingService.Log("compare (compare 2 db schemas):");
            loggingService.Log("compare <dbms> <from connection string> <to connection string>");
            loggingService.Log("sync (sync 2 db schemas):");
            loggingService.Log("sync <dbms> <from connection string> <to connection string>");
            loggingService.Log("deploy (deploy script to target):");
            loggingService.Log("deploy <dbms> <to connection string> <script to run>");
        }
    }
}
