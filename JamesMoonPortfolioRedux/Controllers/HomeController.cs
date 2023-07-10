using JamesMoonPortfolioRedux.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using ProfanityFilter;
using System.Text;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filepath = @".\wwwroot\data\Comment.json";
        private readonly string apipathGET = @"https://api.jsonbin.io/v3/b/64ab5553b89b1e2299bc7475?meta=false";
        private readonly string apipathPUT = @"https://api.jsonbin.io/v3/b/64ab5553b89b1e2299bc7475";


        public IActionResult Index()
        {
            return View();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Master-Key", "$2b$10$csAm2BVAu8I0tSKnlJjkOuOWnLbwlMKdqL6XY/c4GChhH0NUHs0bu");
                HttpResponseMessage response = client.GetAsync(apipathGET).Result;
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(jsonResponse) ?? new List<Comment>();
                }
                //else
                //{
                //    comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath)) ?? new List<Comment>();
                //}
            }
            return comments;
        }

        public List<Comment> StoreComment(string author, string comment)
        {
            List<Comment> allComments = GetComments();
            bool valid = ValidateComment(author, comment);          
            if (valid)
            {
                var newestComment = allComments.Max(x => x.CommentID) + 1;
                //Comment newComment = new Comment { CommentID = newestComment, CommentAuthor = author, CommentString = comment, CommentDate = DateTime.Now.ToString("g") };
                allComments.Add(
                    new Comment
                    {
                        CommentID = allComments.Max(c => c.CommentID) + 1,
                        CommentAuthor = author,
                        CommentString = comment,
                        CommentDate = DateTime.Now.ToString("g")
                    }
                );
                var jsonData = JsonConvert.SerializeObject(allComments);
                var serialized = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        client.DefaultRequestHeaders.Add("X-Master-Key", "$2b$10$csAm2BVAu8I0tSKnlJjkOuOWnLbwlMKdqL6XY/c4GChhH0NUHs0bu");
                        HttpResponseMessage response = client.PutAsync(apipathPUT, serialized).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            throw new Exception($"API failed to send data. Error: {response.StatusCode}");
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Failed to compile json data, or could not send data to storage. Error: {e.Message}");
                    }
                }
                //System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allcomments));
            }
            return GetComments();
        }

        private bool ValidateComment(string author, string comment)
        {
            try
            {
                // 0. If you dont type anything.
                if (author.Length <= 0 || comment.Length <= 0)
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
    }
}
