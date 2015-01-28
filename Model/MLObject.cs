using System;

namespace APSV1.Model
{
    public class MLObject : DBObject
    {
        public string Mtrl_ID { get; set; }
        public string Cust_ID { get; set; }
        public string SaleTask_ID { get; set; }

        public int scid { get; set; }
        public int OrderID { get; set; }
        public string OrderCode { get; set; }

        public string relcode { get; set; }
        public int mtrlid { get; set; }
        public string status_mode { get; set; }
        public string woodcode { get; set; }
        public string pcode { get; set; }
        public double orderqty { get; set; }
        public string pfcode { get; set; }
        public DateTime? Requiredate { get; set; }

        public int level { get; set; }
        public int plan_flag { get; set; }

        public string saletaskcode { get; set; }
        public int cusid { get; set; }
        public int taskid { get; set; }
        public int taskscid { get; set; }

        public string cuscode { get; set; }
        public string cusname { get; set; }
        public string typename { get; set; }

        public double totalhours { get; set; }

        public DateTime? UsableEnd { get; set; }
        public DateTime? UsableBegin { get; set; }

        public string groupstr { get; set; } // 成组号
        public string assign_emp { get; set; } // 业务员
        public bool ifware { get; set; } // 是否考虑可用数
        public int batchtype { get; set; } // 来源
        public int printid { get; set; } // 序号
        public double saleqty { get; set; } // 订货数

        public DateTime? BeginDate { get; set; } // 开始时间
        public DateTime? EndDate { get; set; } // 结束时间

        public int OTLevel { get; set; } // 实际加班级别
    }
}
