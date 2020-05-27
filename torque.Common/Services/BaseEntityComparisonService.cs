using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using torque.Common.Contracts.Services;
using torque.Common.Models;
using torque.Models.DatabaseObjects;
using Index = torque.Models.DatabaseObjects.Index;

namespace torque.Common.Services
{
    public class BaseEntityComparisonService : IEntityComparisonService
    {
        private readonly ResourceManager _rm;

        private readonly IConstraintMappingService _constraintMappingService;

        public BaseEntityComparisonService(IConstraintMappingService constraintMappingService)
        {
            this._rm = new ResourceManager("DatabaseObjects", Assembly.GetExecutingAssembly());
            this._constraintMappingService = constraintMappingService;
        }

        public async Task<IEnumerable<ComparisonResult>> CompareObjects(ExecutionContext context)
        {
            var results = new List<ComparisonResult>();
            foreach (var objectType in context.ObjectsToDeploy)
            {
                switch ((object)objectType)
                {
                    case Constraint c:
                        var cquery = this._rm.GetString("Constraint");
                        var sourceConstraints = await this.GetConstraints(context.FromConnString, cquery);
                        var destConstraints = await this.GetConstraints(context.ToConnString, cquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceConstraints, destConstraints));
                        break;
                    case Function f:
                        var fquery = this._rm.GetString("Function");
                        var sourceFunctions = await this.GetFunctions(context.FromConnString, fquery);
                        var destFunctions = await this.GetFunctions(context.ToConnString, fquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceFunctions, destFunctions));
                        break;
                    case Index i:
                        var iquery = this._rm.GetString("Index");
                        var sourceIndexes = await this.GetIndexes(context.FromConnString, iquery);
                        var destIndexes = await this.GetIndexes(context.ToConnString, iquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceIndexes, destIndexes));
                        break;
                    case Procedure p:
                        var pquery = this._rm.GetString("Procedure");
                        var sourceProcedures = await this.GetProcedures(context.FromConnString, pquery);
                        var destProcedures = await this.GetProcedures(context.ToConnString, pquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceProcedures, destProcedures));
                        break;
                    case Schema s:
                        //TODO
                        break;
                    case Table t:
                        //TODO
                        break;
                }
            }

            return results;
        }

        private async Task<IEnumerable<Constraint>> GetConstraints(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Constraint>(query);
        }

        private async Task<IEnumerable<Function>> GetFunctions(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Function>(query);
        }

        private async Task<IEnumerable<Index>> GetIndexes(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Index>(query);
        }

        private async Task<IEnumerable<Procedure>> GetProcedures(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Procedure>(query);
        }

        private async Task<IEnumerable<Schema>> GetSchemas(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Schema>(query);
        }

        private async Task<IEnumerable<Table>> GetTables(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<Table>(query);
        }
    }
}
