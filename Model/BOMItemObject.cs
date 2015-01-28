
namespace APSV1.Model
{
    /// <summary>
    /// 物料清单明细
    /// </summary>
    public sealed class BOMItemObject : DBObject
    {
        /// <summary>
        /// 所属清单ID
        /// </summary>
        public string BOM_ID { get; set; }
        /// <summary>
        /// 子物料ID
        /// </summary>
        public string Sonmtrl_id { get; set; }
        /// <summary>
        /// 子物料清单
        /// </summary>
        public string sonpfcode { get; set; }
        /// <summary>
        /// 用量比例
        /// </summary>
        public double Sonscale { get; set; }
        /// <summary>
        /// 损耗率
        /// </summary>
        public double SonLoss { get; set; }
        /// <summary>
        /// 固定用量
        /// </summary>
        public double SonDECLosS { get; set; }

        // 用料量 = qty * Sonscale / (1 - SonLoss) + SonDECLosS
    }
}
