using System;
using System.Linq;

namespace torque.Models.DatabaseObjects
{
    public struct Table
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

        public string GetColumnDropDefinition()
        {
            return $"ALTER TABLE {this.GetCanonicalTableName()} DROP COLUMN {this.ColumnName}";
        }

        public string GetColumnCreateDefinition()
        {
            return $"ALTER TABLE {this.GetCanonicalTableName()} ADD {this.GetColumnDefinition(this)}";
        }

        public string GetTableCreateDefinition(IGrouping<object, Table> table)
        {
            var createScript = $"CREATE TABLE {this.GetCanonicalTableName()} (";
            foreach (var column in table)
            {
                createScript += $"{this.GetColumnDefinition(column)},";
            }
            createScript = createScript.Remove(createScript.Length - 1);
            createScript += ")";

            return createScript;
        }

        public string GetCanonicalTableName()
        {
            return $"{this.Schema}.{this.TableName}";
        }

        private string GetColumnDefinition(Table table)
        { 
            return $"{table.ColumnName} {table.DataType}" +
                $"{(table.IsIdentity ? " IDENTITY(1, 1)" : string.Empty)}" +
                $"{(table.NotNull ? " NOT NULL" : " NULL")}" +
                $"{table.DefaultDefinition}";
        }
    }
}
