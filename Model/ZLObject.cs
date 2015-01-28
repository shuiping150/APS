using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    public class ZLObject : DBObject
    {
        public string ML_ID { get; set; }
        public string Mtrl_ID { get; set; }
        public string Wkp_ID { get; set; }
        public List<string> Next_ID { get; set; }

        public int scid { get; set; }
        public int OrderID { get; set; }
        public string OrderCode { get; set; }
        public string relcode { get; set; }
        public int mtrlid { get; set; }
        public byte Status { get; set; }
        public int wrkGrpid { get; set; }

        public double orderqty { get; set; }
        public DateTime? Requiredate { get; set; }

        public string status_mode { get; set; }
        public string woodcode { get; set; }
        public string pcode { get; set; }

        public int taskmxid { get; set; }

        public string wpcode { get; set; }
        public string pfcode { get; set; }

        public DateTime? Begin { get; set; }
        public DateTime? ZZBegin { get; set; }
        public DateTime? End { get; set; }
    }
}
