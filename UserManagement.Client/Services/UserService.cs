using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using UserManagement.Client.Models;
using Microsoft.Extensions.Options;

namespace UserManagement.Client.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private const string DefaultUserApiUrl = "https://jsonplaceholder.typicode.com/users"; // Default URL

        public UserService(HttpClient httpClient, IOptions<APISettings> apiSettings)
        {
            _httpClient = httpClient;

            // Set the base URL; fall back to a default if the value is missing
            _baseUrl = !string.IsNullOrEmpty(apiSettings.Value.UserAPIUrl)
                ? apiSettings.Value.UserAPIUrl
                : DefaultUserApiUrl;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            // Ensure _baseUrl is an absolute URI
            if (string.IsNullOrWhiteSpace(_baseUrl) || !Uri.IsWellFormedUriString(_baseUrl, UriKind.Absolute))
            {
                throw new InvalidOperationException("The base URL for the User API is not valid.");
            }

            return await _httpClient.GetFromJsonAsync<List<User>>(_baseUrl);
        }

        public bool DeleteUserAsync(int id)
        {
            // Dummy implementation for deletion logic
            return true;
        }
    }
}
