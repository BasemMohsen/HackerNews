﻿using Newtonsoft.Json;

namespace HackerNews.ApiClient.Models
{
    public class Story
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public string PostedBy { get; set; }
        public long Time { get; set; }

        public int Score { get; set; }
        public int CommentCount { get; set; }
    }
}
