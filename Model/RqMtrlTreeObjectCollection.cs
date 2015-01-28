using System.Collections.Generic;

namespace APSV1.Model
{
    public class RqMtrlTreeObjectCollection : DBObjectCollection<RqMtrlTreeObject>
    {
        private Dictionary<string, DBObjectCollection<RqMtrlTreeObject>> Rq_Children = new Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>();

        private Dictionary<string, DBObjectCollection<RqMtrlTreeObject>> ml_rqs = new Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>();

        private Dictionary<string, Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>> ml_mtrl_rq = new Dictionary<string, Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>>();

        private Dictionary<string, Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>> ml_group_rqs = new Dictionary<string, Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>>();

        public override void Add(RqMtrlTreeObject item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
                if (!Rq_Children.ContainsKey(item.Parent_ID))
                {
                    Rq_Children[item.Parent_ID] = new DBObjectCollection<RqMtrlTreeObject>();
                }
                Rq_Children[item.Parent_ID].Add(item);
                if (!ml_rqs.ContainsKey(item.ML_ID))
                {
                    ml_rqs[item.ML_ID] = new DBObjectCollection<RqMtrlTreeObject>();
                }
                ml_rqs[item.ML_ID].Add(item);
                if (!ml_mtrl_rq.ContainsKey(item.ML_ID))
                {
                    ml_mtrl_rq[item.ML_ID] = new Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>();
                }
                if (!ml_mtrl_rq[item.ML_ID].ContainsKey(item.Mtrl_ID))
                {
                    ml_mtrl_rq[item.ML_ID][item.Mtrl_ID] = new DBObjectCollection<RqMtrlTreeObject>();
                }
                ml_mtrl_rq[item.ML_ID][item.Mtrl_ID].Add(item);
                if (!ml_group_rqs.ContainsKey(item.ML_ID))
                {
                    ml_group_rqs[item.ML_ID] = new Dictionary<string, DBObjectCollection<RqMtrlTreeObject>>();
                }
                if (!ml_group_rqs[item.ML_ID].ContainsKey(item.linkname))
                {
                    ml_group_rqs[item.ML_ID][item.linkname] = new DBObjectCollection<RqMtrlTreeObject>();
                }
                ml_group_rqs[item.ML_ID][item.linkname].Add(item);
            }
        }

        public override bool Remove(RqMtrlTreeObject item)
        {
            if (this.Contains(item))
            {
                if (Rq_Children.ContainsKey(item.Parent_ID) && Rq_Children[item.Parent_ID].Contains(item))
                {
                    Rq_Children[item.Parent_ID].Remove(item);
                }
                if (ml_rqs.ContainsKey(item.ML_ID) && ml_rqs[item.ML_ID].Contains(item))
                {
                    ml_rqs[item.ML_ID].Remove(item);
                }
                if (ml_mtrl_rq.ContainsKey(item.ML_ID) && ml_mtrl_rq[item.ML_ID].ContainsKey(item.Mtrl_ID) && ml_mtrl_rq[item.ML_ID][item.Mtrl_ID].Contains(item))
                {
                    ml_mtrl_rq[item.ML_ID][item.Mtrl_ID].Remove(item);
                }
                if (ml_group_rqs.ContainsKey(item.ML_ID) && ml_group_rqs[item.ML_ID].ContainsKey(item.linkname))
                {
                    ml_group_rqs[item.ML_ID][item.linkname].Remove(item);
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
            Rq_Children.Clear();
            ml_rqs.Clear();
            ml_mtrl_rq.Clear();
            ml_group_rqs.Clear();
            base.Clear();
        }

        public List<RqMtrlTreeObject> GetRqs(string ML_ID, string Mtrl_ID)
        {
            List<RqMtrlTreeObject> rslt = new List<RqMtrlTreeObject>();
            if (ml_mtrl_rq.ContainsKey(ML_ID) && ml_mtrl_rq[ML_ID].ContainsKey(Mtrl_ID))
            {
                foreach (var rq in ml_mtrl_rq[ML_ID][Mtrl_ID])
                {
                    rslt.Add(rq);
                }
            }
            return rslt;
        }

        public List<RqMtrlTreeObject> GetRqs(string ML_ID)
        {
            List<RqMtrlTreeObject> rslt = new List<RqMtrlTreeObject>();
            if (ml_rqs.ContainsKey(ML_ID))
            {
                foreach (var rq in ml_rqs[ML_ID])
                {
                    rslt.Add(rq);
                }
            }
            return rslt;
        }

        public RqMtrlTreeObject GetParent(RqMtrlTreeObject rq)
        {
            return this.GetByID(rq.Parent_ID);
        }

        public List<RqMtrlTreeObject> GetChildren(RqMtrlTreeObject rq)
        {
            var rslt = new List<RqMtrlTreeObject>();
            if (Rq_Children.ContainsKey(rq._ID))
            {
                foreach (var c in Rq_Children[rq._ID])
                {
                    rslt.Add(c);
                }
            }
            return rslt;
        }

        public List<string> GetLinkName(MLObject ml)
        {
            List<string> rslt = new List<string>();
            if (ml_group_rqs.ContainsKey(ml._ID))
            {
                foreach (var kvp in ml_group_rqs[ml._ID])
                {
                    rslt.Add(kvp.Key);
                }
            }
            return rslt;
        }

        public IList<RqMtrlTreeObject> GetGroupRqs(MLObject ml, string linkname)
        {
            List<RqMtrlTreeObject> rslt = new List<RqMtrlTreeObject>();
            if (ml_group_rqs.ContainsKey(ml._ID) && ml_group_rqs[ml._ID].ContainsKey(linkname))
            {
                foreach (var rq in ml_group_rqs[ml._ID][linkname])
                {
                    rslt.Add(rq);
                }
            }
            return rslt;
        }
    }
}
