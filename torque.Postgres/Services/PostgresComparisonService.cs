using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using torque.Common.Contracts.Repositories;
using torque.Common.Contracts.Services;
using torque.Common.Enum;
using torque.Common.Extensions;
using torque.Common.Models;
using torque.Models.DatabaseObjects;
using torque.Postgres.Contracts.Services;

namespace torque.Postgres.Services
{
    public class PostgresComparisonService : IComparisonService
    {
        private readonly IEntityComparisonService _entityComparisonService;

        private readonly ICommandRepository _commandRepository;

        private readonly string _statementTerminator = ";";

        public PostgresComparisonService(IEntityComparisonService entityComparisonService,
            ICommandRepository commandRepository)
        {
            this._entityComparisonService = entityComparisonService;
            this._commandRepository = commandRepository;
        }

        public async Task DeployComparison(ExecutionContext context)
        {
            var comparison = await this.GenerateComparison(context);
            if (context.Deploy)
                await this._commandRepository.ExecuteQuery(context.ToConnString, comparison);
        }

        public async Task<string> GenerateComparison(ExecutionContext context)
        {
            var diff = new StringBuilder();
            var differences = await this._entityComparisonService.CompareObjects(context);

            //Creates First
            var toCreate = differences.Where(it => it.Direction == ComparisonDirection.OnlyInSource);
            toCreate.SortDependencies();

            foreach (var t in toCreate)
            {
                diff.Append(t.ObjectDiff);
                diff.Append(_statementTerminator);
                diff.Append(Environment.NewLine);
            }

            //Then Differences
            //TODO

            //Drops Last
            var toDrop = differences.Where(it => it.Direction == ComparisonDirection.OnlyInDest);
            toDrop.SortDependencies();

            foreach (var t in toDrop)
            {
                if(t.ObjectType.Name == nameof(Constraint))
                    diff.Append($"ALTER TABLE {((Constraint)t.Entity).Schema}.{((Constraint)t.Entity).TableName} DROP {t.ObjectType.Name} {t.CanonicalName}{_statementTerminator}");
                else
                    diff.Append($"DROP {t.ObjectType.Name} {t.CanonicalName}{_statementTerminator}");
                diff.Append(Environment.NewLine);
            }

            return diff.ToString();
        }
    }
}
