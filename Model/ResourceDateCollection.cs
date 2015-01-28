using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    // 资源日历
    public class ResourceDateCollection : DBObjectCollection<ResourceDate>
    {
        // 每个资源每天
        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<ResourceDate>>> _rs_dt_objs = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<ResourceDate>>>();

        private Dictionary<string, Dictionary<DateTime, ResourceDate>> _rs_dt_date = new Dictionary<string, Dictionary<DateTime, ResourceDate>>();

        private Dictionary<string, SortedSet<DateTime>> _rs_dts = new Dictionary<string, SortedSet<DateTime>>();

        public override void Add(ResourceDate item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_rs_dt_objs.ContainsKey(item.Resource_ID))
                {
                    _rs_dt_objs[item.Resource_ID] = new Dictionary<DateTime, DBObjectCollection<ResourceDate>>();
                }
                if (!_rs_dt_objs[item.Resource_ID].ContainsKey(item.begintime.Date))
                {
                    _rs_dt_objs[item.Resource_ID][item.begintime.Date] = new DBObjectCollection<ResourceDate>();
                }
                _rs_dt_objs[item.Resource_ID][item.begintime.Date].Add(item);
                if (!_rs_dt_date.ContainsKey(item.Resource_ID))
                {
                    _rs_dt_date[item.Resource_ID] = new Dictionary<DateTime, ResourceDate>();
                }
                _rs_dt_date[item.Resource_ID][item.begintime] = item;
                if (!_rs_dts.ContainsKey(item.Resource_ID))
                {
                    _rs_dts[item.Resource_ID] = new SortedSet<DateTime>();
                }
                _rs_dts[item.Resource_ID].Add(item.begintime);
            }
        }

        public override bool Remove(ResourceDate item)
        {
            if (Contains(item))
            {
                if (_rs_dt_objs.ContainsKey(item.Resource_ID) && _rs_dt_objs[item.Resource_ID].ContainsKey(item.begintime.Date) && _rs_dt_objs[item.Resource_ID][item.begintime.Date].Contains(item))
                {
                    _rs_dt_objs[item.Resource_ID][item.begintime.Date].Remove(item);
                }
                if (_rs_dt_date.ContainsKey(item.Resource_ID) && _rs_dt_date[item.Resource_ID].ContainsKey(item.begintime))
                {
                    _rs_dt_date[item.Resource_ID].Remove(item.begintime);
                }
                if (_rs_dts.ContainsKey(item.Resource_ID))
                {
                    _rs_dts[item.Resource_ID].Remove(item.begintime);
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
            _rs_dt_objs.Clear();
            _rs_dt_date.Clear();
            base.Clear();
        }

        // 返回资源当天班次
        public IList<ResourceDate> GetDates(ResourceObject resourceObj, DateTime dt)
        {
            List<ResourceDate> rslt = new List<ResourceDate>();
            if (_rs_dt_objs.ContainsKey(resourceObj._ID) && _rs_dt_objs[resourceObj._ID].ContainsKey(dt.Date))
            {
                foreach (var date in _rs_dt_objs[resourceObj._ID][dt.Date])
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
            }
            return rslt;
        }

        public List<ResourceDate> GetDates(ResourceObject resourceObj, DateTime begin, DateTime end)
        {
            List<ResourceDate> rslt = new List<ResourceDate>();

            if (_rs_dts.ContainsKey(resourceObj._ID))
            {
                var begins = _rs_dts[resourceObj._ID].GetViewBetween(begin, end);
                foreach (var dt in begins)
                {
                    if (_rs_dt_date.ContainsKey(resourceObj._ID) && _rs_dt_date[resourceObj._ID].ContainsKey(dt))
                    {
                        rslt.Add(_rs_dt_date[resourceObj._ID][dt]);
                    }
                }
            }

            return rslt;
        }

        public ResourceDate GetOneDate(ResourceObject rc, DateTime begin)
        {
            if (_rs_dt_date.ContainsKey(rc._ID) && _rs_dt_date[rc._ID].ContainsKey(begin))
            {
                return _rs_dt_date[rc._ID][begin];
            }
            return null;
        }

        public ResourceDate AddVirtualDate(ResourceObject rc, DateTime begin, DateTime end, double workhour, int OTLevel)
        {
            ResourceDate date = new ResourceDate();
            date._ID = ToID(rc, begin, end);
            date.Resource_ID = rc._ID;
            date.begintime = begin;
            date.endtime = end;
            date.workhour = workhour;
            date.usedhour = 0;
            date.OTLevel = Convert.ToByte(OTLevel);
            Add(date);
            return date;
        }

        public string ToID(ResourceObject rc, DateTime begin, DateTime end)
        {
            return ToID(rc._ID, begin, end);
        }

        public string ToID(string Resource_ID, DateTime begin, DateTime end)
        {
            return "RcDate:" + Resource_ID + "," + begin.ToString("yyyy-MM-dd HH:nn:ss") + "," + end.ToString("yyyy-MM-dd HH:nn:ss");
        }
    }
}
