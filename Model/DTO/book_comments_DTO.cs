using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class book_comments_DTO
    {
        public string comment { get; set; }

        /// <summary>
        /// DTO成还有一个作用可能就是把那些数据库可null的字段，传递给前端之前做成不可Null的，比如这里为0
        /// 2021-2-19 20:54:44
        /// </summary>
        public int book_id { get; set; }

        public int comment_id { get; set; }
    }
}
