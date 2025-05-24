using Microsoft.AspNetCore.Mvc;
using FeedbackAPI.Models;
using System;
using System.Linq;

namespace FeedbackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostFeedback(Feedback feedback)
        {
            if (FeedbackStore.Feedbacks.Any(f => f.PostId == feedback.PostId))
            {
                return BadRequest("Ya existe feedback para este post.");
            }

            feedback.Fecha = DateTime.Now;
            FeedbackStore.Feedbacks.Add(feedback);
            return Ok(feedback);
        }

        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            return Ok(FeedbackStore.Feedbacks);
        }
    }
}