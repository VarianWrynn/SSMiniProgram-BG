using System;
using System.Collections.Generic;
using System.Text;

namespace Model.POCOs
{
    public class Journal
    {

        public int id { get; set; }

        /// <summary>
        /// 如果数据库字段是可空，实体类一定也要设置为可空类型，否则当数据库的字段是null的时候，会报错：
        /// InvalidCastException: Unable to cast object of type 'System.DBNull' to type 'System.Int32'.
        /// 2020-4-14 22:15:01
        /// </summary>
        public int? index { get; set; }
        public int type { get; set; }

        public string content { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public DateTime? pubdate { get; set; }
    }
}
