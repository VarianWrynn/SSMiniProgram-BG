﻿using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using Model;
using Model.DTO;

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

        public BookServices(IBookRepository book, IBook_Member_Like_Repository like)
        {
            _bookRep = book;
            _likeRep = like;
        }

        public BookDTO GetBook(int book_id = 0)
        {
            var ret = _bookRep.List(r => r.book_id == 0 | r.book_id == book_id).FirstOrDefault();
            var book = new BookDTO();
            if (ret == null) return book;
            {
                book.book_id = ret.book_id;
                book.author = ret.author;
                book.image = ret.image;
                book.title = ret.title;
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
            var retTemp = _bookRep.List(r => r.book_id == book_id)
                .FirstOrDefault();
            return retTemp?.book_comments_list.Select(l => new book_comments_DTO // if (retTemp == null)
            {
                book_id = l.book_id.ConvertToNotNull(),
                comment = l.comment
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