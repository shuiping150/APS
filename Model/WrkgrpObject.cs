
namespace APSV1.Model
{
    public class WrkgrpObject : DBObject
    {
        public string Wkp_ID { get; set; }

        public int wrkGrpid { get; set; }
        public string wrkgrpcode { get; set; }
        public string wrkGrpName { get; set; }

        public Tasktype tasktype { get; set; }

        public double shortrate { get; set; }

        public int empnum { get; set; }
    }
}
