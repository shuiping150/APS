using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    // 资源日历占用对象
    public class TreeDateCollection : DBObjectCollection<TreeDate>
    {
        private Dictionary<string, DBObjectCollection<TreeDate>> _date_trees = new Dictionary<string, DBObjectCollection<TreeDate>>();

        private Dictionary<string, DBObjectCollection<TreeDate>> _tree_dates = new Dictionary<string, DBObjectCollection<TreeDate>>();

        private Dictionary<string, SortedSet<DateTime>> _tree_begins = new Dictionary<string, SortedSet<DateTime>>();

        private Dictionary<string, SortedSet<DateTime>> _tree_ends = new Dictionary<string, SortedSet<DateTime>>();

        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>> _tree_begin_dates = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>();

        private Dictionary<string, Dictionary<string, SortedSet<DateTime>>> _tree_group_begins = new Dictionary<string, Dictionary<string, SortedSet<DateTime>>>();

        private Dictionary<string, Dictionary<string, SortedSet<DateTime>>> _tree_group_ends = new Dictionary<string, Dictionary<string, SortedSet<DateTime>>>();

        private Dictionary<string, Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>> _tree_group_begin_dates = new Dictionary<string, Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>>();

        private Dictionary<string, Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>> _tree_rs_dates = new Dictionary<string, Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>>();
        private Dictionary<string, Dictionary<string, SortedSet<DateTime>>> _tree_rs_begins = new Dictionary<string, Dictionary<string, SortedSet<DateTime>>>();

        private Dictionary<string, Dictionary<string, SortedSet<DateTime>>> _tree_rs_ends = new Dictionary<string, Dictionary<string, SortedSet<DateTime>>>();

        private Dictionary<string, SortedSet<DateTime>> _group_begins = new Dictionary<string, SortedSet<DateTime>>();
        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>> _group_begin_dates = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>();

        private Dictionary<string, SortedSet<DateTime>> _rs_begins = new Dictionary<string, SortedSet<DateTime>>();
        private Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>> _rs_begin_dates = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>();

        public override void Add(TreeDate item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_date_trees.ContainsKey(item.ResourceDate_ID))
                {
                    _date_trees[item.ResourceDate_ID] = new DBObjectCollection<TreeDate>();
                }
                _date_trees[item.ResourceDate_ID].Add(item);

                if (!_tree_dates.ContainsKey(item.Tree_ID))
                {
                    _tree_dates[item.Tree_ID] = new DBObjectCollection<TreeDate>();
                }
                _tree_dates[item.Tree_ID].Add(item);

                if (!_tree_begin_dates.ContainsKey(item.Tree_ID))
                {
                    _tree_begin_dates[item.Tree_ID] = new Dictionary<DateTime, DBObjectCollection<TreeDate>>();
                }
                if (!_tree_begin_dates[item.Tree_ID].ContainsKey(item.begin))
                {
                    _tree_begin_dates[item.Tree_ID][item.begin] = new DBObjectCollection<TreeDate>();
                }
                _tree_begin_dates[item.Tree_ID][item.begin].Add(item);

                if (!_tree_begins.ContainsKey(item.Tree_ID))
                {
                    _tree_begins[item.Tree_ID] = new SortedSet<DateTime>();
                }
                _tree_begins[item.Tree_ID].Add(item.begin);

                if (!_tree_ends.ContainsKey(item.Tree_ID))
                {
                    _tree_ends[item.Tree_ID] = new SortedSet<DateTime>();
                }
                _tree_ends[item.Tree_ID].Add(item.end);

                if (!_tree_group_begin_dates.ContainsKey(item.Tree_ID))
                {
                    _tree_group_begin_dates[item.Tree_ID] = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>();
                }
                if (!_tree_group_begin_dates[item.Tree_ID].ContainsKey(item.ResourceGroup))
                {
                    _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup] = new Dictionary<DateTime, DBObjectCollection<TreeDate>>();
                }
                if (!_tree_group_begin_dates[item.Tree_ID][item.ResourceGroup].ContainsKey(item.begin))
                {
                    _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup][item.begin] = new DBObjectCollection<TreeDate>();
                }
                _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup][item.begin].Add(item);

                if (!_tree_group_begins.ContainsKey(item.Tree_ID))
                {
                    _tree_group_begins[item.Tree_ID] = new Dictionary<string, SortedSet<DateTime>>();
                }
                if (!_tree_group_begins[item.Tree_ID].ContainsKey(item.ResourceGroup))
                {
                    _tree_group_begins[item.Tree_ID][item.ResourceGroup] = new SortedSet<DateTime>();
                }
                _tree_group_begins[item.Tree_ID][item.ResourceGroup].Add(item.begin);

                if (!_tree_group_ends.ContainsKey(item.Tree_ID))
                {
                    _tree_group_ends[item.Tree_ID] = new Dictionary<string, SortedSet<DateTime>>();
                }
                if (!_tree_group_ends[item.Tree_ID].ContainsKey(item.ResourceGroup))
                {
                    _tree_group_ends[item.Tree_ID][item.ResourceGroup] = new SortedSet<DateTime>();
                }
                _tree_group_ends[item.Tree_ID][item.ResourceGroup].Add(item.end);

                if (!_tree_rs_dates.ContainsKey(item.Tree_ID))
                {
                    _tree_rs_dates[item.Tree_ID] = new Dictionary<string, Dictionary<DateTime, DBObjectCollection<TreeDate>>>();
                }
                if (!_tree_rs_dates[item.Tree_ID].ContainsKey(item.Resource_ID))
                {
                    _tree_rs_dates[item.Tree_ID][item.Resource_ID] = new Dictionary<DateTime, DBObjectCollection<TreeDate>>();
                }
                if (!_tree_rs_dates[item.Tree_ID][item.Resource_ID].ContainsKey(item.begin))
                {
                    _tree_rs_dates[item.Tree_ID][item.Resource_ID][item.begin] = new DBObjectCollection<TreeDate>();
                }
                _tree_rs_dates[item.Tree_ID][item.Resource_ID][item.begin].Add(item);

                if (!_tree_rs_begins.ContainsKey(item.Tree_ID))
                {
                    _tree_rs_begins[item.Tree_ID] = new Dictionary<string, SortedSet<DateTime>>();
                }
                if (!_tree_rs_begins[item.Tree_ID].ContainsKey(item.Resource_ID))
                {
                    _tree_rs_begins[item.Tree_ID][item.Resource_ID] = new SortedSet<DateTime>();
                }
                _tree_rs_begins[item.Tree_ID][item.Resource_ID].Add(item.begin);

                if (!_tree_rs_ends.ContainsKey(item.Tree_ID))
                {
                    _tree_rs_ends[item.Tree_ID] = new Dictionary<string, SortedSet<DateTime>>();
                }
                if (!_tree_rs_ends[item.Tree_ID].ContainsKey(item.Resource_ID))
                {
                    _tree_rs_ends[item.Tree_ID][item.Resource_ID] = new SortedSet<DateTime>();
                }
                _tree_rs_ends[item.Tree_ID][item.Resource_ID].Add(item.end);

                if (!_group_begins.ContainsKey(item.ResourceGroup))
                {
                    _group_begins[item.ResourceGroup] = new SortedSet<DateTime>();
                }
                _group_begins[item.ResourceGroup].Add(item.begin);
                if (!_group_begin_dates.ContainsKey(item.ResourceGroup))
                {
                    _group_begin_dates[item.ResourceGroup] = new Dictionary<DateTime, DBObjectCollection<TreeDate>>();
                }
                if (!_group_begin_dates[item.ResourceGroup].ContainsKey(item.begin))
                {
                    _group_begin_dates[item.ResourceGroup][item.begin] = new DBObjectCollection<TreeDate>();
                }
                _group_begin_dates[item.ResourceGroup][item.begin].Add(item);

                if (!_rs_begins.ContainsKey(item.Resource_ID))
                {
                    _rs_begins[item.Resource_ID] = new SortedSet<DateTime>();
                }
                _rs_begins[item.Resource_ID].Add(item.begin);
                if (!_rs_begin_dates.ContainsKey(item.Resource_ID))
                {
                    _rs_begin_dates[item.Resource_ID] = new Dictionary<DateTime, DBObjectCollection<TreeDate>>();
                }
                if (!_rs_begin_dates[item.Resource_ID].ContainsKey(item.begin))
                {
                    _rs_begin_dates[item.Resource_ID][item.begin] = new DBObjectCollection<TreeDate>();
                }
                _rs_begin_dates[item.Resource_ID][item.begin].Add(item);
            }
            else
            {
                var date = GetByID(item._ID);
                date.workhour += item.workhour;
            }
        }

        public override bool Remove(TreeDate item)
        {
            if (Contains(item))
            {
                if (_date_trees.ContainsKey(item.ResourceDate_ID) && _date_trees[item.ResourceDate_ID].Contains(item))
                {
                    _date_trees[item.ResourceDate_ID].Remove(item);
                }
                if (_tree_dates.ContainsKey(item.Tree_ID) && _tree_dates[item.Tree_ID].Contains(item))
                {
                    _tree_dates[item.Tree_ID].Remove(item);
                }
                if (_tree_begin_dates.ContainsKey(item.Tree_ID) && _tree_begin_dates[item.Tree_ID].ContainsKey(item.begin) && _tree_begin_dates[item.Tree_ID][item.begin].Contains(item))
                {
                    _tree_begin_dates[item.Tree_ID][item.begin].Remove(item);
                    if (_tree_begin_dates[item.Tree_ID][item.begin].Count == 0)
                    {
                        if (_tree_begins.ContainsKey(item.Tree_ID) && _tree_begins[item.Tree_ID].Contains(item.begin))
                        {
                            _tree_begins[item.Tree_ID].Remove(item.begin);
                        }
                        if (_tree_ends.ContainsKey(item.Tree_ID) && _tree_ends[item.Tree_ID].Contains(item.end))
                        {
                            _tree_ends[item.Tree_ID].Remove(item.end);
                        }
                    }
                }
                if (_tree_group_begin_dates.ContainsKey(item.Tree_ID) && _tree_group_begin_dates[item.Tree_ID].ContainsKey(item.ResourceGroup) && _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup].ContainsKey(item.begin) && _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup][item.begin].Contains(item))
                {
                    _tree_group_begin_dates[item.Tree_ID][item.ResourceGroup][item.begin].Remove(item);
                    if (_tree_group_begin_dates[item.Tree_ID][item.ResourceGroup][item.begin].Count == 0)
                    {
                        if (_tree_group_begins.ContainsKey(item.Tree_ID) && _tree_group_begins[item.Tree_ID].ContainsKey(item.ResourceGroup) && _tree_group_begins[item.Tree_ID][item.ResourceGroup].Contains(item.begin))
                        {
                            _tree_group_begins[item.Tree_ID][item.ResourceGroup].Remove(item.begin);
                        }
                        if (_tree_group_ends.ContainsKey(item.Tree_ID) && _tree_group_ends[item.Tree_ID].ContainsKey(item.ResourceGroup) && _tree_group_ends[item.Tree_ID][item.ResourceGroup].Contains(item.end))
                        {
                            _tree_group_ends[item.Tree_ID][item.ResourceGroup].Remove(item.end);
                        }
                    }
                }
                if (_tree_rs_dates.ContainsKey(item.Tree_ID) && _tree_rs_dates[item.Tree_ID].ContainsKey(item.Resource_ID) && _tree_rs_dates[item.Tree_ID][item.Resource_ID].ContainsKey(item.begin) && _tree_rs_dates[item.Tree_ID][item.Resource_ID][item.begin].Contains(item))
                {
                    _tree_rs_dates[item.Tree_ID][item.Resource_ID][item.begin].Remove(item);
                    if (_tree_rs_dates[item.Tree_ID][item.Resource_ID][item.begin].Count == 0)
                    {
                        if (_tree_rs_begins.ContainsKey(item.Tree_ID) && _tree_rs_begins[item.Tree_ID].ContainsKey(item.Resource_ID) && _tree_rs_begins[item.Tree_ID][item.Resource_ID].Contains(item.begin))
                        {
                            _tree_rs_begins[item.Tree_ID][item.Resource_ID].Remove(item.begin);
                        }
                        if (_tree_rs_ends.ContainsKey(item.Tree_ID) && _tree_rs_ends[item.Tree_ID].ContainsKey(item.Resource_ID) && _tree_rs_ends[item.Tree_ID][item.Resource_ID].Contains(item.end))
                        {
                            _tree_rs_ends[item.Tree_ID][item.Resource_ID].Remove(item.end);
                        }
                    }
                }
                if (_group_begin_dates.ContainsKey(item.ResourceGroup) && _group_begin_dates[item.ResourceGroup].ContainsKey(item.begin) && _group_begin_dates[item.ResourceGroup][item.begin].Contains(item))
                {
                    _group_begin_dates[item.ResourceGroup][item.begin].Remove(item);
                    if (_group_begins.ContainsKey(item.ResourceGroup) && _group_begins[item.ResourceGroup].Contains(item.begin) && _group_begin_dates[item.ResourceGroup][item.begin].Count == 0)
                    {
                        _group_begins[item.ResourceGroup].Remove(item.begin);
                    }
                }
                if (_rs_begin_dates.ContainsKey(item.Resource_ID) && _rs_begin_dates[item.Resource_ID].ContainsKey(item.begin) && _rs_begin_dates[item.Resource_ID][item.begin].Contains(item))
                {
                    _rs_begin_dates[item.Resource_ID][item.begin].Remove(item);
                    if (_rs_begins.ContainsKey(item.Resource_ID) && _rs_begins[item.Resource_ID].Contains(item.begin) && _rs_begin_dates[item.Resource_ID][item.begin].Count == 0)
                    {
                        _rs_begins[item.Resource_ID].Remove(item.begin);
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
            _date_trees.Clear();
            _tree_dates.Clear();
            _tree_begin_dates.Clear();
            _tree_begins.Clear();
            _tree_ends.Clear();
            _tree_group_begin_dates.Clear();
            _tree_group_begins.Clear();
            _tree_group_ends.Clear();
            _tree_rs_dates.Clear();
            _tree_rs_begins.Clear();
            _tree_rs_ends.Clear();
            _group_begins.Clear();
            _group_begin_dates.Clear();
            _rs_begins.Clear();
            _rs_begin_dates.Clear();
            base.Clear();
        }
        public List<TreeDate> GetByDate(ResourceDate date)
        {
            // DONE: 获取资源日历的分配情况
            List<TreeDate> rslt = new List<TreeDate>();
            if (_date_trees.ContainsKey(date._ID))
            {
                rslt.AddRange(_date_trees[date._ID]);
            }
            return rslt;
        }
        public List<TreeDate> GetByTree(TreeObject tree)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_tree_dates.ContainsKey(tree._ID))
            {
                rslt.AddRange(_tree_dates[tree._ID]);
            }
            return rslt;
        }

        public DateTime? GetFirstBegin(TreeObject tree)
        {
            if (_tree_begins.ContainsKey(tree._ID) && _tree_begins[tree._ID].Count > 0)
            {
                return _tree_begins[tree._ID].Min;
            }
            return null;
        }

        public DateTime? GetLastEnd(TreeObject tree)
        {
            if (_tree_ends.ContainsKey(tree._ID) && _tree_ends[tree._ID].Count > 0)
            {
                return _tree_ends[tree._ID].Max;
            }
            return null;
        }

        public IList<string> GetGroups(TreeObject tree)
        {
            var rslt = new List<string>();
            if (_tree_group_begins.ContainsKey(tree._ID))
            {
                foreach (var kvp in _tree_group_begins[tree._ID])
                {
                    rslt.Add(kvp.Key);
                }
            }
            return rslt;
        }

        public DateTime? GetGroupFirstBegin(TreeObject tree, string group)
        {
            DateTime? rslt = null;
            if (_tree_group_begins.ContainsKey(tree._ID) && _tree_group_begins[tree._ID].ContainsKey(group) && _tree_group_begins[tree._ID][group].Count > 0)
            {
                rslt = _tree_group_begins[tree._ID][group].Min;
            }
            return rslt;
        }

        public DateTime? GetGroupLastEnd(TreeObject tree, string group)
        {
            DateTime? rslt = null;
            if (_tree_group_ends.ContainsKey(tree._ID) && _tree_group_ends[tree._ID].ContainsKey(group) && _tree_group_ends[tree._ID][group].Count > 0)
            {
                rslt = _tree_group_ends[tree._ID][group].Max;
            }
            return rslt;
        }

        public List<TreeDate> GetGroupDates(TreeObject tree, string group, DateTime begin)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_tree_group_begin_dates.ContainsKey(tree._ID) && _tree_group_begin_dates[tree._ID].ContainsKey(group) && _tree_group_begin_dates[tree._ID][group].ContainsKey(begin))
            {
                foreach (var td in _tree_group_begin_dates[tree._ID][group][begin])
                {
                    rslt.Add(td);
                }
            }
            return rslt;
        }

        public List<DateTime> GetGroupBegins(TreeObject tree, string group)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_tree_group_begins.ContainsKey(tree._ID) && _tree_group_begins[tree._ID].ContainsKey(group) && _tree_group_begins[tree._ID][group].Count > 0)
            {
                rslt.AddRange(_tree_group_begins[tree._ID][group]);
            }
            return rslt;

        }

        public List<TreeDate> GetGroupDates(TreeObject tree, string group)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_tree_group_begin_dates.ContainsKey(tree._ID) && _tree_group_begin_dates[tree._ID].ContainsKey(group))
            {
                foreach (var dates in _tree_group_begin_dates[tree._ID][group])
                {
                    foreach (var td in dates.Value)
                    {
                        rslt.Add(td);
                    }
                }
            }
            return rslt;
        }

        public IList<TreeDate> GetRsDatesAsc(TreeObject tree, ResourceObject rs)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_tree_rs_begins.ContainsKey(tree._ID) && _tree_rs_begins[tree._ID].ContainsKey(rs._ID))
            {
                foreach (var dt in _tree_rs_begins[tree._ID][rs._ID])
                {
                    if (_tree_rs_dates.ContainsKey(tree._ID) && _tree_rs_dates[tree._ID].ContainsKey(rs._ID) && _tree_rs_dates[tree._ID][rs._ID].ContainsKey(dt))
                    {
                        foreach (var td in _tree_rs_dates[tree._ID][rs._ID][dt])
                        {
                            rslt.Add(td);
                        }
                    }
                }
            }

            return rslt;
        }

        public IList<DateTime> GetTreeRsBegins(TreeObject tree, ResourceObject rs)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_tree_rs_begins.ContainsKey(tree._ID) && _tree_rs_begins[tree._ID].ContainsKey(rs._ID))
            {
                rslt.AddRange(_tree_rs_begins[tree._ID][rs._ID]);
            }
            return rslt;
        }

        public List<TreeDate> GetTreeRsBegins(TreeObject tree, ResourceObject rs, DateTime begin, DateTime end)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_tree_rs_begins.ContainsKey(tree._ID) && _tree_rs_begins[tree._ID].ContainsKey(rs._ID))
            {
                foreach (var dt in _tree_rs_begins[tree._ID][rs._ID].GetViewBetween(begin, end))
                {
                    rslt.AddRange(_tree_rs_dates[tree._ID][rs._ID][dt]);
                }
            }
            return rslt;
        }

        public DateTime? GetTreeRsBegin(TreeObject tree, ResourceObject rs)
        {
            if (_tree_rs_begins.ContainsKey(tree._ID) && _tree_rs_begins[tree._ID].ContainsKey(rs._ID) && _tree_rs_begins[tree._ID][rs._ID].Count > 0)
            {
                return _tree_rs_begins[tree._ID][rs._ID].Min;
            }
            return null;
        }

        public DateTime? GetTreeRsEnd(TreeObject tree, ResourceObject rs)
        {
            if (_tree_rs_ends.ContainsKey(tree._ID) && _tree_rs_ends[tree._ID].ContainsKey(rs._ID) && _tree_rs_ends[tree._ID][rs._ID].Count > 0)
            {
                return _tree_rs_ends[tree._ID][rs._ID].Max;
            }
            return null;
        }

        public IList<DateTime> GetGroupBegins(string groupstr)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_group_begins.ContainsKey(groupstr))
            {
                foreach (var begin in _group_begins[groupstr])
                {
                    rslt.Add(begin);
                }
            }
            return rslt;
        }

        public IList<TreeDate> GetGroupDates(string groupstr, DateTime begin)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_group_begin_dates.ContainsKey(groupstr))
            {
                if (_group_begin_dates[groupstr].ContainsKey(begin))
                {
                    rslt.AddRange(_group_begin_dates[groupstr][begin]);
                }
            }
            return rslt;
        }

        public IList<DateTime> GetRsBegins(ResourceObject obj)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_rs_begins.ContainsKey(obj._ID))
            {
                rslt.AddRange(_rs_begins[obj._ID]);
            }
            return rslt;
        }

        public IList<DateTime> GetRsBegins(ResourceObject obj, DateTime lower, DateTime upper)
        {
            List<DateTime> rslt = new List<DateTime>();
            if (_rs_begins.ContainsKey(obj._ID))
            {
                rslt.AddRange(_rs_begins[obj._ID].GetViewBetween(lower, upper));
            }
            return rslt;
        }

        public IList<TreeDate> GetRsDates(ResourceObject obj, DateTime begin)
        {
            List<TreeDate> rslt = new List<TreeDate>();
            if (_rs_begin_dates.ContainsKey(obj._ID) && _rs_begin_dates[obj._ID].ContainsKey(begin))
            {
                rslt.AddRange(_rs_begin_dates[obj._ID][begin]);
            }
            return rslt;
        }

        public TreeDate NewDate(TreeObject tree, ResourceDate date, double workhour, string RsGroup)
        {
            TreeDate rslt = new TreeDate();
            rslt._ID = ToID(tree, date);
            rslt.Tree_ID = tree._ID;
            rslt.ResourceDate_ID = date._ID;
            rslt.begin = date.begintime;
            rslt.end = date.endtime;
            rslt.workhour = workhour;
            rslt.ResourceGroup = RsGroup;
            rslt.Resource_ID = date.Resource_ID;
            return rslt;
        }

        public string ToID(TreeObject tree, ResourceDate date)
        {
            return "占用:" + tree._ID + "," + date._ID;
        }

        public string ToGroupStr(IEnumerable<ResourceObject> rses)
        {
            SortedSet<string> sortedIDs = new SortedSet<string>();
            foreach (var obj in rses)
            {
                sortedIDs.Add(obj._ID);
            }

            string rslt = string.Empty;
            foreach (var obj in sortedIDs)
            {
                if (string.IsNullOrEmpty(rslt))
                {
                    rslt = obj;
                }
                else
                {
                    rslt += ";" + obj;
                }
            }
            return rslt;
        }

        public IList<string> GetGroupResources(string groupstr)
        {
            return groupstr.Split(';');
        }
    }
}
