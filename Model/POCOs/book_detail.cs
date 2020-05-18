using System;
using System.ComponentModel.DataAnnotations;

namespace Model.POCOs
{
    public class book_detail
    {

        [Key]
        public int detail_id { get; set; }

        public string author { get; set; }
        public string binding { get; set; }
        public string category { get; set; }
        public string image_large { get; set; }
        public string isbn { get; set; }
        public string publisher { get; set; }
        public string subtitle { get; set; }
        public string summary { get; set; }
        public string translator { get; set; }
        public DateTime? pubdate { get; set; }


        public int? pages { get; set; }
        public double? price { get; set; }

        public Book book { get; set; }
    }
}