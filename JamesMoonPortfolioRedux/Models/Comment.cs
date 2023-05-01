using Amazon.DynamoDBv2.DataModel;

namespace JamesMoonPortfolioRedux.Models
{
    [DynamoDBTable("Comment")]
    public class Comment
    {
        [DynamoDBHashKey("CommentID")]
        public int CommentID { get; set; }

        [DynamoDBProperty("CommentAuthor")]
        public string CommentAuthor { get; set; } = String.Empty;

        [DynamoDBProperty("CommentString")]
        public string CommentString { get; set; } = String.Empty;

        [DynamoDBProperty("CommentDate")]
        public DateTime CommentDate { get; set; }
    }
}
