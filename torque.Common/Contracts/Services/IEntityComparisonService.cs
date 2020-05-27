using System.Collections.Generic;
using System.Threading.Tasks;
using torque.Common.Models;

namespace torque.Common.Contracts.Services
{
    public interface IEntityComparisonService
    {
        Task<IEnumerable<ComparisonResult>> CompareObjects(ExecutionContext context);
    }
}
