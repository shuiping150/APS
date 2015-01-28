using System.Collections.Generic;

namespace APSV1.Model
{
    public class WkpObjectCollection : DBObjectCollection<WkpObject>
    {
        private Dictionary<string, DBObjectCollection<WkpObject>> _sc_wkps = new Dictionary<string, DBObjectCollection<WkpObject>>();

        public override void Add(WkpObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_sc_wkps.ContainsKey(item.Sc_ID))
                {
                    _sc_wkps[item.Sc_ID] = new DBObjectCollection<WkpObject>();
                }
                _sc_wkps[item.Sc_ID].Add(item);
            }
        }

        public override bool Remove(WkpObject item)
        {
            if (Contains(item))
            {
                if (_sc_wkps.ContainsKey(item.Sc_ID) && _sc_wkps[item.Sc_ID].Contains(item))
                {
                    _sc_wkps[item.Sc_ID].Remove(item);
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
            _sc_wkps.Clear();
            base.Clear();
        }

        public List<WkpObject> GetChildWkps(ScObject sc)
        {
            List<WkpObject> rslt = new List<WkpObject>();
            if (_sc_wkps.ContainsKey(sc._ID))
            {
                rslt.AddRange(_sc_wkps[sc._ID]);
            }
            return rslt;
        }

        public WkpObject GetParent(WrkgrpObject wrkgrp)
        {
            return GetByID(wrkgrp.Wkp_ID);
        }

        public string ToWkp_ID(int wkpid)
        {
            return "wkp:" + wkpid;
        }

    }
}
