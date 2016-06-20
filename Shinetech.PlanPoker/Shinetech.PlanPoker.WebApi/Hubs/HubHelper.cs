using Shinetech.PlanPoker.Logic.Tools;

namespace Shinetech.PlanPoker.WebApi.Hubs
{
    public class HubHelper
    {
        public static string GetProjectId(string code)
        {
            var result = -1;
            int.TryParse(code, out result);
            if (result <= 0)
            {
               return TokenGenerator.DecodeToken(code);
            }
            return code;
        }
    }
}