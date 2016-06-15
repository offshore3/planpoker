namespace Shinetech.PlanPoker.Logic.Tools
{
    public class TokenGenerator
    {
        public static string Generate(string email, string password,string dateTime)
        {
            var token = string.Format("{0}&{1}&{2}", email, password, dateTime);
            return Base64Helper.Base64Encode(token);
        }

        public static string EncodeToken(string token)
        {
            return Base64Helper.Base64Encode(token);
        }

        public static string DecodeToken(string token)
        {
            return Base64Helper.Base64Decode(token);
        }
    }
}