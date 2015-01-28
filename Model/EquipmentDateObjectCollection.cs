using System.Collections.Generic;

 namespace APSV1.Model
{
    public class EquipmentDateObjectCollection : DBObjectCollection<EquipmentDateObject>
    {
        private Dictionary<string, DBObjectCollection<EquipmentDateObject>> _wrkgrp_eq_dates = new Dictionary<string, DBObjectCollection<EquipmentDateObject>>();

        private Dictionary<string, DBObjectCollection<EquipmentDateObject>> _workgroup_eq_dates = new Dictionary<string, DBObjectCollection<EquipmentDateObject>>();

        public override void Add(EquipmentDateObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (item.tasktype == Tasktype.Wrkgrp)
                {
                    if (!_wrkgrp_eq_dates.ContainsKey(item.WrkgrpDate_ID))
                    {
                        _wrkgrp_eq_dates[item.WrkgrpDate_ID] = new DBObjectCollection<EquipmentDateObject>();
                    }
                    _wrkgrp_eq_dates[item.WrkgrpDate_ID].Add(item);
                }
                else if (item.tasktype == Tasktype.Workgroup)
                {
                    if (!_workgroup_eq_dates.ContainsKey(item.WorkgroupDate_ID))
                    {
                        _workgroup_eq_dates[item.WorkgroupDate_ID] = new DBObjectCollection<EquipmentDateObject>();
                    }
                    _workgroup_eq_dates[item.WorkgroupDate_ID].Add(item);
                }
                else if (item.tasktype == Tasktype.Emp)
                {

                }
            }
        }

        public override bool Remove(EquipmentDateObject item)
        {
            if (Contains(item))
            {
                if (item.tasktype == Tasktype.Wrkgrp)
                {
                    if (_wrkgrp_eq_dates.ContainsKey(item.WrkgrpDate_ID) && _wrkgrp_eq_dates[item.WrkgrpDate_ID].Contains(item))
                    {
                        _wrkgrp_eq_dates[item.WrkgrpDate_ID].Remove(item);
                    }
                }
                else if (item.tasktype == Tasktype.Workgroup)
                {
                    if (_workgroup_eq_dates.ContainsKey(item.WorkgroupDate_ID) && _workgroup_eq_dates[item.WorkgroupDate_ID].Contains(item))
                    {
                        _workgroup_eq_dates[item.WorkgroupDate_ID].Remove(item);
                    }
                }
                else if (item.tasktype == Tasktype.Emp)
                {
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
            _wrkgrp_eq_dates.Clear();
            _workgroup_eq_dates.Clear();
            base.Clear();
        }

        public DBObjectCollection<EquipmentDateObject> GetEquipmentDates(WrkgrpDateObject wrkgrpDate)
        {
            if (_wrkgrp_eq_dates.ContainsKey(wrkgrpDate._ID))
            {
                return _wrkgrp_eq_dates[wrkgrpDate._ID];
            }
            else
            {
                return new DBObjectCollection<EquipmentDateObject>();
            }
        }

        public DBObjectCollection<EquipmentDateObject> GetEquipmentDates(WorkgroupDateObject workgroupDate)
        {
            if (_workgroup_eq_dates.ContainsKey(workgroupDate._ID))
            {
                return _workgroup_eq_dates[workgroupDate._ID];
            }
            else
            {
                return new DBObjectCollection<EquipmentDateObject>();
            }
        }

    }
}
