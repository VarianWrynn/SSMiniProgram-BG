using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interface;
using Model.POCOs;

namespace DAL.Repository
{
    public class Book_DetailRepository : BaseRepository<book_detail>, IBook_DetailRepository
    {
        public Book_DetailRepository(DBContext context) : base(context)
        {
        }
    }

}