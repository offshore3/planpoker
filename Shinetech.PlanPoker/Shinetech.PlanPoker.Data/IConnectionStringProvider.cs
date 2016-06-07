using System.Configuration;

namespace Shinetech.PlanPoker.Data
{
    public interface IConnectionStringProvider
    {
        ConnectionStringSettings GetConnectionString(string parameterName);
    }
}