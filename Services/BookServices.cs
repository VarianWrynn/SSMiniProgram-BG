using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using Model;

namespace Services
{
    public interface IBookServices
    {
        BookDTO getBook(int book_id = 0);
        bool Add(BookDTO model);

        bool Remove(BookDTO model);

        List<BookDTO> getBookList();
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

        public BookDTO getBook(int book_id = 0)
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
                book.like_status = likeRep.List(r => r.book_id == ret.book_id).Count();
            }

            return book;
        }

        public List<BookDTO> getBookList()
        {
            var ret = bookRep.List(r => r.book_id > 0).Select(r => new BookDTO
            {
                book_id = r.book_id,
                author = r.author,
                image = r.image,
                title = r.title,
                fav_nums = likeRep.List(l => l.book_id == r.book_id).Count(),
                like_status = likeRep.List(l => l.book_id == r.book_id).Count()
            });
            return ret.ToList();
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