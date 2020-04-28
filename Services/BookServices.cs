using System.Linq;
using DAL.Interface;
using Model;

namespace Services
{
    public interface IBookServices
    {
        BookDTO getBooks(int book_id = 0);
        bool Add(BookDTO model);

        bool Remove(BookDTO model);
    }

    public class BookServices : IBookServices
    {
        private readonly IBookRepository bookRep;
        private readonly IBook_Member_Like_Repository likeRep;

        public BookServices(IBookRepository book, IBook_Member_Like_Repository like)
        {
            bookRep = book;
            likeRep = like;
        }

        public BookDTO getBooks(int book_id = 0)
        {
            var ret = bookRep.List(r => r.book_id == 0 | r.book_id == book_id).FirstOrDefault();
            var book = new BookDTO();
            if (ret != null)
            {
                book.book_id = ret.book_id;
                book.author = ret.author;
                book.image = ret.image;
                book.title = ret.title;
                book.fav_nums = likeRep.List(r => r.book_id == ret.book_id).Count();
            }

            return book;
        }

        public bool Add(BookDTO model)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(BookDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}