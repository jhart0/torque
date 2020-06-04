using System.Collections.Generic;
using torque.Common.Models;
using torque.Common.Models.Contracts.DatabaseObjects;
using torque.Models.DatabaseObjects;

namespace torque.Common.Contracts.Services
{
    public interface IObjectMappingService
    {
        IEnumerable<ComparisonResult> MapObjects<T>(IEnumerable<T> sourceObjects, IEnumerable<T> destObjects) where T : IComparableEntity;

        IEnumerable<ComparisonResult> MapTables(IEnumerable<Table> sourceTableColumns, IEnumerable<Table> destTableColumns);
    }
}
