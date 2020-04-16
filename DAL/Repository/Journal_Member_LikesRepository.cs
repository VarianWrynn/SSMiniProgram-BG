using DAL.Interface;
using Model.POCOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class Journal_Member_LikesRepository : BaseRepository<Journal_Member_Likes>, IJournal_Member_LikesRepository
    {
        public Journal_Member_LikesRepository(DBContext context) : base(context)
        {
        }
    }
}
