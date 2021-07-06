using Newtonsoft.Json;

namespace PersonalPatientAccount.Models.ModelsView
{
    public class Token
    {
        public Token(string access_token, string username)
        {
            AccessToken = access_token;
            Username = username;
        }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
