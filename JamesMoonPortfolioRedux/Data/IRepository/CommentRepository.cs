using JamesMoonPortfolioRedux.Models;
using JamesMoonPortfolioRedux.Data.IRepository;

namespace JamesMoonPortfolioRedux.Data.IRepository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private ProfileDbContext _db;
        public CommentRepository(ProfileDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Comment comment)
        {
            _db.Update(comment);
        }
    }
}
