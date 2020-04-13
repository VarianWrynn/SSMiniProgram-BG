using System;
using System.Collections.Generic;
using System.Text;

namespace Model.POCOs
{
    public class Journal
    {

        public int id { get; set; }
        public int index { get; set; }
        public int type { get; set; }

        public string content { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public DateTime? pubdate { get; set; }
    }
}
