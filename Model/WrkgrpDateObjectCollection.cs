using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    public class WrkgrpDateObjectCollection : DBObjectCollection<WrkgrpDateObject>
    {
        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<WrkgrpDateObject>>> _wrkgrp_dates = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<WrkgrpDateObject>>>();

        private Dictionary<string, SortedSet<DateTime>> _wrkgrp_begindates = new Dictionary<string, SortedSet<DateTime>>();

        private Dictionary<string, SortedSet<DateTime>> _wrkgrp_begins = new Dictionary<string, SortedSet<DateTime>>();

        public override void Add(WrkgrpDateObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_wrkgrp_dates.ContainsKey(item.Wrkgrp_ID))
                {
                    _wrkgrp_dates[item.Wrkgrp_ID] = new Dictionary<DateTime,DBObjectCollection<WrkgrpDateObject>>();
                }
                if (!_wrkgrp_dates[item.Wrkgrp_ID].ContainsKey(item.WorkDate.Date))
                {
                    _wrkgrp_dates[item.Wrkgrp_ID][item.WorkDate.Date] = new DBObjectCollection<WrkgrpDateObject>();
                }
                _wrkgrp_dates[item.Wrkgrp_ID][item.WorkDate.Date].Add(item);
                if (!_wrkgrp_begindates.ContainsKey(item.Wrkgrp_ID))
                {
                    _wrkgrp_begindates[item.Wrkgrp_ID] = new SortedSet<DateTime>();
                }
                _wrkgrp_begindates[item.Wrkgrp_ID].Add(item.begintime);
            }
        }

        public override bool Remove(WrkgrpDateObject item)
        {
            if (Contains(item))
            {
                if (_wrkgrp_dates.ContainsKey(item.Wrkgrp_ID) && _wrkgrp_dates[item.Wrkgrp_ID].ContainsKey(item.WorkDate.Date) && _wrkgrp_dates[item.Wrkgrp_ID][item.WorkDate.Date].Contains(item))
                {
                    _wrkgrp_dates[item.Wrkgrp_ID][item.WorkDate.Date].Remove(item);
                }
                if (_wrkgrp_begindates.ContainsKey(item.Wrkgrp_ID))
                {
                    _wrkgrp_begindates[item.Wrkgrp_ID].Remove(item.begintime);
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
            _wrkgrp_dates.Clear();
            _wrkgrp_begins.Clear();
            _wrkgrp_begindates.Clear();
            base.Clear();
        }

        public List<WrkgrpDateObject> GetDates(WrkgrpObject wrkgrp, DateTime dt)
        {
            var rslt = new List<WrkgrpDateObject>();
            if (_wrkgrp_dates.ContainsKey(wrkgrp._ID) && _wrkgrp_dates[wrkgrp._ID].ContainsKey(dt.Date))
            {
                foreach (var date in _wrkgrp_dates[wrkgrp._ID][dt.Date])
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

        public void PutWrkgrpBegin(WrkgrpObject wrkgrp, DateTime begin)
        {
            if (!_wrkgrp_begins.ContainsKey(wrkgrp._ID))
            {
                _wrkgrp_begins[wrkgrp._ID] = new SortedSet<DateTime>();
            }
            _wrkgrp_begins[wrkgrp._ID].Add(begin);
        }

        public IList<DateTime> GetDateArr(WrkgrpObject wrkgrp, DateTime lower, DateTime upper)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_wrkgrp_begins.ContainsKey(wrkgrp._ID))
            {
                rslt.AddRange(_wrkgrp_begins[wrkgrp._ID].GetViewBetween(lower, upper));
            }
            return rslt;
        }

        public DateTime? GetFirstBegin(WrkgrpObject wrkgrp)
        {
            if (_wrkgrp_begindates.ContainsKey(wrkgrp._ID))
            {
                if (_wrkgrp_begindates[wrkgrp._ID].Count > 0)
                {
                    return _wrkgrp_begindates[wrkgrp._ID].Min;
                }
            }
            return null;
        }

        public DateTime? GetLastBegin(WrkgrpObject wrkgrp)
        {
            if (_wrkgrp_begindates.ContainsKey(wrkgrp._ID))
            {
                if (_wrkgrp_begindates[wrkgrp._ID].Count > 0)
                {
                    return _wrkgrp_begindates[wrkgrp._ID].Max;
                }
            }
            return null;
        }
    }
}
