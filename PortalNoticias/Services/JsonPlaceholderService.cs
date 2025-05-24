using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using PortalNoticias.Models;

namespace PortalNoticias.Services
{
    public class JsonPlaceholderService
    {
        private readonly HttpClient _http;

        public JsonPlaceholderService(HttpClient http) => _http = http;

        public async Task<List<Post>> GetPostsAsync() =>
            await _http.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts");

        public async Task<User> GetUserAsync(int userId) =>
            await _http.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{userId}");

        public async Task<List<Comment>> GetCommentsAsync(int postId) =>
            await _http.GetFromJsonAsync<List<Comment>>($"https://jsonplaceholder.typicode.com/comments?postId={postId}");
    }
}
