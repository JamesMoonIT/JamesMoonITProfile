namespace JamesMoonPortfolioRedux.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentAuthor { get; set; } = String.Empty;

        public string CommentString { get; set; } = String.Empty;

        public string CommentDate { get; set; } = String.Empty;
    }
}
