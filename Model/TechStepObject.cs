
namespace APSV1.Model
{
    /// <summary>
    /// 工艺路线 主键(物料、序号) 树型结构 描述部件生产的主要步骤
    /// </summary>
    public sealed class TechStepObject : DBObject
    {
        /// <summary>
        /// 所属物料
        /// </summary>
        public string Mtrl_ID { get; set; }
        /// <summary>
        /// 后工艺路线
        /// </summary>
        public string ParentTechStep_ID { get; set; }
        /// <summary>
        /// 工艺名称
        /// </summary>
        public string partname { get; set; }
        /// <summary>
        /// 子部件编码
        /// </summary>
        public string relname { get; set; }
        /// <summary>
        /// 子部件名称
        /// </summary>
        public string relmtrlname { get; set; }

    }
}
