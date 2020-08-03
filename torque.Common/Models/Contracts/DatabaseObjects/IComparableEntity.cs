using System;

namespace torque.Common.Models.Contracts.DatabaseObjects
{
    public interface IComparableEntity : IEquatable<IComparableEntity>
    {
        string Name { get; }

        string Schema { get; }

        string Definition { get; }
    }
}
