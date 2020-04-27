using DAL.Interface;
using Model.POCOs;

namespace DAL.Repository
{
    public class BookRepository: BaseRepository<Book>, IBookRepository
    {
        public BookRepository(DBContext context) : base(context)
        {
        }
    }
}