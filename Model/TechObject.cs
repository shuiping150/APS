using System;

namespace APSV1.Model
{
    public class TechObject : DBObject
    {
        public string Tree_ID { get; set; }
        public DateTime? begindate { get; set; }
        public DateTime? enddate { get; set; }

        public double qty { get; set; }
        public double workhour { get; set; }


        #region 准备无效属性
        public string Wrkgrp_ID { get; set; }
        public string Workgroup_ID { get; set; }

        public string WrkgrpDate_ID { get; set; }
        public string WorkgroupDate_ID { get; set; }


        public double fqty { get; set; }

        // 
        public int billid { get; set; }
        public string billcode { get; set; }
        #endregion

    }
}
