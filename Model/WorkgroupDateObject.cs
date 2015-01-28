using System;

namespace APSV1.Model
{
    public class WorkgroupDateObject : DBObject
    {
        public string Workgroup_ID { get; set; }

        public DateTime WorkDate { get { return begintime.Date; } }
        //public int printid { get; set; }
        // 班次名称
        public string scname { get; set; }
        // 开始时间
        public DateTime begintime { get; set; }
        // 结束时间
        public DateTime endtime { get; set; }

        // 工人数
        //public double totalemp { get; set; }
        //public double totalemp_ori { get; set; }

        // 已用人时
        //public double usehour { get; set; }
        //public double usehour_ori { get; set; }

        // 默认排程比例
        //public double userate { get; set; }

        // 加班级别
        public int OTLevel { get; set; }

    }
}
