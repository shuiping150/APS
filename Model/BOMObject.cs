
namespace APSV1.Model
{
    /// <summary>
    /// 物料清单
    /// </summary>
    public sealed class BOMObject : DBObject
    {
        /// <summary>
        /// 上级物料
        /// </summary>
        public string mtrl_id { get; set; }
        /// <summary>
        /// 物料清单号
        /// </summary>
        public string pfcode { get; set; }
    }
}
