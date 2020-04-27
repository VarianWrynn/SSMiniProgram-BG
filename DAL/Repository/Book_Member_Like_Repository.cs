using DAL.Interface;
using Model.POCOs;

namespace DAL.Repository
{
    public class Book_Member_Like_Repository: BaseRepository<book_member_like>,IBook_Member_Like_Repository

    {
        public Book_Member_Like_Repository(DBContext context) : base(context)
        {
        }
    }
}