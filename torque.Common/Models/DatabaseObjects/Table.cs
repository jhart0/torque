namespace torque.Models.DatabaseObjects
{
    public abstract class Table
    {
        public string RelationType { get; set; } //pg: c == type, r == table

        public string Schema { get; set; }

        public string TableName { get; set; }

        public int Ordinal { get; set; }

        public string ColumnName { get; set; }

        public bool NotNull { get; set; }

        public bool IsIdentity { get; set; }

        public string DefaultDefinition { get; set; }

        public string DataType { get; set; }
    }
}
