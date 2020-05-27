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
            var objects = new List<Type>
            {
                typeof(Constraint),
                typeof(Function),
                typeof(Index),
                typeof(Procedure),
                typeof(Schema),
                typeof(Table)
            };

            Objects = objects;
        }

        public static IEnumerable<Type> Objects { get; }
    }
}
