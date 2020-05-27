using System;
using System.Threading.Tasks;
using torque.Common.Contracts.Services;
using torque.Common.Models;
using torque.Postgres.Contracts.Services;

namespace torque.Postgres.Services
{
    public class PostgresComparisonService : IComparisonService
    {
        private readonly IEntityComparisonService _entityComparisonService;

        public PostgresComparisonService(IEntityComparisonService entityComparisonService)
        {
            this._entityComparisonService = entityComparisonService;
        }

        public Task DeployComparison(ExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateComparison(ExecutionContext context)
        {
            var diff = string.Empty;
            var differences = await this._entityComparisonService.CompareObjects(context);

            return diff;
        }
    }
}
