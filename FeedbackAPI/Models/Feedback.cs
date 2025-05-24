using System;

namespace FeedbackAPI.Models
{
    public class Feedback
    {
        public int PostId { get; set; }
        public string Sentimiento { get; set; } // "like" o "dislike"
        public DateTime Fecha { get; set; }
    }
}