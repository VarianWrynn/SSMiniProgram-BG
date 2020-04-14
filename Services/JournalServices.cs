using DAL.Interface;
using Model.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class JournalServices
    {
        private readonly IJournalRepository repo;
        public JournalServices(IJournalRepository r)
        {
            repo = r;
        }


        public Journal getJournal(int id = 0)
        {
            //var result = repo.List(w => (id  == 0 ||w.id ==id)).FirstOrDefault();//如果没有First()前端收到的JSON是数组形式[]
            var result = repo.List(w => (0 == 0)).FirstOrDefault();
            return result;
        }
    }
}
