
namespace APSV1.Model
{
    /// <summary>
    /// 资源-类别关系表
    /// </summary>
    public class ResourceMap : DBObject
    {
        public string ResourceType_ID { get; set; } // 类别ID
        public string Resource_ID { get; set; } // 资源ID
    }
}
