using DAL.Interface;
using Model.POCOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class JournalRepository : BaseRepository<Journal>, IJournalRepository
    {
        public JournalRepository(DBContext context):base(context)
        {

        }
    }
}
 