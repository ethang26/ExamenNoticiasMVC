using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PortalNoticias.Models;
using PortalNoticias.Services;

namespace PortalNoticias.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonPlaceholderService _service;
        private readonly HttpClient _feedbackClient;

        public HomeController(JsonPlaceholderService service, IHttpClientFactory factory)
        {
            _service = service;
            _feedbackClient = factory.CreateClient("feedback");
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _service.GetPostsAsync();
            return View(posts);
        }

        public async Task<IActionResult> Post(int id)
        {
            var post = (await _service.GetPostsAsync()).First(p => p.Id == id);
            var user = await _service.GetUserAsync(post.UserId);
            var comments = await _service.GetCommentsAsync(id);
            ViewBag.User = user;
            ViewBag.Comments = comments;
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(int postId, string sentimiento)
        {
            var feedback = new { PostId = postId, Sentimiento = sentimiento, Fecha = DateTime.Now };
            var res = await _feedbackClient.PostAsJsonAsync("/api/feedback", feedback);
            if (!res.IsSuccessStatusCode)
            {
                TempData["Error"] = "Ya diste feedback.";
            }
            return RedirectToAction("Post", new { id = postId });
        }
    }
}
