using System;
using System.Collections.Generic;
using torque.Common.Models;

namespace torque.Postgres
{
    public static class PostgresDefaultComparisonObjects
    {
        static PostgresDefaultComparisonObjects()
        {
            var objects = new List<string>();
            objects.AddRange(GenericDefaults.Objects);
            objects.Add(nameof(Type));

            Objects = objects;
        }

        public static IEnumerable<string> Objects { get; }
    }
}
