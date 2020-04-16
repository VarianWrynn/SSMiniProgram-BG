using DAL.Interface;
using Model.POCOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class MemberInfoRepository : BaseRepository<MemberInfo>, IMemberInfoRepository
    {
        public MemberInfoRepository(DBContext context) : base(context)
        {
        }
    }
}
