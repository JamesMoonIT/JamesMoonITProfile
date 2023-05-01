using JamesMoonPortfolioRedux.Models;
using Microsoft.AspNetCore.Mvc;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig
            {
                ServiceURL = $"{Request.Scheme}://{Request.Host}"
            });

            var scanRequest = new ScanRequest
            {
                TableName = "Comment"
            };

            var response = client.ScanAsync(scanRequest);

            foreach (var item in response.Result.Items)
            {
                Console.WriteLine($"ID: {item["CommentID"].S}, Name: {item["CommentAuthor"].S}, CommentString: {item["CommentString"].S}, DateTime: {item["CommentDate"].S}");
            }

            return View();
        }
    }
}
