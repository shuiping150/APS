
namespace APSV1.Model
{
    public class WkpObject : DBObject
    {
        public string Sc_ID { get; set; }

        public int wrkGrpid { get; set; }
        public string wrkgrpcode { get; set; }
        public string wrkGrpName { get; set; }

        public plantype plantype { get; set; }
    }
}
