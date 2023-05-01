using JamesMoonPortfolioRedux.Models;

namespace JamesMoonPortfolioRedux.Data.IRepository
{
    public interface ICommentRepository : IProfileRepository<Comment>
    {
        void Update(Comment comment);
    }
}
