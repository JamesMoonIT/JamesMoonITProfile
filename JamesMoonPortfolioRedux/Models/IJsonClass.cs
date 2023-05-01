using JamesMoonPortfolioRedux.Models;

namespace JamesMoonPortfolioRedux.Models
{
    public interface IJsonClass
    {
        JsonClass ReadObject(string filePath);
    }
}