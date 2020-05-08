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
    }
}