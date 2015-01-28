using System;

namespace APSV1.Model
{
    public class EquipmentDateObject : DBObject
    {
        public string Equipment_ID { get; set; }
        public Tasktype tasktype { get; set; }
        public string WrkgrpDate_ID { get; set; }
        public string WorkgroupDate_ID { get; set; }

        public double equipmentnum { get; set; } // 设备数
        public double equipmentnum_ori { get; set; }

        public double assignhour { get; set; } // 已用设备时
        public double assignhour_ori { get; set; }
    }
}
