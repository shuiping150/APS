using System;

namespace APSV1.Model
{
    public class TreeObject : DBObject
    {
        public string ZL_ID { get; set; }
        public string Next_ID { get; set; }
        public string Wrkgrp_ID { get; set; }
        public string Workgroup_ID { get; set; }

        public double maxempnum { get; set; } // 最大分配人数

        public double workhour { get; set; } // 单件生产人时
        public double beforehour { get; set; } // 前准备工时
        public double afterhour { get; set; } // 后准备工时

        public double qty { get; set; } // 总数量

        public double lasthour { get; set; } // 交接周期
        public double waittime { get; set; } // 完工等待时间
        public lastmode lastmode { get; set; } // 交接模式

        public double eq_empnum { get; set; } // 设备分配人数

        public string eqnames { get; set; } // 使用设备
        public string mdnames { get; set; } // 使用模具

        public string eqids { get; set; }
        public string mdids { get; set; }

        public double eqnum { get; set; } // 设备可用数
        public double mdnum { get; set; } // 模具可用数

        public int swkpid { get; set; }
        public int owkpid { get; set; }

        public int printid { get; set; }
        public int parentid { get; set; }
        public int lp { get; set; }

        public string partname { get; set; }


        public DateTime? UsableEnd { get; set; }
        public DateTime? UsableBegin { get; set; }
        public int MaxGroupNum { get; set; }

        public string relname { get; set; } // 子部件编码
        public string relmtrlname { get; set; } // 子部件名称

        public DateTime? begindate { get; set; }
        public DateTime? enddate { get; set; }

        public beginmode beginMode { get; set; }
        public bool CanSplit { get; set; }

        public DateTime? firstBegin { get; set; }
        public DateTime? firstEnd { get; set; }
        public string DebugMsg { get; set; }
    }
}
