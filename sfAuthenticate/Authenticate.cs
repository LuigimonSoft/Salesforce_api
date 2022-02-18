using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace sfAuthenticate
{
    public class Authenticate : sRequestBase.RequestBase
    {
        public string Password { private get; set; }
        public string SecretToken { private get; set; }
        public string Username {  get; set; }
        public GrantTypes grant_type = 0;
        private string _client_id {  get; set; }
        private string _client_secret {  get; set; }

        public Authenticate(string domain,string client_id,string client_secret, string username, string password, string secrettoken)
        {
            Url = "/services/oauth2";
            Username = username;
            Password = password;
            SecretToken= secrettoken;
            Domain = domain;
            _client_id = client_id;
            _client_secret = client_secret;
        }

        public async Task<(bool isSuccess, Models.UserAuthenticate?)> getToken(GrantTypes grant_type)
        {
            if (grant_type == GrantTypes.password)
                return await TokenFromPassword();
            else
            {
                return (false, null);
            }

                      
        }

        private async Task<(bool isSuccess, Models.UserAuthenticate? AuthenticateData)> TokenFromPassword()
        {
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", _client_id},
                {"client_secret", _client_secret},
                {"username", Username},
                {"password", string.Concat(Password,SecretToken)}
            });

            HttpResponseMessage httpResponseMessage = await SendPostForm("/token", content);
            if (httpResponseMessage.IsSuccessStatusCode)
                return (true, await JsonSerializer.DeserializeAsync<Models.UserAuthenticate>(await httpResponseMessage.Content.ReadAsStreamAsync()));
            else
            {
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    return (false, await JsonSerializer.DeserializeAsync<Models.UserAuthenticate>(await httpResponseMessage.Content.ReadAsStreamAsync()));
                else
                    return (false, null);
            }


        }
    }
}
