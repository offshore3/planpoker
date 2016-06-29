using Newtonsoft.Json;

namespace Shinetech.PlanPoker.LogicModel
{
    public class LinkedInLogicModel
    {
        [JsonProperty("emailAddress")]
        public string Email { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("pictureUrl")]
        public string Picture { get; set; }
    }

    public class LinkedInAuthResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }
    }
}