namespace JamesMoonPortfolioRedux.Models
{
    public class Comment
    {
        public int commentID { get; set; }

        public string commentAuthor { get; set; } = String.Empty;

        public string commentString { get; set; } = String.Empty;

        public string commentDate { get; set; } = String.Empty;
    }
}
