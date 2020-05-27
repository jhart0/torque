using System.Collections.Generic;
using System.Threading.Tasks;

namespace torque.Common.Contracts.Repositories
{
    public interface IQueryRepository
    {
        Task<IEnumerable<T>> FetchEntities<T>(string connectionString, string query);
    }
}
