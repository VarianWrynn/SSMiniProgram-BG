using DAL.Interface;
using Model;

namespace Services
{
    public interface IBookServices
    {
        BookDTO getBooks(int index = 0);
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

        public BookDTO getBooks(int index = 0)
        {
            throw new System.NotImplementedException();
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