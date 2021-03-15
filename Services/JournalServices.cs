using DAL.Interface;
using Model;
using Model.POCOs;
using System.Linq;

namespace Services
{
    public interface IJournalServices
    {
        JournalDTO getJournal(int index = 0);
        bool Add(Journal_Member_Likes model);

        bool Remove (Journal_Member_Likes model);
    }
    public class JournalServices : IJournalServices
    {
        private readonly IJournalRepository jPo;
        private readonly IJournal_Member_LikesRepository lPo;

        private readonly string _myUrl = $@"https://localhost:5001/";

        //public JournalServices(IJournalRepository r, IJournal_Member_LikesRepository l)
        // 如果是这种方式，则需要在Startup类上注入每一个接口和类
        public JournalServices(IBaseRepository<Journal> r, IBaseRepository<Journal_Member_Likes> l)
        {
            jPo = (IJournalRepository)r;
            lPo = (IJournal_Member_LikesRepository)l;
        }

        public bool Add(Journal_Member_Likes model)
        {
            lPo.Save(model);

            return true;
        }

        public JournalDTO getJournal(int index = 0)
        {
            //var result = repo.List(w => (id  == 0 ||w.id ==id)).OrderByDescending(r=>r.index).FirstOrDefault();//如果没有First()前端收到的JSON是数组形式[]
            var result = jPo.List(w => (index == 0 || w.index == index)).OrderByDescending(r => r.index).FirstOrDefault();
            var dto = new JournalDTO();
     
            if (result == null)
            {

                return dto;
            }
            var like = lPo.List(l => l.Jornal_Id == result.index).ToList();
            //var like = lPo.List(l => l.Jornal_Id>0).ToList();

            dto.id = result.id;
            dto.like_id = like.Where(r => r.Member_Id == 1).Select(r => r.Id).FirstOrDefault();//这个id实际上是点赞用的ID
            dto.image = result.image;
            dto.content = result.content;
            dto.index = result.index.ConvertToNotNull();
            dto.pubdate = result.pubdate.ConvertToString();
            dto.title = result.title;
            dto.type = result.type;
            dto.like_status = like.Count(r => r.Member_Id == 1);
            dto.fav_nums = like.Count();
            return dto;
        }

        public bool Remove(Journal_Member_Likes model)
        {
            /*因为 like 请求没有设置回调函数，无法更新like_id,所以这里只能用
             * member_id + journal_id来请求更新与新增 2020-4-19 18:36:34*/

            /*cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. 
             * When attaching existing entities, ensure that only one entity instance with a given key value is attached*/
            var item = lPo.FirstOrDefaultAsync(r => r.Jornal_Id == model.Jornal_Id && r.Member_Id == model.Member_Id).Result;

            /*lPo.Delete(model); /*cannot be tracked because another instance with the same key value for {'Id'} is already being tracked. 
             * When attaching existing entities, ensure that only one entity instance with a given key value is attached*/

            lPo.Delete(item);
            return true;
        }
    }
}
