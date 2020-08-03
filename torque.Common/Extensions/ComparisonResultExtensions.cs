using System;
using System.Collections.Generic;
using System.Linq;
using torque.Common.Models;
using torque.Common.Models.Contracts.DatabaseObjects;
using torque.Models.DatabaseObjects;
using Index = torque.Models.DatabaseObjects.Index;

namespace torque.Common.Extensions
{
    public static class ComparisonResultExtensions
    {
        public static IEnumerable<ComparisonOutput> SortDependencies(this IEnumerable<ComparisonOutput> comparisons)
        {
            return comparisons.OrderBy(it =>
                it.ObjectType == typeof(Schema) ? 1 :
                it.ObjectType == typeof(Table) ? 2 :
                it.ObjectType == typeof(Function) ? 3 :
                it.ObjectType == typeof(Procedure) ? 4 :
                it.ObjectType == typeof(Constraint) ? 5 :
                it.ObjectType == typeof(Index) ? 6 :
                7
                );
        }

        public static string GetCanonicalName(this IComparableEntity entity)
        {
            if (entity is Constraint)
                return $"{entity.Name}";

            return $"{entity.Schema}.{entity.Name}";
        }
    }
}
