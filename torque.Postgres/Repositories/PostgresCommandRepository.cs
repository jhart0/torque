using Dapper;
using Npgsql;
using System.Threading.Tasks;
using torque.Common.Contracts.Repositories;

namespace torque.Postgres.Repositories
{
    public class PostgresCommandRepository : ICommandRepository
    {
        public async Task ExecuteQuery(string connectionString, string command)
        {
            using var conn = new NpgsqlConnection(connectionString);
            await conn.ExecuteAsync(command);
        }
    }
}
