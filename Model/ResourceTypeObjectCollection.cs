using System.Collections.Generic;

namespace APSV1.Model
{
    public class ResourceTypeObjectCollection : DBObjectCollection<ResourceTypeObject>
    {
        private Dictionary<string, DBObjectCollection<ResourceTypeObject>> _wrkgrp_eqtypes = new Dictionary<string, DBObjectCollection<ResourceTypeObject>>();

        public override void Add(ResourceTypeObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!string.IsNullOrEmpty(item.Eq_ID) && !string.IsNullOrEmpty(item.Wrkgrp_ID))
                {
                    if (!_wrkgrp_eqtypes.ContainsKey(item.Wrkgrp_ID))
                    {
                        _wrkgrp_eqtypes[item.Wrkgrp_ID] = new DBObjectCollection<ResourceTypeObject>();
                    }
                    _wrkgrp_eqtypes[item.Wrkgrp_ID].Add(item);
                }
            }
        }

        public override bool Remove(ResourceTypeObject item)
        {
            if (Contains(item))
            {
                if (!string.IsNullOrEmpty(item.Eq_ID) && !string.IsNullOrEmpty(item.Wrkgrp_ID))
                {
                    if (_wrkgrp_eqtypes.ContainsKey(item.Wrkgrp_ID))
                    {
                        _wrkgrp_eqtypes[item.Wrkgrp_ID].Remove(item);
                    }
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
            _wrkgrp_eqtypes.Clear();
            base.Clear();
        }

        public ResourceTypeObject GetEmpRs(WrkgrpObject wrkgrp)
        {
            string _ID = "类型:" + wrkgrp._ID + ",人力";
            ResourceTypeObject rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceTypeObject();
                rslt._ID = _ID;
                this.Add(rslt);
            }
            return rslt;
        }

        public ResourceTypeObject GetEmpRs(WorkgroupObject workgroup)
        {
            string _ID = "类型:" + workgroup._ID + ",人力";
            ResourceTypeObject rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceTypeObject();
                rslt._ID = _ID;
                this.Add(rslt);
            }
            return rslt;
        }

        public ResourceTypeObject GetEqRs(WrkgrpObject wrkgrp, IEnumerable<EquipmentObject> eqs)
        {
            string _ID = "类型:" + wrkgrp._ID;
            foreach (var eq in eqs)
            {
                _ID += "," + eq._ID;
            }

            ResourceTypeObject rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceTypeObject();
                rslt._ID = _ID;
                this.Add(rslt);
            }
            return rslt;
        }

        public ResourceTypeObject GetEqRs(WrkgrpObject wrkgrp, EquipmentObject eq)
        {
            string _ID = "类型:" + wrkgrp._ID + "," + eq._ID;

            ResourceTypeObject rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceTypeObject();
                rslt._ID = _ID;
                rslt.Wrkgrp_ID = wrkgrp._ID;
                rslt.Eq_ID = eq._ID;
                this.Add(rslt);
            }
            return rslt;
        }

        public List<ResourceTypeObject> GetWrkgrpEqs(WrkgrpObject wrkgrp)
        {
            var rslt = new List<ResourceTypeObject>();
            if (_wrkgrp_eqtypes.ContainsKey(wrkgrp._ID))
            {
                rslt.AddRange(_wrkgrp_eqtypes[wrkgrp._ID]);
            }
            return rslt;
        }

        public ResourceTypeObject GetEqRs(WrkgrpObject wrkgrp)
        {
            string _ID = "类型:" + wrkgrp._ID + ",设备";

            ResourceTypeObject rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceTypeObject();
                rslt._ID = _ID;
                this.Add(rslt);
            }
            return rslt;
        }
    }
}
