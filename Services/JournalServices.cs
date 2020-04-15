using DAL.Interface;
using Model;
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


        public JournalDTO getJournal(int index = 0)
        {
            //var result = repo.List(w => (id  == 0 ||w.id ==id)).OrderByDescending(r=>r.index).FirstOrDefault();//如果没有First()前端收到的JSON是数组形式[]
            var result = repo.List(w => (index == 0 || w.index == index)).OrderByDescending(r => r.index).FirstOrDefault();
            var dto = new JournalDTO();

            if (result ==null)
            {

                return dto;
            }
            //dto.id = result.id;
            dto.image = result.image;
            dto.content = result.content;
            dto.index = result.index.ConvertToNotNull();
            dto.pubdate = result.pubdate.ConvertToString();
            dto.title = result.title;
            dto.type = result.type;
            dto.like_status = 1;
            //dto.fav_nums = 666;
            return dto;
        }
    }
}
