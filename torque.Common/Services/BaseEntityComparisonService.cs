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
        private readonly IConstraintMappingService _constraintMappingService;

        private readonly IQueryRepository _queryRepository;

        public BaseEntityComparisonService(IConstraintMappingService constraintMappingService,
            IQueryRepository queryRepository)
        {
            this._constraintMappingService = constraintMappingService;
            this._queryRepository = queryRepository;
        }

        public async Task<IEnumerable<ComparisonResult>> CompareObjects(ExecutionContext context)
        {
            var results = new List<ComparisonResult>();
            var rm = context.ResourceManager;
            foreach (var objectType in context.ObjectsToDeploy)
            {
                switch (objectType)
                {
                    case nameof(Constraint):
                        var cquery = rm.GetString("Constraint");
                        var sourceConstraints = await this._queryRepository.FetchEntities<Constraint>(context.FromConnString, cquery);
                        var destConstraints = await this._queryRepository.FetchEntities<Constraint>(context.ToConnString, cquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceConstraints, destConstraints));
                        break;
                    case nameof(Function):
                        var fquery = rm.GetString("Function");
                        var sourceFunctions = await this._queryRepository.FetchEntities<Function>(context.FromConnString, fquery);
                        var destFunctions = await this._queryRepository.FetchEntities<Function>(context.ToConnString, fquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceFunctions, destFunctions));
                        break;
                    case nameof(Index):
                        var iquery = rm.GetString("Index");
                        var sourceIndexes = await this._queryRepository.FetchEntities<Index>(context.FromConnString, iquery);
                        var destIndexes = await this._queryRepository.FetchEntities<Index>(context.ToConnString, iquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceIndexes, destIndexes));
                        break;
                    case nameof(Procedure):
                        var pquery = rm.GetString("Procedure");
                        var sourceProcedures = await this._queryRepository.FetchEntities<Procedure>(context.FromConnString, pquery);
                        var destProcedures = await this._queryRepository.FetchEntities<Procedure>(context.ToConnString, pquery);
                        results.AddRange(this._constraintMappingService.MapObjects(sourceProcedures, destProcedures));
                        break;
                    case nameof(Schema):
                        //TODO
                        break;
                    case nameof(Table):
                        //TODO
                        break;
                }
            }

            return results;
        }
    }
}
