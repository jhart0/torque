using torque.Common.Models.Contracts.DatabaseObjects;

namespace torque.Models.DatabaseObjects
{
    public struct Constraint : IComparableEntity
    {
        public string Schema { get; set; }

        public string Name { get; set; }

        public string TableName { get; set; }

        public string Definition { get; set; }

        public string Type { get; set; }
    }
}
