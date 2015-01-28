using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    public class TechObjectCellection : DBObjectCollection<TechObject>
    {
        private Dictionary<string, List<TechObject>> _tree_techs = new Dictionary<string, List<TechObject>>();

        private Dictionary<string, DBObjectCollection<TechObject>> _wrkgrpdate_techs = new Dictionary<string, DBObjectCollection<TechObject>>();

        private Dictionary<string, DBObjectCollection<TechObject>> _workgroupdate_techs = new Dictionary<string, DBObjectCollection<TechObject>>();

        public override void Add(TechObject item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
                if (!_tree_techs.ContainsKey(item.Tree_ID))
                {
                    _tree_techs[item.Tree_ID] = new List<TechObject>();
                }
                int i = 0;
                for (i = 0; i < _tree_techs[item.Tree_ID].Count; i++)
                {
                    if (item.begindate < _tree_techs[item.Tree_ID][i].begindate)
                    {
                        break;
                    }
                }
                _tree_techs[item.Tree_ID].Insert(i, item);

                // 添加与工组日历的关系
                if (!string.IsNullOrEmpty(item.WrkgrpDate_ID))
                {
                    if (!_wrkgrpdate_techs.ContainsKey(item.WrkgrpDate_ID))
                    {
                        _wrkgrpdate_techs[item.WrkgrpDate_ID] = new DBObjectCollection<TechObject>();
                    }
                    _wrkgrpdate_techs[item.WrkgrpDate_ID].Add(item);
                }

                // 添加与工作中心日历的关系
                if (!string.IsNullOrEmpty(item.WorkgroupDate_ID))
                {
                    if (!_workgroupdate_techs.ContainsKey(item.WorkgroupDate_ID))
                    {
                        _workgroupdate_techs[item.WorkgroupDate_ID] = new DBObjectCollection<TechObject>();
                    }
                    _workgroupdate_techs[item.WorkgroupDate_ID].Add(item);
                }
            }
        }

        public override bool Remove(TechObject item)
        {
            if (this.Contains(item))
            {
                if (_tree_techs.ContainsKey(item.Tree_ID) && _tree_techs[item.Tree_ID].Contains(item))
                {
                    _tree_techs[item.Tree_ID].Remove(item);
                }

                // 删除与工组日历的关系
                if (!string.IsNullOrEmpty(item.WrkgrpDate_ID)
                    && _wrkgrpdate_techs.ContainsKey(item.WrkgrpDate_ID)
                    && _wrkgrpdate_techs[item.WrkgrpDate_ID].Contains(item))
                {
                    _wrkgrpdate_techs[item.WrkgrpDate_ID].Remove(item);
                }

                // 删除与工作中心日历的关系
                if (!string.IsNullOrEmpty(item.WorkgroupDate_ID)
                    && _workgroupdate_techs.ContainsKey(item.WorkgroupDate_ID)
                    && _workgroupdate_techs[item.WorkgroupDate_ID].Contains(item))
                {
                    _workgroupdate_techs[item.WorkgroupDate_ID].Remove(item);
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
            _tree_techs.Clear();
            _wrkgrpdate_techs.Clear();
            _workgroupdate_techs.Clear();
            base.Clear();
        }

        public IList<TechObject> GetTechs(TreeObject tree)
        {
            if (_tree_techs.ContainsKey(tree._ID))
            {
                return _tree_techs[tree._ID].ToArray();
            }
            else
            {
                return new List<TechObject>();
            }
        }

        public IEnumerable<TechObject> GetTechs(WrkgrpDateObject date)
        {
            // DONE: 返回工组日历下的派工
            List<TechObject> rslt = new List<TechObject>();
            if (_wrkgrpdate_techs.ContainsKey(date._ID))
            {
                foreach (var tech in _wrkgrpdate_techs[date._ID])
                {
                    rslt.Add(tech);
                }
            }
            return rslt;
        }

        public IEnumerable<TechObject> GetTechs(WorkgroupDateObject date)
        {
            // DONE: 返回工组日历下的派工
            List<TechObject> rslt = new List<TechObject>();
            if (_workgroupdate_techs.ContainsKey(date._ID))
            {
                foreach (var tech in _workgroupdate_techs[date._ID])
                {
                    rslt.Add(tech);
                }
            }
            return rslt;
        }

        public TechObject GetTech(TreeObject toptree, WrkgrpDateObject date)
        {
            // DONE: 返回工艺树在工组日历下的派工
            if (_tree_techs.ContainsKey(toptree._ID))
            {
                var techs = _tree_techs[toptree._ID];
                foreach (var tech in techs)
                {
                    if (tech.Wrkgrp_ID == date.Wrkgrp_ID &&
                        tech.begindate >= date.begintime &&
                        tech.begindate < date.endtime)
                    {
                        return tech;
                    }
                }
            }
            return null;
        }
    }
}
