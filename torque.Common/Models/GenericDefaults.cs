using System;
using System.Collections.Generic;
using torque.Models.DatabaseObjects;
using Index = torque.Models.DatabaseObjects.Index;

namespace torque.Common.Models
{
    public static class GenericDefaults
    {
        static GenericDefaults()
        {
            var objects = new List<string>
            {
                nameof(Constraint),
                nameof(Function),
                nameof(Index),
                nameof(Procedure),
                nameof(Schema),
                nameof(Table)
            };

            Objects = objects;
        }

        public static IEnumerable<string> Objects { get; }
    }
}
