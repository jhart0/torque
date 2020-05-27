using torque.Common.Models.Contracts.DatabaseObjects;

namespace torque.Models.DatabaseObjects
{
    public struct Index : IComparableEntity
    {
        public string Schema { get; set; }

        public string Name { get; set; }

        public string TableName { get; set; }

        public string Definition { get; set; }
    }
}
