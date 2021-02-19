using System.Collections.Generic;

namespace Model.POCOs
{
    public class Book
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }


        /// <summary>
        /// 书籍id
        /// </summary>
        public int book_id { get; set; }


        /// <summary>
        /// 外键
        /// </summary>
        public int detail_id { get; set; }

        /// <summary>
        /// 书籍图片
        /// </summary>
        public string image { get; set; }


        /// <summary>
        /// 书籍题目
        /// </summary>
        public string title { get; set; }

        public book_detail book_detail { get; set; }

        /// <summary>
        /// 这样子，在查找短评的时候，就不需要再单独写一个Ibook_comments_repository类在Service成去做查询了
        ///
        /// 2021-2-19 21:05:30
        /// </summary>
        public List<book_comments> book_comments_list { get; set; }
    }
}