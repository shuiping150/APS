using System;

namespace APSV1.Model
{
    public class RqMtrlTreeObject : DBObject
    {
        public string Parent_ID { get; set; }
        public string ML_ID { get; set; }
        public string Mtrl_ID { get; set; }

        public int scid { get; set; }
        public int OrderID { get; set; }
        public int MtrlID { get; set; }
        public string pfcode { get; set; }
        public int printid { get; set; }
        public int lp { get; set; }
        public double qty { get; set; }

        public DateTime? Begin { get; set; }
        public DateTime? ZZBegin { get; set; }
        public DateTime? End { get; set; }

        public double lastdays { get; set; } // 周期
        public double mindays { get; set; } // 经验生产周期
        public double readydays { get; set; } // 保险期


        public bool Enought { get; set; }

        public string status { get; set; }
        public string woodcode { get; set; }
        public string pcode { get; set; }

        public DateTime? UsableEnd { get; set; }
        public DateTime? UsableBegin { get; set; }

        public string linkname { get; set; }
        public string plinkname { get; set; }
    }
}
