using JamesMoonPortfolioRedux.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filepath = @".\wwwroot\data\Comment.json";

        public IActionResult Index()
        {
            ViewBag.Comments = GetComments();
            return View();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            try // try the api to see if it is capable of returning comments
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync("https://jamesmoonitprofilecomments.azurewebsites.net/Comment/GetComments").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(jsonResponse) ?? new List<Comment>();
                    }
                    else
                    {
                        throw null;
                    }
                }
            }
            catch // if the api fails to return comments, fall back to backup
            {
                comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath)) ?? new List<Comment>();
            }
            return comments;
        }

        public List<Comment> StoreComment(string author, string comment)
        {
            List<Comment> allComments = new List<Comment>();
            if (ValidateComment(author, comment)) // If the comment and author name do not contain profanity
            {
                if (!IsApiDown()) // If the api could be contacted on page load
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = client.PutAsync("https://jamesmoonitprofilecomments.azurewebsites.net/Comment/AddComment?author=" + author + "&comment=" + comment, null).Result; // Send author and comment to external api
                        if (response.Content.ReadAsStringAsync().Result == "true")
                        {
                            //Add check if process is successful. Not a required feature yet, but set up incase I want to use it later.
                        }
                    }
                }
                allComments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath)) ?? new List<Comment>();
                allComments.Add(
                    new Comment
                    {
                        commentID = allComments.Max(c => c.commentID) + 1,
                        commentAuthor = author,
                        commentString = comment,
                        commentDate = DateTime.Now.ToString("g")
                    });
                var jsonData = JsonConvert.SerializeObject(allComments);
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allComments));
                return GetComments();
            }
            return System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath)) ?? new List<Comment>();
        }

        private bool ValidateComment(string author, string comment)
        {
            try
            {
                // 0. If you dont type anything.
                if ((author == null || comment == null) || (author.Length <= 0 || comment.Length <= 0))
                {
                    throw new CustomException("Please fill both the Author and Your Comment field. If you wish to remain anonymous, you can write Anonymous.");
                }
                // 1. If character limit is breached manually
                if (author.Length > 25 || comment.Length > 500)
                {
                    // should never hit because of front end, but incase someone thinks they can break my website.
                    throw new CustomException("Character limit breached. Don't try to be smart and break my site in Developer Tools!");
                }
                // 2. If profanity detected
                var filter = new ProfanityFilter.ProfanityFilter();
                // 2.1. If author is profanity
                if (filter.ContainsProfanity(author))
                {
                    throw new CustomException("Author name contains profanity. Please refrain from posting this in the comments.");
                }
                //2.2. If comment contains profanity
                if (filter.ContainsProfanity(comment))
                {
                    throw new CustomException("Comment contains profanity. Please refrain from posting this in the comments.");
                }
            }
            catch (CustomException e)
            {
                throw new CustomException(e.Message);
            }
            return true;
        }

        private bool IsApiDown()
        {
            using (HttpClient c = new HttpClient())
            {
                if (c.GetAsync("https://jamesmoonitprofilecomments.azurewebsites.net/Ping").Result.Content.ReadAsStringAsync().Result == "Pong")
                {
                    return false;
                }
                return true;
            }
        }
    }
}
