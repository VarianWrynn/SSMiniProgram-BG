using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using Model;
using Model.DTO;
using Model.POCOs;

namespace Services
{
    public interface IBookServices
    {
        BookDTO GetBook(int book_id = 0);
        bool Add(BookDTO model);

        bool Remove(BookDTO model);

        List<BookDTO> GetBookList();

        List<book_comments_DTO> GetComments(int book_id);
    }

    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRep;
        private readonly IBook_Member_Like_Repository _likeRep;
        private readonly IBook_CommentsRepository _bookComRep;
        private readonly IBook_DetailRepository _bookDetailRep;



        /// <summary>
        /// 在这里可以根据表的拓展，随时注入新的表的接口，比如IBook_Comments_Repository
        /// 2021-2-21 10:53:03
        /// </summary>
        /// <param name="book"></param>
        /// <param name="like"></param>
        //public BookServices(IBookRepository book, IBook_Member_Like_Repository like, 
        //    IBook_CommentsRepository bookCom, IBook_DetailRepository bookDet)
        //{
        //    _bookRep = book;
        //    _likeRep = like;
        //    _bookComRep = bookCom;
        //    _bookDetailRep = bookDet;
        //}

        //    public BookServices(IBaseRepository<Book> book, IBaseRepository<Journal_Member_Likes> like,
        //IBaseRepository<book_comments> bookCom, IBaseRepository<book_detail> bookDet)
        //    {
        //        _bookRep = (IBookRepository)book;
        //        _likeRep = (IBook_Member_Like_Repository)like;
        //        _bookComRep = (IBook_CommentsRepository)bookCom;
        //        _bookDetailRep = (IBook_DetailRepository)bookDet;
        //    }

        //在这里，Net Core会自动把你已注册的服务给注入进来，不需要你再实例化了。2021-5-3 22:55:37
        //这种方式被称为构造函数注入；
        public BookServices(IBookRepository book, IBook_Member_Like_Repository like,
            IBook_CommentsRepository bookCom, IBook_DetailRepository bookDet)
        {
            _bookRep = book;
            _likeRep = like;
            _bookComRep = bookCom;
            _bookDetailRep = bookDet;
        }

        public BookDTO GetBook(int book_id = 0)
        {
            var ret = _bookRep.List(r => r.book_id == 0 | r.book_id == book_id).FirstOrDefault();
            //var ret = _bookDetailRep.List(r=>r.book_id==book_id).FirstOrDefault();
            var book = new BookDTO();
            if (ret == null) return book;
            {
                book.book_id = ret.book_id;
                book.author = ret.author;
                book.image = ret.image;
                book.title = ret.title;
                book.Summary = _bookDetailRep.List(o => o.book_id == book_id).FirstOrDefault()?.summary;
                book.fav_nums = _likeRep.List(r => r.book_id == ret.book_id).Count();
                book.like_status = _likeRep.List(r => r.book_id == ret.book_id).Count();
            }

            return book;
        }

        public List<BookDTO> GetBookList()
        {
            var ret = _bookRep.List(r => r.book_id > 0).Select(r => new BookDTO
            {
                book_id = r.book_id,
                author = r.author,
                image = r.image,
                title = r.title,
                fav_nums = _likeRep.List(l => l.book_id == r.book_id).Count(),
                like_status = _likeRep.List(l => l.book_id == r.book_id).Count()
            });
            return ret.ToList();
        }


        /// <summary>
        /// 这里其实应该用Redist来实现，减少DB查询
        /// </summary>
        /// <returns></returns>
        public List<book_comments_DTO> GetComments(int book_id)
        {
            /*var retTemp = _bookRep.List(r => r.book_id == book_id)
                .FirstOrDefault();
            //return retTemp?.book_comments_list.Select(l => new book_comments_DTO // if (retTemp == null)
            if (retTemp?.book_comments_list == null)// if (retTemp == null || retTemp.book_comments_list == null)
            {
                return new List<book_comments_DTO>();
            }

            return retTemp.book_comments_list.Select(l => new book_comments_DTO
            {
                book_id = l.book_id.ConvertToNotNull(),
                comment = l.comment
            }).ToList();*/

            //MySqlException: Unknown column 'b.book_id1' in 'field list'
            //  var ret = _bookComRep.List(r => r.book_id == book_id);

            return _bookComRep
                .List(r => r.book_id == book_id)
                .Select(l => new book_comments_DTO
                {
                    book_id = l.book_id.ConvertToNotNull(),
                    comment_id = l.comment_id,
                    comment = l.comment,
                    agree_num = l.agree_num.ConvertToNotNull()
                }).ToList();
        }

        public bool Add(BookDTO model)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(BookDTO model)
        {
            throw new System.NotImplementedException();
        }
    }
}