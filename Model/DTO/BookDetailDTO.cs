using System.Collections.Generic;

namespace Model
{
    public class Images
    {
        /// <summary>
        /// 
        /// </summary>
        public string large { get; set; }
    }

    public class BookDetailDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> author { get; set; }

        /// <summary>
        /// 平装
        /// </summary>
        public string binding { get; set; }

        /// <summary>
        /// 算法
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int book_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Images images { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string isbn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pages { get; set; }

        /// <summary>
        /// 149.00元
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pubdate { get; set; }

        /// <summary>
        /// 人民邮电出版社
        /// </summary>
        public string publisher { get; set; }

        /// <summary>
        /// 全球开源社区集体智慧结晶，领略Linux内核的绝美风光
        /// </summary>
        public string subtitle { get; set; }

        /// <summary>
        /// 众所周知，Linux操作系统的源代码复杂、文档少，对程序员的要求高，要想看懂这些代码并不是一件容易事...
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 深入Linux内核架构
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> translator { get; set; }

    }
}