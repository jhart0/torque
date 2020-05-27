using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using torque.Common.Contracts.Repositories;
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

        private readonly IQueryRepository _queryRepository;

        public BaseEntityComparisonService(IConstraintMappingService constraintMappingService,
            IQueryRepository queryRepository)
        {
            this._rm = new ResourceManager("DatabaseObjects", Assembly.GetExecutingAssembly());
            this._constraintMappingService = constraintMappingService;
            this._queryRepository = queryRepository;
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
                        var sourceConstraints = await this._queryRepository.FetchEntities<Constraint>(context.FromConnString, cquery);
                        var destConstraints = await this._queryRepository.FetchEntities<Constraint>(context.ToConnString, cquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceConstraints, destConstraints));
                        break;
                    case Function f:
                        var fquery = this._rm.GetString("Function");
                        var sourceFunctions = await this._queryRepository.FetchEntities<Function>(context.FromConnString, fquery);
                        var destFunctions = await this._queryRepository.FetchEntities<Function>(context.ToConnString, fquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceFunctions, destFunctions));
                        break;
                    case Index i:
                        var iquery = this._rm.GetString("Index");
                        var sourceIndexes = await this._queryRepository.FetchEntities<Index>(context.FromConnString, iquery);
                        var destIndexes = await this._queryRepository.FetchEntities<Index>(context.ToConnString, iquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceIndexes, destIndexes));
                        break;
                    case Procedure p:
                        var pquery = this._rm.GetString("Procedure");
                        var sourceProcedures = await this._queryRepository.FetchEntities<Procedure>(context.FromConnString, pquery);
                        var destProcedures = await this._queryRepository.FetchEntities<Procedure>(context.ToConnString, pquery);
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
    }
}
