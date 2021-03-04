namespace Model
{
    public class BookDTO
    {
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int fav_nums { get; set; }

        /// <summary>
        /// 书籍id
        /// </summary>
        public int book_id { get; set; }

        /// <summary>
        /// 书籍图片
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 是否点赞
        /// </summary>
        public int like_status { get; set; }

        /// <summary>
        /// 书籍题目
        /// </summary>
        public string title { get; set; }

        public BookDetailDTO bookDetail { get; set; }

    }
}