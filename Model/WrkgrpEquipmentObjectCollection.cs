using System.Collections.Generic;

namespace APSV1.Model
{
    public class WrkgrpEquipmentObjectCollection : DBObjectCollection<WrkgrpEquipmentObject>
    {
        private Dictionary<string, DBObjectCollection<WrkgrpEquipmentObject>> _wrkgrp_eqs = new Dictionary<string, DBObjectCollection<WrkgrpEquipmentObject>>();

        public override void Add(WrkgrpEquipmentObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_wrkgrp_eqs.ContainsKey(item.Wrkgrp_ID))
                {
                    _wrkgrp_eqs[item.Wrkgrp_ID] = new DBObjectCollection<WrkgrpEquipmentObject>();
                }
                _wrkgrp_eqs[item.Wrkgrp_ID].Add(item);
            }
        }

        public override bool Remove(WrkgrpEquipmentObject item)
        {
            if (Contains(item))
            {
                if (_wrkgrp_eqs.ContainsKey(item.Wrkgrp_ID) && _wrkgrp_eqs[item.Wrkgrp_ID].Contains(item))
                {
                    _wrkgrp_eqs[item.Wrkgrp_ID].Remove(item);
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
            _wrkgrp_eqs.Clear();
            base.Clear();
        }

        public IList<WrkgrpEquipmentObject> GetEquipments(WrkgrpObject wrkgrp)
        {
            List<WrkgrpEquipmentObject> rslt = new List<WrkgrpEquipmentObject>();
            if (_wrkgrp_eqs.ContainsKey(wrkgrp._ID))
            {
                foreach (var eq in _wrkgrp_eqs[wrkgrp._ID])
                {
                    rslt.Add(eq);
                }
            }
            return rslt;
        }
    }
}
