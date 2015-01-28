
namespace APSV1.Model
{
    public class MtrlObject : DBObject
    {
        public int mtrlid { get; set; }
        public string mtrlcode { get; set; }
        public string mtrlname { get; set; }
        public string mtrlmode { get; set; }
        public string unit { get; set; }
        public string dscrp { get; set; }
        public string mtrlsectype { get; set; }
        public string zxmtrlmode { get; set; }
        public string usermtrlmode { get; set; }
        public int ifselforder { get; set; } // 0无、1独立排主计划、2车间生产指令、3包件进仓指令
        public int Mtrlorigin { get; set; } // 0自制、2采购、3外协、6客户来料

        public double buydays { get; set; } // 采购周期
        public double buydays_bx { get; set; } // 采购保险期

        public double wfjgdays { get; set; } // 外协周期
        public double wfjgdays_bx { get; set; } // 外协保险期

        public double aheaddays { get; set; } // 来料周期

        public double expday { get; set; } // 经验生产周期
        public double maxzjday { get; set; } // 生产周期上限
        public double orderdays { get; set; } // 生产保险期

        public int dftwrkGrpid { get; set; } // 默认生产车间

        public int ifpackpro { get; set; }
        public int ordertype { get; set; }
        public int ifpack { get; set; }
    }
}
