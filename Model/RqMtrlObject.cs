
namespace APSV1.Model
{
    public class RqMtrlObject : DBObject
    {
        public string ML_ID { get; set; }
        public string Mtrl_ID { get; set; }

        public int scid { get; set; }
        public int OrderID { get; set; }
        public int MtrlID { get; set; }

        public string status { get; set; }
        public string woodcode { get; set; }
        public string pcode { get; set; }

        public double qty { get; set; }

        public int mxpkid { get; set; }
        public string pfcode { get; set; }

        public int wrkGrpid { get; set; }
        public byte plantype { get; set; }
        public int produce_wrkGrpid { get; set; }
    }
}
