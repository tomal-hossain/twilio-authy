using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace TwilioAuthy
{
    public class Authy
    {
        private readonly HttpClient _client;

        public Authy(string apiKey)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-Authy-API-Key", apiKey);
        }

        public async Task<int> CreateUser(string email, string cellPhone, string countryCode)
        {
            try
            {
                var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("user[email]", email),
                    new KeyValuePair<string, string>("user[cellphone]", cellPhone),
                    new KeyValuePair<string, string>("user[country_code]", countryCode),
                });

                HttpResponseMessage response = _client.PostAsync("https://api.authy.com/protected/json/users/new", requestContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var serializeOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string responseBody = await response.Content.ReadAsStringAsync();
                    ResponseModel responseModel = JsonSerializer.Deserialize<ResponseModel>(responseBody, serializeOptions);
                    return responseModel.User.Id;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public bool SendOTP(int authyId)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"https://api.authy.com/protected/json/sms/{authyId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool VerifyOTP(int authyId, string token)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"https://api.authy.com/protected/json/verify/{token}/{authyId}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        public bool RemoveUser(int authyId)
        {
            try
            {
                HttpResponseMessage response = _client.PostAsync($"https://api.authy.com/protected/json/users/{authyId}/remove", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}
