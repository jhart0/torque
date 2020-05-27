using System;
using torque.Common.Enum;

namespace torque.Common.Models
{
    public struct ComparisonResult
    {
        public ComparisonResult(Type objectType,
            ComparisonDirection direction,
            string objectDiff,
            object entity,
            string canonicalName)
        {
            ObjectType = objectType;
            Direction = direction;
            ObjectDiff = objectDiff;
            Entity = entity;
            CanonicalName = canonicalName;
        }

        public Type ObjectType { get; }

        public ComparisonDirection Direction { get; }

        public string ObjectDiff { get; }

        public object Entity { get; }

        public string CanonicalName { get; }
    }
}
