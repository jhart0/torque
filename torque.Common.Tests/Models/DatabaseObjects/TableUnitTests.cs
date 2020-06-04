using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using torque.Models.DatabaseObjects;

namespace torque.Common.Tests
{
    public class TableUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenATableDefinitionIsSupplied_WhenGetTableCreateDefinition_ThenFullCreateScriptIsReturned()
        {
            var table = new List<Table>();
            var column1 = new Table();
            column1.ColumnName = "ColumnA";
            column1.DataType = "int";
            column1.DefaultDefinition = string.Empty;
            column1.IsIdentity = true;
            column1.NotNull = true;
            column1.Schema = "public";
            column1.TableName = "NewTable";
            var column2 = new Table();
            column2.ColumnName = "ColumnB";
            column2.DataType = "varchar(50)";
            column2.DefaultDefinition = string.Empty;
            column2.IsIdentity = false;
            column2.NotNull = false;
            column2.Schema = "public";
            column2.TableName = "NewTable";
            table.Add(column1);
            table.Add(column2);

            var groups = table.GroupBy(it => new { it.Schema, it.TableName });

            foreach (var group in groups)
            {
                var actual = column1.GetTableCreateDefinition(group);
                var expected = $"CREATE TABLE public.NewTable (ColumnA int IDENTITY(1, 1) NOT NULL,ColumnB varchar(50) NULL)";

                Assert.AreEqual(expected, actual);
            }
        }
    }
}