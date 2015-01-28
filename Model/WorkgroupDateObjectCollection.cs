using System.Collections.Generic;
using System;

namespace APSV1.Model
{
    public class WorkgroupDateObjectCollection : DBObjectCollection<WorkgroupDateObject>
    {
        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<WorkgroupDateObject>>> _workgroup_dates = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<WorkgroupDateObject>>>();

        public override void Add(WorkgroupDateObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_workgroup_dates.ContainsKey(item.Workgroup_ID))
                {
                    _workgroup_dates[item.Workgroup_ID] = new Dictionary<DateTime, DBObjectCollection<WorkgroupDateObject>>();
                }
                if (!_workgroup_dates[item.Workgroup_ID].ContainsKey(item.WorkDate.Date))
                {
                    _workgroup_dates[item.Workgroup_ID][item.WorkDate.Date] = new DBObjectCollection<WorkgroupDateObject>();
                }
                _workgroup_dates[item.Workgroup_ID][item.WorkDate.Date].Add(item);
            }
        }

        public override bool Remove(WorkgroupDateObject item)
        {
            if (Contains(item))
            {
                if (_workgroup_dates.ContainsKey(item.Workgroup_ID) && _workgroup_dates[item.Workgroup_ID].ContainsKey(item.WorkDate.Date) && _workgroup_dates[item.Workgroup_ID][item.WorkDate.Date].Contains(item))
                {
                    _workgroup_dates[item.Workgroup_ID][item.WorkDate.Date].Remove(item);
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
            _workgroup_dates.Clear();
            base.Clear();
        }

        public List<WorkgroupDateObject> GetDates(WorkgroupObject workgroup, DateTime dt)
        {
            if (_workgroup_dates.ContainsKey(workgroup._ID) && _workgroup_dates[workgroup._ID].ContainsKey(dt.Date))
            {
                List<WorkgroupDateObject> rslt = new List<WorkgroupDateObject>();
                foreach (var date in _workgroup_dates[workgroup._ID][dt.Date])
                {
                    int i = 0;
                    for (i = 0; i < rslt.Count; i++)
                    {
                        if (date.begintime < rslt[i].begintime)
                        {
                            break;
                        }
                    }
                    rslt.Insert(i, date);
                }
                return rslt;
            }
            else
            {
                return new List<WorkgroupDateObject>();
            }
        }
    }
}
