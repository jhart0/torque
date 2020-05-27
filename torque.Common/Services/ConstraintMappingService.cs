using System.Collections.Generic;
using System.Linq;
using torque.Common.Contracts.Services;
using torque.Common.Models;
using torque.Common.Models.Contracts.DatabaseObjects;
using torque.Models.DatabaseObjects;

namespace torque.Common.Services
{
    public class ConstraintMappingService : IConstraintMappingService
    {
        public IEnumerable<ComparisonResult> MapObjects<T>(IEnumerable<T> sourceObjects, IEnumerable<T> destObjects) where T : IComparableEntity
        {
            var results = new List<ComparisonResult>();
            var only2 = destObjects.Except(sourceObjects);

            foreach (var item in sourceObjects)
            {
                if (destObjects.Any(it => it.Equals(item)))
                    break;

                if (destObjects.Any(it => it.Name == item.Name && it.Schema == item.Schema))
                    results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.InBothButDifferent, item.Definition, item));

                results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.OnlyInSource, item.Definition, item));
            }

            foreach (var item in only2)
            {
                results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.OnlyInDest, item.Definition, item));
            }

            return results;
        }
    }
}
