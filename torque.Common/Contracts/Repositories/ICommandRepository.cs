using System.Threading.Tasks;

namespace torque.Common.Contracts.Repositories
{
    public interface ICommandRepository
    {
        Task ExecuteQuery(string connectionString, string command);
    }
}
