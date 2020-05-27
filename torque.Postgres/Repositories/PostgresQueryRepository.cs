using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using torque.Common.Contracts.Repositories;

namespace torque.Postgres.Repositories
{
    public class PostgresQueryRepository : IQueryRepository
    {
        public async Task<IEnumerable<T>> FetchEntities<T>(string connectionString, string query)
        {
            using var conn = new NpgsqlConnection(connectionString);
            return await conn.QueryAsync<T>(query);
        }
    }
}
