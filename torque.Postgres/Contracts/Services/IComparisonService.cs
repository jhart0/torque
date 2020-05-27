using System.Threading.Tasks;
using torque.Common.Models;

namespace torque.Postgres.Contracts.Services
{
    public interface IComparisonService
    {
        Task<string> GenerateComparison(ExecutionContext context);

        Task DeployComparison(ExecutionContext contextn);
    }
}
