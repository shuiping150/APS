
namespace APSV1.Model
{
    /// <summary>
    /// 任务所需要的资源
    /// </summary>
    public class TreeRsType : DBObject
    {
        public string Tree_ID { get; set; } // 任务ID
        public string ResourceType_ID { get; set; } // 资源类别ID
        public int num { get; set; } // 需要资源数量
    }
}
