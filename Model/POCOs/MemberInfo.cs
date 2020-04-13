using System;
using System.Collections.Generic;
using System.Text;

namespace Model.POCOs
{
    public class MemberInfo
    {
        public string ChineseName { get; set; }
        public string CreatedBy { get; set; }
        public int Gender { get; set; }
        public string MemberName { get; set; }
        public string TitleId { get; set; }
   
        public string MemberStatus { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int ClubId { get; set; }
        public int MemberId { get; set; }
        public int MemberNo { get; set; }
    }
}
