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

        public string GetConstraintDropDefinition()
        {
            return $"ALTER TABLE {this.GetCanonicalTableName()} DROP COLUMN {this.Name}";
        }

        public string GetConstraintCreateDefinition()
        {
            return $"ALTER TABLE {this.GetCanonicalTableName()} ADD {this.Definition}";
        }

        public string GetCanonicalTableName()
        {
            return $"{this.Schema}.{this.TableName}";
        }

        public bool Equals(IComparableEntity other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name
                && this.Schema == other.Schema
                && this.Definition == other.Definition;
        }

        public override bool Equals(object obj) => Equals(obj as IComparableEntity);
        public override int GetHashCode() => (Name, Schema, Definition).GetHashCode();
    }
}
