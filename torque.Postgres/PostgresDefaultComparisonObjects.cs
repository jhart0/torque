using System;
using System.Collections.Generic;
using torque.Common.Models;

namespace torque.Postgres
{
    public static class PostgresDefaultComparisonObjects
    {
        static PostgresDefaultComparisonObjects()
        {
            var objects = new List<Type>();
            objects.AddRange(GenericDefaults.Objects);
            objects.Add(typeof(Type));

            Objects = objects;
        }

        public static IEnumerable<Type> Objects { get; }
    }
}
