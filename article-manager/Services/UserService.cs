using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using backendlib.Models;
using Microsoft.AspNetCore.Components;


namespace article_manager.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;


        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUser(int id)
        {
            return await _httpClient.GetJsonAsync<User>($"api/User/Get/{id}");
        }

        public async Task<string> Login(User user)
        {

            HttpResponseMessage httpResponse;
            httpResponse = await _httpClient.PostAsJsonAsync<User>("api/User/Login", user);
            var content = await httpResponse.Content.ReadAsStringAsync();

            return content.ToString();

        }
    }
}