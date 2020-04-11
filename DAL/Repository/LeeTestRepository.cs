using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interface;
using Model.POCOs;

namespace DAL.Repository
{
   public class LeeTestRepository: BaseRepository<LeeTest>, ILeeTestRepository
    {
        public LeeTestRepository(DBContext context):base(context)
        {

        }
    }
}
