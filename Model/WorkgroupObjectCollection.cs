using System.Collections.Generic;

namespace APSV1.Model
{
    public class WorkgroupObjectCollection : DBObjectCollection<WorkgroupObject>
    {
        private Dictionary<string, DBObjectCollection<WorkgroupObject>> _wrkgrp_workgroups = new Dictionary<string, DBObjectCollection<WorkgroupObject>>();

        public override void  Add(WorkgroupObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_wrkgrp_workgroups.ContainsKey(item.Wrkgrp_ID))
                {
                    _wrkgrp_workgroups[item.Wrkgrp_ID] = new DBObjectCollection<WorkgroupObject>();
                }
                _wrkgrp_workgroups[item.Wrkgrp_ID].Add(item);
            }
        }

        public override bool Remove(WorkgroupObject item)
        {
            if (Contains(item))
            {
                if (_wrkgrp_workgroups.ContainsKey(item.Wrkgrp_ID) && _wrkgrp_workgroups[item.Wrkgrp_ID].Contains(item))
                {
                    _wrkgrp_workgroups[item.Wrkgrp_ID].Remove(item);
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
            _wrkgrp_workgroups.Clear();
            base.Clear();
        }

        public List<WorkgroupObject> GetChildWorkgroups(WrkgrpObject wrkgrp)
        {
            List<WorkgroupObject> rslt = new List<WorkgroupObject>();
            if (_wrkgrp_workgroups.ContainsKey(wrkgrp._ID))
            {
                rslt.AddRange(_wrkgrp_workgroups[wrkgrp._ID]);
            }
            return rslt;
        }

        public WorkgroupObject GetWorkgroup(WorkgroupDateObject workgroupDate)
        {
            return GetByID(workgroupDate.Workgroup_ID);
        }

        internal string ToID(int workgroupid)
        {
            return "workgroup:" + workgroupid;
        }
    }
}
