using JamesMoonPortfolioRedux.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filepath = @".\wwwroot\data\Comment.json";

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath));
            return comments;
        }

        public List<Comment> StoreComment(string author, string comment)
        {
            List<Comment> allcomments = GetComments();
            if (author.Length > 25 || comment.Length > 500) 
            {
                throw new Exception("Character limit breached. Don't try breaking my website!");
            }
            int highest = 1;
            foreach (var com in allcomments)
            {
                if (com.CommentID > highest)
                {
                    highest = com.CommentID;
                }
            }
            allcomments.Add(
                new Comment
                {
                    CommentID = highest + 1,
                    CommentAuthor = author,
                    CommentString = comment,
                    CommentDate = DateTime.Now.ToString("g")
                }
            );
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allcomments));
            return GetComments();
        } 
    }
}
