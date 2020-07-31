using System.Collections.Generic;
using System.Linq;
using torque.Common.Contracts.Services;
using torque.Common.Extensions;
using torque.Common.Models;
using torque.Common.Models.Contracts.DatabaseObjects;
using torque.Models.DatabaseObjects;

namespace torque.Common.Services
{
    public class ObjectMappingService : IObjectMappingService
    {
        public IEnumerable<ComparisonResult> MapObjects<T>(IEnumerable<T> sourceObjects, IEnumerable<T> destObjects) where T : IComparableEntity
        {
            var results = new List<ComparisonResult>();
            var only2 = destObjects.Except(sourceObjects);

            foreach (var item in sourceObjects)
            {
                if (destObjects.Any(it => it.Equals(item)))
                    break;

                if (destObjects.Any(it => it.Name == item.Name && it.Schema == item.Schema))
                    results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.InBothButDifferent, item.Definition, item, item.GetCanonicalName()));

                results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.OnlyInSource, item.Definition, item, item.GetCanonicalName()));
            }

            foreach (var item in only2)
            {
                results.Add(new ComparisonResult(typeof(T), Enum.ComparisonDirection.OnlyInDest, item.Definition, item, item.GetCanonicalName()));
            }

            return results;
        }

        public IEnumerable<ComparisonResult> MapTables(IEnumerable<Table> sourceTableColumns, IEnumerable<Table> destTableColumns)
        {
            var results = new List<ComparisonResult>();
            var sourceTables = sourceTableColumns.GroupBy(it => new { it.Schema, it.TableName });
            var destTables = destTableColumns.GroupBy(it => new { it.Schema, it.TableName });

            foreach (var table in sourceTables)
            {
                if (destTables.Any(it => table.Key.Schema == it.Key.Schema && it.Key.TableName == it.Key.TableName))
                {
                    var destTable = destTables.First(it => table.Key.Schema == it.Key.Schema && it.Key.TableName == it.Key.TableName);
                    foreach (var column in table)
                    {
                        if (!destTable.Any(it => it.ColumnName == column.ColumnName))
                        { 
                            results.Add(new ComparisonResult(typeof(Table), Enum.ComparisonDirection.OnlyInDest, column.GetColumnDropDefinition(), column, column.ColumnName));
                        }
                    }
                    foreach (var column in destTable)
                    {
                        if (!table.Any(it => it.ColumnName == column.ColumnName))
                        {
                            results.Add(new ComparisonResult(typeof(Table), Enum.ComparisonDirection.OnlyInSource, column.GetColumnCreateDefinition(), column, column.ColumnName));
                        }
                    }
                }
                else
                {
                    results.Add(new ComparisonResult(typeof(Table), Enum.ComparisonDirection.OnlyInSource, table.First().GetTableCreateDefinition(table), table, table.First().GetCanonicalTableName()));
                }

                //TODO: drop tables not in source but in dest
            }

            return results;
        }
    }
}
