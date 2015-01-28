
using System.Collections.Generic;
namespace APSV1.Model
{
    public class RqMtrlObjectCollection : DBObjectCollection<RqMtrlObject>
    {
        private Dictionary<string, DBObjectCollection<RqMtrlObject>> ml_rqs = new Dictionary<string, DBObjectCollection<RqMtrlObject>>();

        private Dictionary<string, DBObjectCollection<RqMtrlObject>> mlmtrl_rqs = new Dictionary<string, DBObjectCollection<RqMtrlObject>>();

        public override void Add(RqMtrlObject item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
                if (!ml_rqs.ContainsKey(item.ML_ID))
                {
                    ml_rqs[item.ML_ID] = new DBObjectCollection<RqMtrlObject>();
                }
                ml_rqs[item.ML_ID].Add(item);
                var key = ToRqMtrl_ID(item.ML_ID, item.MtrlID, item.status, item.woodcode, item.pcode);
                if (!mlmtrl_rqs.ContainsKey(key))
                {
                    mlmtrl_rqs[key] = new DBObjectCollection<RqMtrlObject>();
                }
                mlmtrl_rqs[key].Add(item);
            }
        }

        public override bool Remove(RqMtrlObject item)
        {
            if (this.Contains(item))
            {
                if (ml_rqs.ContainsKey(item.ML_ID) && ml_rqs[item.ML_ID].Contains(item))
                {
                    ml_rqs[item.ML_ID].Remove(item);
                }
                var key = ToRqMtrl_ID(item.ML_ID, item.MtrlID, item.status, item.woodcode, item.pcode);
                if (mlmtrl_rqs.ContainsKey(key) && mlmtrl_rqs[key].Contains(item))
                {
                    mlmtrl_rqs[key].Remove(item);
                }
                return base.Remove(item);
            }
            else
            {
                return false;
            }
        }

        public override void Clear()
        {
            ml_rqs.Clear();
            mlmtrl_rqs.Clear();
            base.Clear();
        }

        public IList<RqMtrlObject> GetRqs(string ML_ID)
        {
            List<RqMtrlObject> rslt = new List<RqMtrlObject>();
            if (ml_rqs.ContainsKey(ML_ID))
            {
                foreach (var rq in ml_rqs[ML_ID])
                {
                    rslt.Add(rq);
                }
            }
            return rslt;
        }

        public string ToRqMtrl_ID(string ML_ID, int mtrlid, string status, string woodcode, string pcode)
        {
            return ML_ID + "rqmtrl:" + mtrlid + "," + status + "," + woodcode + "," + pcode;
        }

        public IList<RqMtrlObject> GetRqs(string ML_ID, int mtrlid, string status, string woodcode, string pcode)
        {
            var rslt = new List<RqMtrlObject>();
            var key = ToRqMtrl_ID(ML_ID, mtrlid, status, woodcode, pcode);
            if (mlmtrl_rqs.ContainsKey(key))
            {
                rslt.AddRange(mlmtrl_rqs[key]);
            }
            return rslt;
        }
    }
}
