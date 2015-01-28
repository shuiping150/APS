 using System;

namespace APSV1.Model
{
    /// <summary>
    /// 资源日历
    /// </summary>
    public class ResourceDate : DBObject
    {
        public string Resource_ID { get; set; }
        public DateTime begintime { get; set; }
        public DateTime endtime { get; set; }
        public double workhour { get; set; }
        public double usedhour { get; set; }
        public byte OTLevel { get; set; }
    }
}
