using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interface;
using Model.POCOs;

namespace DAL.Repository
{
    public class Book_CommentsRepository : BaseRepository<book_comments>, IBook_CommentsRepository
    {
        public Book_CommentsRepository(DBContext context) : base(context)
        {
        }
    }
}
