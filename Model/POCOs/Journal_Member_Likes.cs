using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.POCOs
{
    public class Journal_Member_Likes
    {
        public DateTime? CreatedTime { get; set; }

        
        public int Id { get; set; }
        public int Jornal_Id { get; set; }
        public int Member_Id { get; set; }
    }
}
