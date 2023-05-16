using JamesMoonPortfolioRedux.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using ProfanityFilter;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly string filepath = @".\wwwroot\data\Comment.json";

        public IActionResult Index()
        {
            return View();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = System.Text.Json.JsonSerializer.Deserialize<List<Comment>>(System.IO.File.ReadAllText(filepath)) ?? new List<Comment>();
            return comments;
        }

        public List<Comment> StoreComment(string author, string comment)
        {
            List<Comment> allcomments = GetComments();
            if (author.Length > 25 || comment.Length > 500) 
            {
                // should never hit because of front end, but incase someone thinks they can break my website.
                throw new Exception("Character limit breached. Don't try to be smart and break my site in Developer Tools!");
            }
            bool valid = ValidateComment(author, comment);
            if (valid)
            {
                allcomments.Add(
                    new Comment
                    {
                        CommentID = allcomments.Max(c => c.CommentID) + 1,
                        CommentAuthor = author,
                        CommentString = comment,
                        CommentDate = DateTime.Now.ToString("g")
                    }
                );
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(allcomments));
            }
            return GetComments();
        }

        private bool ValidateComment(string author, string comment)
        {
            try
            {
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
            finally
            {

            }
            return true;
        }
    }
}
