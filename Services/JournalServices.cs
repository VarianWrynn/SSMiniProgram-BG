using DAL.Interface;
using Model;
using Model.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public interface IJournalServices
    {
        JournalDTO getJournal(int index = 0);
        void UpdateLikeStatus(int like_id,  bool isCancled);
    }
    public class JournalServices: IJournalServices
    {
        private readonly IJournalRepository jPo;
        private readonly IJournal_Member_LikesRepository lPo;
        public JournalServices(IJournalRepository r, IJournal_Member_LikesRepository l)
        {
            jPo = r;
            lPo = l;
        }


        public JournalDTO getJournal(int index = 0)
        {
            //var result = repo.List(w => (id  == 0 ||w.id ==id)).OrderByDescending(r=>r.index).FirstOrDefault();//如果没有First()前端收到的JSON是数组形式[]
            var result = jPo.List(w => (index == 0 || w.index == index)).OrderByDescending(r => r.index).FirstOrDefault();
            var dto = new JournalDTO();
            if (result ==null)
            {

                return dto;
            }
            var like = lPo.List(l => l.Jornal_Id == result.id).ToList();
            //var like = lPo.List(l => l.Jornal_Id>0).ToList();
            dto.id = result.id;
            dto.like_id = like.Where(r=>r.Member_Id == 1).Select(r=>r.Id).FirstOrDefault();//这个id实际上是点赞用的ID
            dto.image = result.image;
            dto.content = result.content;
            dto.index = result.index.ConvertToNotNull();
            dto.pubdate = result.pubdate.ConvertToString();
            dto.title = result.title;
            dto.type = result.type;
            dto.like_status = 1;
            dto.fav_nums = like.Count();
            return dto;
        }

        public void UpdateLikeStatus(int like_id, bool isCancled)
        {
            var lModle = lPo.List(l => l.Id == like_id).FirstOrDefault();

            if (isCancled)//取消点赞
            {
                lPo.Delete(lModle);
            }
            else
            {
                lPo.Save(lModle);
            }
        }
    }
}
