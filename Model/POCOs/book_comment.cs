using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.POCOs
{
    /// <summary>
    /// 书籍快评
    /// </summary>
    public class book_comments
    {
        public string comment { get; set; }
        public int? book_id { get; set; }

        [Key]
        public int comment_id { get; set; }
    }
}
