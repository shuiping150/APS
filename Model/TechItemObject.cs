
namespace APSV1.Model
{
    /// <summary>
    /// 工艺路线步骤可选方案 与机器抛光与人工抛光
    /// </summary>
    public sealed class TechItemObject : DBObject
    {
        /// <summary>
        /// 所属工艺步骤
        /// </summary>
        public string TechStep_ID { get; set; }
        /// <summary>
        /// 单件工时
        /// </summary>
        public double workhour { get; set; }
        /// <summary>
        /// 前准备工时
        /// </summary>
        public double beforehour { get; set; }
        /// <summary>
        /// 后收尾工时
        /// </summary>
        public double afterhour { get; set; }
        /// <summary>
        /// 交接周期
        /// </summary>
        public double lasthour { get; set; }
        /// <summary>
        /// 完工等待时间
        /// </summary>
        public double waittime { get; set; }
        /// <summary>
        /// 交接模式
        /// </summary>
        public lastmode lastmode { get; set; }

    }
}
