using System.Collections.Generic;

namespace APSV1.Model
{
    public class WrkgrpObjectCollection : DBObjectCollection<WrkgrpObject>
    {
        private Dictionary<string, DBObjectCollection<WrkgrpObject>> _wkp_wrkgrps = new Dictionary<string, DBObjectCollection<WrkgrpObject>>();

        public override void Add(WrkgrpObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_wkp_wrkgrps.ContainsKey(item.Wkp_ID))
                {
                    _wkp_wrkgrps[item.Wkp_ID] = new DBObjectCollection<WrkgrpObject>();
                }
                _wkp_wrkgrps[item.Wkp_ID].Add(item);
            }
        }

        public override bool Remove(WrkgrpObject item)
        {
            if (Contains(item))
            {
                if (_wkp_wrkgrps.ContainsKey(item.Wkp_ID) && _wkp_wrkgrps[item.Wkp_ID].Contains(item))
                {
                    _wkp_wrkgrps[item.Wkp_ID].Remove(item);
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
            _wkp_wrkgrps.Clear();
            base.Clear();
        }

        public string ToID(int wrkgrpid)
        {
            return "wrkgrp:" + wrkgrpid;
        }

        public List<WrkgrpObject> GetChildWrkgrps(WkpObject wkp)
        {
            List<WrkgrpObject> rslt = new List<WrkgrpObject>();
            if (_wkp_wrkgrps.ContainsKey(wkp._ID))
            {
                rslt.AddRange(_wkp_wrkgrps[wkp._ID]);
            }
            return rslt;
        }

        public WrkgrpObject GetParent(WorkgroupObject workgroup)
        {
            return GetByID(workgroup.Wrkgrp_ID);
        }

        public WrkgrpObject GetWrkgrp(WrkgrpDateObject wrkgrpDate)
        {
            return GetByID(wrkgrpDate.Wrkgrp_ID);
        }

        public WrkgrpObject GetWrkgrp(TreeObject tree)
        {
            return GetByID(tree.Wrkgrp_ID);
        }
    }
}
