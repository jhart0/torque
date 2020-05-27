namespace torque.Common.Models.Contracts.DatabaseObjects
{
    public interface IComparableEntity
    {
        string Name { get; }

        string Schema { get; }

        string Definition { get; }
    }
}
