
namespace APSV1.Model
{
    public class WorkgroupObject : DBObject
    {
        public string Wrkgrp_ID { get; set; } // 工组
        public int workgroupid { get; set; }
        public string workgroupcode { get; set; }
        public string workgroupname { get; set; } // 名称
        public double empnum { get; set; } // 人数
    }
}
