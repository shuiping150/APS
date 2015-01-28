using System.Collections.Generic;

namespace APSV1.Model
{
    public class WorkgroupEquipmentObjectCollection : DBObjectCollection<WorkgroupEquipmentObject>
    {
        private Dictionary<string, DBObjectCollection<WorkgroupEquipmentObject>> _workgroup_eqs = new Dictionary<string, DBObjectCollection<WorkgroupEquipmentObject>>();

        public override void Add(WorkgroupEquipmentObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_workgroup_eqs.ContainsKey(item.Workgroup_ID))
                {
                    _workgroup_eqs[item.Workgroup_ID] = new DBObjectCollection<WorkgroupEquipmentObject>();
                }
                _workgroup_eqs[item.Workgroup_ID].Add(item);
            }
        }

        public override bool Remove(WorkgroupEquipmentObject item)
        {
            if (Contains(item))
            {
                if (_workgroup_eqs.ContainsKey(item.Workgroup_ID) && _workgroup_eqs[item.Workgroup_ID].Contains(item))
                {
                    _workgroup_eqs[item.Workgroup_ID].Remove(item);
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
            _workgroup_eqs.Clear();
            base.Clear();
        }

        public IList<WorkgroupEquipmentObject> GetEquipments(WorkgroupObject workgroup)
        {
            List<WorkgroupEquipmentObject> rslt = new List<WorkgroupEquipmentObject>();
            if (_workgroup_eqs.ContainsKey(workgroup._ID))
            {
                foreach (var eq in _workgroup_eqs[workgroup._ID])
                {
                    rslt.Add(eq);
                }
            }
            return rslt;
        }
    }
}
