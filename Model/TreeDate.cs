using System;

namespace APSV1.Model
{
    /// <summary>
    /// 工艺时间各资源
    /// </summary>
    public class TreeDate : DBObject
    {
        /// <summary>
        /// 对应工艺树ID
        /// </summary>
        public string Tree_ID { get; set; }
        /// <summary>
        /// 对应资源日历
        /// </summary>
        public string ResourceDate_ID { get; set; }
        /// <summary>
        /// 资源组合
        /// </summary>
        public string ResourceGroup { get; set; }
        /// <summary>
        /// 资源ID
        /// </summary>
        public string Resource_ID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime begin { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end { get; set; }
        /// <summary>
        /// 占用时间
        /// </summary>
        public double workhour { get; set; }
    }
}
