using System;
using System.Collections.Generic;

namespace APSV1.Model
{
    // 逻辑数据
    public class DBCache
    {
        #region 私有成员
        private MLObjectCollection _MLObjectCollection = new MLObjectCollection();
        private ZLObjectCollection _ZLObjectCollection = new ZLObjectCollection();
        private TreeObjectCellection _TreeObjectCellection = new TreeObjectCellection();
        private TechObjectCellection _TechObjectCellection = new TechObjectCellection();
        private ScObjectCellection _ScObjectCellection = new ScObjectCellection();
        private WkpObjectCollection _WkpObjectCollection = new WkpObjectCollection();
        private WrkgrpObjectCollection _WrkgrpObjectCollection = new WrkgrpObjectCollection();
        private WorkgroupObjectCollection _WorkgroupObjectCollection = new WorkgroupObjectCollection();
        private WrkgrpDateObjectCollection _WrkgrpDateObjectCollection = new WrkgrpDateObjectCollection();
        private WorkgroupDateObjectCollection _WorkgroupDateObjectCollection = new WorkgroupDateObjectCollection();
        private EquipmentObjectCollection _EquipmentObjectCollection = new EquipmentObjectCollection();
        private EquipmentDateObjectCollection _EquipmentDateObjectCollection = new EquipmentDateObjectCollection();
        private TreeEqObjectCollection _TreeEqObjectCollection = new TreeEqObjectCollection();
        private RqMtrlTreeObjectCollection _RqMtrlTreeObjectCollection = new RqMtrlTreeObjectCollection();
        private RqMtrlObjectCollection _RqMtrlObjectCollection = new RqMtrlObjectCollection();
        private MtrlObjectCollection _MtrlObjectCollection = new MtrlObjectCollection();
        private CustObjectCollection _CustObjectCollection = new CustObjectCollection();
        private WrkgrpEquipmentObjectCollection _WrkgrpEquipmentObjectCollection = new WrkgrpEquipmentObjectCollection();
        private WorkgroupEquipmentObjectCollection _WorkgroupEquipmentObjectCollection = new WorkgroupEquipmentObjectCollection();
        private ResourceTypeObjectCollection _ResourceTypeObjectCollection = new ResourceTypeObjectCollection();
        private TreeRsTypeCollection _TreeRsTypeCollection = new TreeRsTypeCollection();
        private ResourceObjectCollection _ResourceObjectCollection = new ResourceObjectCollection();
        private ResourceMapCollection _ResourceMapCollection = new ResourceMapCollection();
        private ResourceDateCollection _ResourceDateCollection = new ResourceDateCollection();
        private TreeDateCollection _TreeDateCollection = new TreeDateCollection();
        #endregion


        #region 数据集合
        public MLObjectCollection MLObjectCollection { get { return _MLObjectCollection; } }
        public ZLObjectCollection ZLObjectCollection { get { return _ZLObjectCollection; } }
        public TreeObjectCellection TreeObjectCellection { get { return _TreeObjectCellection; } }
        public TechObjectCellection TechObjectCellection { get { return _TechObjectCellection; } }
        public ScObjectCellection ScObjectCellection { get { return _ScObjectCellection; } }
        public WkpObjectCollection WkpObjectCollection { get { return _WkpObjectCollection; } }
        public WrkgrpObjectCollection WrkgrpObjectCollection { get { return _WrkgrpObjectCollection; } }
        public WorkgroupObjectCollection WorkgroupObjectCollection { get { return _WorkgroupObjectCollection; } }
        public WrkgrpDateObjectCollection WrkgrpDateObjectCollection { get { return _WrkgrpDateObjectCollection; } }
        public WorkgroupDateObjectCollection WorkgroupDateObjectCollection { get { return _WorkgroupDateObjectCollection; } }
        public EquipmentObjectCollection EquipmentObjectCollection { get { return _EquipmentObjectCollection; } }
        public EquipmentDateObjectCollection EquipmentDateObjectCollection { get { return _EquipmentDateObjectCollection; } }
        public TreeEqObjectCollection TreeEqObjectCollection { get { return _TreeEqObjectCollection; } }
        public RqMtrlTreeObjectCollection RqMtrlTreeObjectCollection { get { return _RqMtrlTreeObjectCollection; } }
        public RqMtrlObjectCollection RqMtrlObjectCollection { get { return _RqMtrlObjectCollection; } }
        public MtrlObjectCollection MtrlObjectCollection { get { return _MtrlObjectCollection; } }
        public CustObjectCollection CustObjectCollection { get { return _CustObjectCollection; } }
        public WrkgrpEquipmentObjectCollection WrkgrpEquipmentObjectCollection { get { return _WrkgrpEquipmentObjectCollection; } }
        public WorkgroupEquipmentObjectCollection WorkgroupEquipmentObjectCollection { get { return _WorkgroupEquipmentObjectCollection; } }
        public ResourceTypeObjectCollection ResourceTypeObjectCollection { get { return _ResourceTypeObjectCollection; } }
        public TreeRsTypeCollection TreeRsTypeCollection { get { return _TreeRsTypeCollection; } }
        public ResourceObjectCollection ResourceObjectCollection { get { return _ResourceObjectCollection; } }
        public ResourceMapCollection ResourceMapCollection { get { return _ResourceMapCollection; } }
        public ResourceDateCollection ResourceDateCollection { get { return _ResourceDateCollection; } }
        public TreeDateCollection TreeDateCollection { get { return _TreeDateCollection; } }
        #endregion

        private DateTime MaxTime
        {
            get
            {
                return DateTime.Today.AddDays(-DateTime.Today.DayOfYear).AddYears(6);
            }
        }

        private DateTime MaxErrTime
        {
            get
            {
                return MaxTime.AddYears(3);
            }
        }

        private DateTime UsableBegin
        {
            get
            {
                return DateTime.Today;
            }
        }


        #region 资源日历方法
        public double GetUsedHour(ResourceDate date)
        {
            var treeDates = TreeDateCollection.GetByDate(date);
            double usedhour = date.usedhour;
            foreach (var treeDate in treeDates)
            {
                usedhour += treeDate.workhour;
            }
            return usedhour;
        }
        public double GetUsedHour(ResourceDate date, TreeObject tree)
        {
            var zl = ZLObjectCollection.GetParent(tree);
            var ml = MLObjectCollection.GetParent(zl);

            var treeDates = TreeDateCollection.GetByDate(date);
            double usedhour = date.usedhour;
            foreach (var treeDate in treeDates)
            {
                var curtree = TreeObjectCellection.GetByID(treeDate.Tree_ID);
                var curzl = ZLObjectCollection.GetParent(curtree);
                var curml = MLObjectCollection.GetParent(curzl);
                if (curtree == tree)
                {
                    continue;
                }
                if (curzl == zl)
                {
                    if (IsPreOf(curtree, tree) || IsPreOf(tree, curtree))
                    {
                        continue;
                    }
                }
                if (curml == ml)
                {
                    if (IsPreOf(curzl, zl) || IsPreOf(zl, curzl))
                    {
                        continue;
                    }
                }
                if (curml.level > ml.level)
                {
                    continue;
                }
                usedhour += treeDate.workhour;
            }
            return usedhour;
        }


        // 计算班次时间后
        public DateTime? GetTimeAfterLasthour(WrkgrpObject wrkgrp, DateTime dtbegin, double lasthour, bool revert)
        {
            if (lasthour < 0)
            {
                return GetTimeAfterLasthour(wrkgrp, dtbegin, -lasthour, !revert);
            }

            DateTime dt = dtbegin.Date;
            double lakehour = lasthour;
            int days = 0;

            while (lakehour > 0)
            {
                var dates = WrkgrpDateObjectCollection.GetDates(wrkgrp, dt);
                for (int i = 0; i < dates.Count; i++)
                {
                    int index = revert ? dates.Count - i - 1 : i;
                    var date = dates[index];
                    //if (date.totalemp <= 0)
                    //{
                    //    continue;
                    //}
                    if (revert)
                    {
                        if (date.begintime < dtbegin)
                        {
                            DateTime endDt = dtbegin < date.endtime ? dtbegin : date.endtime;
                            double curhour = (endDt - date.begintime).TotalHours;
                            if (lakehour <= curhour)
                            {
                                return endDt.AddHours(-lakehour);
                            }
                            else
                            {
                                lakehour -= curhour;
                            }
                        }
                    }
                    else
                    {
                        if (date.endtime > dtbegin)
                        {
                            DateTime startDt = dtbegin > date.begintime ? dtbegin : date.begintime;
                            double curhour = (date.endtime - startDt).TotalHours;
                            if (lakehour <= curhour)
                            {
                                return startDt.AddHours(lakehour);
                            }
                            else
                            {
                                lakehour -= curhour;
                            }
                        }
                    }

                }
                if (dates.Count == 0)
                {
                    days++;
                    if (days >= 90)
                    {
                        return null;
                    }
                }
                else
                {
                    days = 0;
                }
                if (revert)
                {
                    dt = dt.AddDays(-1);
                }
                else
                {
                    dt = dt.AddDays(1);
                }
            }
            return dtbegin;
        }

        // 正常查询日历,连续、考虑产能、忽略自己及前后、忽略优先级低的
        public bool FindRoom_V2(TreeObject tree, DateTime dt, bool revert, ICollection<TreeDate> treeDates, ICollection<TreeObject> listToMove, ref string arg_msg)
        {
            double totalhour = tree.workhour * tree.qty;
            double lakehour = totalhour;
            var wrkgrp = WrkgrpObjectCollection.GetByID(tree.Wrkgrp_ID);
            DateTime lower = revert ? DateTime.MinValue : dt;
            DateTime upper = revert ? dt : DateTime.MaxValue;
            IList<DateTime> alltime = WrkgrpDateObjectCollection.GetDateArr(wrkgrp, lower, upper);

            int maxGroupNum = (int)(tree.lasthour > 0 ? Math.Round(totalhour / tree.lasthour) : totalhour);
            if (maxGroupNum < 1)
            {
                maxGroupNum = 1;
            }

            DBObjectCollection<ResourceObject> allResources = new DBObjectCollection<ResourceObject>();
            List<List<ResourceObject>> pickedResources = new List<List<ResourceObject>>(); // 选中的资源对象
            List<List<TreeDate>> pickedDates = new List<List<TreeDate>>(); // 资源对象被占用的时间
            List<double> pickedHour = new List<double>(); // 每个组占用的时间
            List<bool> pickedOk = new List<bool>(); // 标记选中资源是否连续有空

            int minBeginIndex = 0;

            for (int i = 0; i < alltime.Count; i++)
            {
                DateTime curBegin = revert ? alltime[alltime.Count - i - 1] : alltime[i];
                if ((revert && curBegin < dt) || (!revert && curBegin >= dt))
                {
                    // 如果已经有选中资源，就优先使用被选中的资源
                    if (pickedResources.Count > 0)
                    {
                        // 看现选资源能否完成任务
                        for (int j = 0; j < pickedResources.Count; j++)
                        {
                            if (!pickedOk[j])
                            {
                                continue;
                            }
                            List<ResourceObject> curRses = pickedResources[j];
                            double freeHour = 0;
                            double usedHour = 0;
                            List<TreeDate> tDates = new List<TreeDate>();
                            GetGroupFree(tree, curRses, curBegin, ref freeHour, ref usedHour, tDates);
                            if (tDates.Count != curRses.Count)
                            {
                                pickedOk[j] = false;// 第j组连续中断
                            }
                            else if (freeHour >= lakehour) // 可以完成
                            {
                                foreach (var date in tDates)
                                {
                                    date.workhour = lakehour;
                                }
                                lakehour -= lakehour;
                                pickedDates[j].AddRange(tDates);
                                pickedHour[j] += lakehour;
                                break;
                            }
                            else if (freeHour > 0) // 非休息日
                            {
                                foreach (var date in tDates)
                                {
                                    date.workhour = freeHour;
                                }
                                lakehour -= freeHour;
                                pickedDates[j].AddRange(tDates);
                                pickedHour[j] += freeHour;
                                if (usedHour > 0)
                                {
                                    pickedOk[j] = false; // 第j组连续中断
                                }
                            }
                            else
                            {
                                if (usedHour > 0)
                                {
                                    pickedOk[j] = false; // 第j组连续中断
                                }
                            }
                        }

                        if (lakehour > 0) // 未完成就删除不连续的组合
                        {
                            bool allTer = true; // 全部中断
                            foreach (bool ok in pickedOk)
                            {
                                if (ok)
                                {
                                    allTer = false;
                                }
                            }

                            if (allTer)
                            {
                                allResources.Clear();
                                pickedResources.Clear();
                                pickedDates.Clear();
                                pickedHour.Clear();
                                pickedOk.Clear();
                                lakehour = totalhour;
                                i = minBeginIndex;
                                continue;
                            }

                            for (int j = pickedResources.Count - 1; j >= 0; j--)
                            {
                                if (!pickedOk[j] && pickedHour[j] <= tree.beforehour + tree.afterhour)
                                {
                                    lakehour = lakehour - tree.beforehour - tree.afterhour + pickedHour[j];
                                    foreach (var rs in pickedResources[j])
                                    {
                                        allResources.Remove(rs);
                                    }
                                    pickedResources.RemoveAt(j);
                                    pickedDates.RemoveAt(j);
                                    pickedHour.RemoveAt(j);
                                    pickedOk.RemoveAt(j);
                                }
                            }

                        }
                    }

                    while (pickedResources.Count < maxGroupNum)
                    {
                        if (lakehour <= 0 && pickedResources.Count > 0)
                        {
                            break;
                        }
                        List<ResourceObject> resources = new List<ResourceObject>();
                        List<TreeDate> tDates = new List<TreeDate>();

                        double freeHour = 0;
                        // 尝试建立组合,返回空闲组合，及共同空闲时间
                        GetFree(tree, curBegin, dt, resources, tDates, ref freeHour, allResources);
                        if (freeHour > 0)
                        {
                            if (pickedResources.Count == 0)
                            {
                                minBeginIndex = i;
                            }

                            lakehour += tree.beforehour + tree.afterhour;

                            double curHour = freeHour >= lakehour ? lakehour : freeHour;
                            foreach (var date in tDates)
                            {
                                date.workhour = curHour;
                            }
                            lakehour -= curHour;
                            allResources.AddRange(resources);
                            pickedResources.Add(resources);
                            pickedDates.Add(tDates);
                            pickedHour.Add(curHour);
                            pickedOk.Add(true);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (lakehour <= 0 && pickedResources.Count > 0)
                {
                    var curzl = ZLObjectCollection.GetParent(tree);
                    var curml = MLObjectCollection.GetParent(curzl);

                    foreach (var dates in pickedDates) // 每个组合
                    {
                        foreach (var date in dates) // 组合下各个占用
                        {
                            // 对应的资源日历
                            var RsDate = ResourceDateCollection.GetByID(date.ResourceDate_ID);
                            foreach (var td in TreeDateCollection.GetByDate(RsDate))
                            {
                                // 其它工艺进度
                                var t = TreeObjectCellection.GetByID(td.Tree_ID);
                                if (t != null)
                                {
                                    var zl = ZLObjectCollection.GetParent(t);
                                    if (zl != null)
                                    {
                                        var ml = MLObjectCollection.GetParent(zl);
                                        if (ml.level > curml.level)
                                        {
                                            listToMove.Add(t);
                                        }
                                    }
                                }
                            }
                            treeDates.Add(date);
                        }
                    }
                    return true;
                }
            }

            arg_msg = "没有找到日历";
            return false;
        }

        // 在选中资源中汇总日历
        private void GetGroupFree(TreeObject tree, IEnumerable<ResourceObject> pickedResources, DateTime curBegin, ref double freeHour, ref double usedHour, ICollection<TreeDate> tDates)
        {
            freeHour = 0;
            usedHour = 0;
            tDates.Clear();
            string RsGroup = TreeDateCollection.ToGroupStr(pickedResources);
            foreach (var obj in pickedResources)
            {
                var date = ResourceDateCollection.GetOneDate(obj, curBegin);
                if (date == null)
                {
                    return;
                }
                TreeDate td = TreeDateCollection.NewDate(tree, date, date.workhour, RsGroup);
                tDates.Add(td);

                var curUsedHour = GetUsedHour(date, tree);
                var curFreeHour = date.workhour - curUsedHour;

                if (freeHour == 0 || curFreeHour < freeHour)
                {
                    freeHour = curFreeHour;
                }
                if (usedHour == 0 || curUsedHour > usedHour)
                {
                    usedHour = curUsedHour;
                }
            }
        }

        // 获取tree在curBegin处可用的时间
        private void GetFree(TreeObject tree, DateTime curBegin, DateTime dt, ICollection<ResourceObject> pickedResources, ICollection<TreeDate> dates, ref double freeHour, DBObjectCollection<ResourceObject> exclude)
        {
            pickedResources.Clear();
            dates.Clear();

            // 记录选中的对象与日历、及共同空闲时间
            List<ResourceObject> p_objs = new List<ResourceObject>();
            List<ResourceDate> p_dates = new List<ResourceDate>();
            freeHour = 0;


            var treeRsType = TreeRsTypeCollection.GetResources(tree);
            foreach (var treeType in treeRsType)
            {
                // 记录当前类型中最空闲的日历
                List<ResourceDate> topDats = new List<ResourceDate>();
                List<double> topFreeHours = new List<double>();
                var type = ResourceTypeObjectCollection.GetByID(treeType.ResourceType_ID);
                var maps = ResourceMapCollection.GetMapsForType(type);
                foreach (var map in maps)
                {
                    var rsObj = ResourceObjectCollection.GetByID(map.Resource_ID);
                    if (exclude.Contains(rsObj))
                    {
                        continue;
                    }
                    var rsDate = ResourceDateCollection.GetOneDate(rsObj, curBegin);
                    if (rsDate != null)
                    {
                        var curFreeHour = rsDate.workhour - GetUsedHour(rsDate, tree);
                        if (!(dt > rsDate.begintime && dt < rsDate.endtime) && curFreeHour > 0)
                        {
                            int i = 0;
                            for (i = 0; i < topDats.Count; i++)
                            {
                                if (curFreeHour > topFreeHours[i])
                                {
                                    break;
                                }
                            }
                            topFreeHours.Insert(i, curFreeHour);
                            topDats.Insert(i, rsDate);
                        }
                    }
                }
                // 资源数量不足就直接返回
                if (topDats.Count < treeType.num)
                {
                    freeHour = 0;
                    return;
                }
                // 记录本类型选中的对象与日历
                for (int i = 0; i < treeType.num; i++)
                {
                    var rsObj = ResourceObjectCollection.GetByID(topDats[i].Resource_ID);
                    p_objs.Add(rsObj);
                    p_dates.Add(topDats[i]);
                    if (freeHour == 0 || topFreeHours[i] < freeHour)
                    {
                        freeHour = topFreeHours[i];
                    }
                }
            }
            string RsGroup = TreeDateCollection.ToGroupStr(p_objs);
            for (int i = 0; i < p_objs.Count; i++)
            {
                pickedResources.Add(p_objs[i]);
                TreeDate tDate = TreeDateCollection.NewDate(tree, p_dates[i], freeHour, RsGroup);
                dates.Add(tDate);
            }
        }

        public double GetHoursAfter(WrkgrpObject wrkgrp, DateTime begin, DateTime end)
        {
            double rslt = 0;
            for (DateTime dt = begin.Date; dt <= end; dt = dt.AddDays(1))
            {
                var wrkgrpdates = WrkgrpDateObjectCollection.GetDates(wrkgrp, dt);
                foreach (var date in wrkgrpdates)
                {
                    //if (date.totalemp == 0)
                    //{
                    //    continue;
                    //}
                    if (date.begintime < end && date.endtime > begin)
                    {
                        DateTime dt1 = begin > date.begintime ? begin : date.begintime;
                        DateTime dt2 = end < date.endtime ? end : date.endtime;
                        if (dt2 > dt1)
                        {
                            rslt += (dt2 - dt1).TotalHours;
                        }
                    }
                }
            }
            return rslt;
        }
        #endregion

        #region 工艺树方法
        public bool IsPreOf(TreeObject curtree, TreeObject tree)
        {
            TreeObject nexttree = curtree;
            while ((nexttree = TreeObjectCellection.GetNextTree(nexttree)) != null)
            {
                if (tree == nexttree)
                {
                    return true;
                }
            }
            return false;
        }

        public DateTime? GetBegintime(TreeObject tree)
        {
            if (tree.begindate != null)
            {
                return tree.begindate;
            }
            else
            {
                return TreeDateCollection.GetFirstBegin(tree);
            }
        }

        public DateTime? GetEndTime(TreeObject tree)
        {
            if (tree.enddate != null)
            {
                return tree.enddate;
            }
            else
            {
                return TreeDateCollection.GetLastEnd(tree);
            }
        }

        public DateTime? GetUsableBeginForNext(TreeObject tree)
        {
            var nexttree = TreeObjectCellection.GetNextTree(tree);
            if (nexttree == null || tree.lastmode == lastmode.Wait || tree.waittime > 0)
            {
                DateTime? dtEnd = GetEndTime(tree);
                if (dtEnd == null)
                {
                    return null;
                }
                return dtEnd.Value.AddHours(tree.waittime);
            }
            else
            {
                return GetBeginAfterLasthour(tree);
            }
        }
        // 按交接周期获取后工艺可开始时间
        private DateTime? GetBeginAfterLasthour(TreeObject tree)
        {
            DateTime? dtbegin = GetBegintime(tree);
            if (dtbegin == null)
            {
                return null;
            }
            var wrkgrp = WrkgrpObjectCollection.GetWrkgrp(tree);
            return GetTimeAfterLasthour(wrkgrp, dtbegin.Value, tree.lasthour, false);
        }

        public DateTime? GetUsableBegin(TreeObject tree)
        {
            // 计算本工艺树的最早可开始时间
            var pretrees = TreeObjectCellection.GetPreTrees(tree);
            if (pretrees.Count == 0)
            {
                // 按前指令的末工艺完工时间计算 + 外协周期
                DateTime? dt = null;
                var zl = ZLObjectCollection.GetParent(tree);
                if (zl == null)
                {
                    return null;
                }
                if (zl.Begin == null)
                {
                    return null;
                }
                dt = zl.Begin;
                return dt;
                //var zlrqs = RqMtrlTreeObjectCollection.GetRqs(zl.ML_ID, zl.Mtrl_ID);
                //foreach (var zlrq in zlrqs)
                //{
                //    if (zlrq.Begin == null)
                //    {
                //        return null;
                //    }
                //    if (dt == null || zlrq.Begin > dt)
                //    {
                //        dt = zlrq.Begin;
                //    }
                //}
                //return dt;
            }
            else
            {
                DateTime? dt = null;
                foreach (var pretree in pretrees)
                {
                    DateTime? dtUsableBeginForNext = GetUsableBeginForNext(pretree);
                    if (dtUsableBeginForNext == null)
                    {
                        return null;
                    }
                    if (dt == null || dtUsableBeginForNext > dt)
                    {
                        dt = dtUsableBeginForNext;
                    }
                }
                return dt;
            }
        }

        public DateTime? GetUsalbeEnd(TreeObject tree)
        {
            DateTime? nextbegin = null;
            var nextTree = TreeObjectCellection.GetNextTree(tree);
            if (nextTree == null)
            {
                var zl = ZLObjectCollection.GetParent(tree);
                if (zl == null)
                {
                    return null;
                }
                if (zl.End == null)
                {
                    return null;
                }
                nextbegin = zl.End;
                //var zlrqs = RqMtrlTreeObjectCollection.GetRqs(zl.ML_ID, zl.Mtrl_ID);
                //foreach (var zlrq in zlrqs)
                //{
                //    if (zlrq.End == null)
                //    {
                //        return null;
                //    }
                //    if (nextbegin == null || zlrq.End < nextbegin)
                //    {
                //        nextbegin = zlrq.End;
                //    }
                //}
            }
            else
            {
                nextbegin = GetBegintime(nextTree);
                if (nextbegin == null)
                {
                    return null;
                }
            }
            var wrkgrp = WrkgrpObjectCollection.GetWrkgrp(tree);
            DateTime? maxBegin = GetTimeAfterLasthour(wrkgrp, nextbegin.Value, tree.lasthour, true);

            var rates = new TreeDateCollection();
            DBObjectCollection<TreeObject> listToMove = new DBObjectCollection<TreeObject>();
            string arg_msg = string.Empty;
            if (nextTree == null || tree.waittime > 0 || tree.lastmode == lastmode.Wait)
            {
                DateTime maxEnd = nextbegin.Value.AddHours(-tree.waittime);
                if (maxBegin == null || maxBegin > maxEnd)
                {
                    return maxEnd;
                }

                // 按交接周期向后排，如果结束时间>maxEnd，返回maxEnd，否则返回实际结束时间
                if (!FindRoom_V2(tree, maxBegin.Value, false, rates, listToMove, ref arg_msg))
                {
                    return maxEnd;
                }
                else
                {
                    DateTime dtEnd = rates.GetLastEnd(tree).Value;
                    if (dtEnd < maxEnd)
                    {
                        return dtEnd;
                    }
                    else
                    {
                        return maxEnd;
                    }
                }
            }
            else
            {
                // 以maxBegin向后排，如果开始时间<=maxBegin就返回预排结束时间，否则以结束时间推前一小时向前排
                if (maxBegin == null)
                {
                    return GetEndTime(nextTree);
                }
                if (!FindRoom_V2(tree, maxBegin.Value, false, rates, listToMove, ref arg_msg))
                {
                    return GetEndTime(nextTree);
                }
                else
                {
                    DateTime dtEnd = rates.GetLastEnd(tree).Value;
                    DateTime dtBegin = rates.GetFirstBegin(tree).Value;
                    DateTime? nextEnd = GetEndTime(nextTree);
                    if (nextEnd == null)
                    {
                        return null;
                    }

                    if (dtBegin <= maxBegin) // 产能足够
                    {
                        return dtEnd < nextEnd ? dtEnd : nextEnd;
                    }
                    else
                    {
                        if (nextEnd < dtEnd)
                        {
                            return nextEnd;
                        }
                        else
                        {
                            return dtEnd.AddHours(-0.1);
                        }
                    }
                }
            }
        }

        public string GetTreeErrMsg(TreeObject tree)
        {
            var tds = TreeDateCollection.GetByTree(tree);
            if (tds.Count <= 0)
            {
                return "暂未排单";
            }
            return string.Empty;
        }

        public IList<TechObject> BuildTechs(TreeObject tree)
        {
            var rslt = new List<TechObject>();
            var groups = TreeDateCollection.GetGroups(tree);
            double lakeqty = tree.qty;
            foreach (var group in groups)
            {
                var rses = group.Split(';');

                ResourceObject rs = null;
                foreach (var rs_id in rses)
                {
                    rs = ResourceObjectCollection.GetByID(rs_id);
                    if (rs != null)
                    {
                        break;
                    }
                }
                if (rs == null)
                {
                    throw new Exception("没有打开任何一个资源" + group);
                }
                var tds = TreeDateCollection.GetRsDatesAsc(tree, rs);
                if (tds.Count > 0)
                {
                    TechObject tech = new TechObject();
                    tech.begindate = tds[0].begin;
                    tech.enddate = tds[tds.Count - 1].end;
                    tech.workhour = 0;
                    foreach (var td in tds)
                    {
                        tech.workhour += td.workhour;
                    }
                    tech.qty = tree.workhour == 0 ? tree.qty : Math.Round((tech.workhour - tree.beforehour - tree.afterhour) / tree.workhour);
                    lakeqty -= tech.qty;
                    rslt.Add(tech);
                }
            }
            if (lakeqty != 0 && rslt.Count > 0)
            {
                rslt[rslt.Count - 1].qty += lakeqty;
            }
            return rslt;
        }

        #endregion

        #region 指令方法
        public bool IsPreOf(ZLObject curzl, ZLObject zl)
        {
            var curRqs = RqMtrlTreeObjectCollection.GetRqs(curzl.ML_ID, curzl.Mtrl_ID);
            var Rqs = RqMtrlTreeObjectCollection.GetRqs(zl.ML_ID, zl.Mtrl_ID);
            foreach (var curRq in curRqs)
            {
                foreach (var rq in Rqs)
                {
                    if (IsPreOf(curRq, rq))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public DateTime? GetBegintime(ZLObject zl)
        {
            var trees = TreeObjectCellection.GetChildTrees(zl);
            var firsttrees = new List<TreeObject>();
            foreach (var tree in trees)
            {
                var pre = TreeObjectCellection.GetPreTrees(tree);
                if (pre.Count == 0)
                {
                    firsttrees.Add(tree);
                }
            }
            if (firsttrees.Count == 0)
            {
                return null;
            }
            DateTime? dt = null;
            foreach (var tree in firsttrees)
            {
                DateTime? begin = GetBegintime(tree);
                if (begin == null)
                {
                    return null;
                }
                if (dt == null || begin < dt)
                {
                    dt = begin;
                }
            }
            return dt;
        }

        public DateTime? GetRealEndTime(ZLObject zl)
        {
            var trees = TreeObjectCellection.GetChildTrees(zl);
            var lasttrees = new List<TreeObject>();
            foreach (var tree in trees)
            {
                var next = TreeObjectCellection.GetNextTree(tree);
                if (next == null)
                {
                    lasttrees.Add(tree);
                }
            }

            if (lasttrees.Count == 0)
            {
                return null;
            }

            DateTime? dt = null;
            foreach (var tree in lasttrees)
            {
                DateTime? end = GetUsableBeginForNext(tree);
                if (end == null)
                {
                    return null;
                }
                if (dt == null || end > dt)
                {
                    dt = end;
                }
            }
            return dt;
        }

        public string GetZLErrMsg(ZLObject zl)
        {
            var trees = TreeObjectCellection.GetChildTrees(zl);
            if (trees.Count <= 0)
            {
                return "没有工艺";
            }
            foreach (var tree in trees)
            {
                string submsg = GetTreeErrMsg(tree);
                if (!string.IsNullOrEmpty(submsg))
                {
                    return submsg;
                }
            }
            return string.Empty;
        }

        public List<TreeObject> GetSortTrees(ZLObject zl)
        {
            var trees = TreeObjectCellection.GetChildTrees(zl);
            List<TreeObject> rslt = new List<TreeObject>();
            foreach (var tree in trees)
            {
                int i = 0;
                for (i = 0; i < rslt.Count; i++)
                {
                    if (IsPreOf(tree, rslt[i]))
                    {
                        break;
                    }
                }
                rslt.Insert(i, tree);
            }
            return rslt;
        }

        public List<TreeObject> GetLastTrees(ZLObject zl)
        {
            var all = TreeObjectCellection.GetChildTrees(zl);
            var rslt = new List<TreeObject>();
            foreach (var tree in all)
            {
                var next = TreeObjectCellection.GetNextTree(tree);
                if (next == null)
                {
                    rslt.Add(tree);
                }
            }
            return rslt;
        }
        #endregion

        #region 主计划方法
        public DateTime? GetBegin(MLObject ml)
        {
            DateTime? rslt = null;

            var firstrqtrees = new List<RqMtrlTreeObject>();
            foreach (var rqtree in RqMtrlTreeObjectCollection.GetRqs(ml._ID))
            {
                var pretrees = RqMtrlTreeObjectCollection.GetChildren(rqtree);
                if (pretrees.Count == 0)
                {
                    firstrqtrees.Add(rqtree);
                }
            }

            foreach (var rqtree in firstrqtrees)
            {
                DateTime? rqtreebegin = rqtree.Begin;
                if (rqtreebegin == null)
                {
                    return null;
                }
                if (rslt == null || rqtreebegin < rslt)
                {
                    rslt = rqtreebegin;
                }
            }
            return rslt;
        }

        public DateTime? GetFirstBegin(MLObject ml, ref TreeObject otree)
        {
            otree = null;
            DateTime? dt = null;
            var zls = ZLObjectCollection.GetChildZLs(ml);
            foreach (var zl in zls)
            {
                TreeObject tree = null;
                var zlbegin = this.GetFirstBegin(zl, ref tree);
                if (zlbegin == null)
                {
                    zlbegin = zl.Begin;
                }
                if (dt == null || zlbegin < dt)
                {
                    dt = zlbegin;
                    if (tree != null)
                    {
                        otree = tree;
                    }
                }
            }
            return dt;
        }

        public DateTime? GetFirstBegin(ZLObject zl, ref TreeObject otree)
        {
            otree = null;

            var trees = TreeObjectCellection.GetChildTrees(zl);
            var firsttrees = new List<TreeObject>();
            foreach (var tree in trees)
            {
                var pre = TreeObjectCellection.GetPreTrees(tree);
                if (pre.Count == 0)
                {
                    firsttrees.Add(tree);
                }
            }
            if (firsttrees.Count == 0)
            {
                return null;
            }
            DateTime? dt = null;
            foreach (var tree in firsttrees)
            {
                DateTime? begin = GetBegintime(tree);
                if (begin == null)
                {
                    return null;
                }
                if (dt == null || begin < dt)
                {
                    dt = begin;
                    otree = tree;
                }
            }
            return dt;
        }

        public DateTime? GetZLBegin(MLObject ml)
        {
            DateTime? rslt = null;
            var zls = ZLObjectCollection.GetChildZLs(ml);
            foreach (var zl in zls)
            {
                var zlbegin = this.GetBegintime(zl);
                if (zlbegin == null)
                {
                    zlbegin = zl.Begin;
                }
                if (rslt == null || zlbegin < rslt)
                {
                    rslt = zlbegin;
                }
            }
            return rslt;
        }

        public DateTime? GetEnd(MLObject ml)
        {
            DateTime? rslt = null;

            var lastRqtrees = new List<RqMtrlTreeObject>();
            foreach (var rqtree in RqMtrlTreeObjectCollection.GetRqs(ml._ID))
            {
                var next = RqMtrlTreeObjectCollection.GetParent(rqtree);
                if (next == null)
                {
                    lastRqtrees.Add(rqtree);
                }
            }

            foreach (var rqtree in lastRqtrees)
            {
                DateTime? rqtreeend = rqtree.End;
                if (rqtreeend == null)
                {
                    return null;
                }
                if (rslt == null || rqtreeend > rslt)
                {
                    rslt = rqtreeend;
                }
            }
            return rslt;
        }

        public double GetTotalHours(MLObject ml)
        {
            double rslt = 0;
            var zls = ZLObjectCollection.GetChildZLs(ml);
            foreach (var zl in zls)
            {
                var trees = TreeObjectCellection.GetChildTrees(zl);
                foreach (var tree in trees)
                {
                    rslt += tree.workhour * tree.qty + tree.beforehour + tree.afterhour;
                }
            }
            return rslt;
        }

        public List<RqMtrlTreeObject> GetSortRqTrees(MLObject ml)
        {
            var rqtrees = RqMtrlTreeObjectCollection.GetRqs(ml._ID);
            List<RqMtrlTreeObject> rslt = new List<RqMtrlTreeObject>();
            foreach (var rqtree in rqtrees)
            {
                int i = 0;
                for (i = 0; i < rslt.Count; i++)
                {
                    if (IsPreOf(rqtree, rslt[i]))
                    {
                        break;
                    }
                }
                rslt.Insert(i, rqtree);
            }
            return rslt;
        }

        public List<RqMtrlTreeObject> GetLastRqTrees(MLObject ml)
        {
            var all = RqMtrlTreeObjectCollection.GetRqs(ml._ID);
            var rslt = new List<RqMtrlTreeObject>();
            foreach (var rq in all)
            {
                var next = RqMtrlTreeObjectCollection.GetParent(rq);
                if (next == null)
                {
                    rslt.Add(rq);
                }
            }
            return rslt;
        }
        #endregion

        #region 运算结果方法
        public bool IsPreOf(RqMtrlTreeObject curtree, RqMtrlTreeObject tree)
        {
            var nexttree = curtree;
            while ((nexttree = RqMtrlTreeObjectCollection.GetParent(nexttree)) != null)
            {
                if (tree == nexttree)
                {
                    return true;
                }
            }
            return false;
        }

        public DateTime? GetUsableBegin(RqMtrlTreeObject rqtree)
        {
            DateTime? rslt = null;
            var pres = RqMtrlTreeObjectCollection.GetChildren(rqtree);
            if (pres.Count == 0)
            {
                return UsableBegin;
            }
            foreach (var pre in pres)
            {
                DateTime? useblebegin = pre.End;
                if (useblebegin == null)
                {
                    return null;
                }
                if (!pre.Enought)
                {
                    useblebegin = useblebegin.Value.AddDays(pre.readydays);
                }
                if (rslt == null || useblebegin > rslt)
                {
                    rslt = useblebegin;
                }
            }
            return rslt;
        }

        public DateTime? GetUsableEnd(RqMtrlTreeObject rqtree)
        {
            DateTime? rslt = null;
            var next = RqMtrlTreeObjectCollection.GetParent(rqtree);
            if (next == null)
            {
                return rqtree.End;
            }
            if (next.Begin == null)
            {
                return null;
            }
            rslt = next.Begin;
            if (!rqtree.Enought)
            {
                rslt = rslt.Value.AddDays(-rqtree.readydays);
            }
            return rslt;
        }

        #endregion


        #region 资源分类
        public IList<ResourceObject> GetResourceForWrkgrp(WrkgrpObject wrkgrp)
        {
            // 获取人资源
            var rslt = new List<ResourceObject>();

            var empRsType = ResourceTypeObjectCollection.GetEmpRs(wrkgrp);
            var maps = ResourceMapCollection.GetMapsForType(empRsType);
            foreach (var map in maps)
            {
                var obj = ResourceObjectCollection.GetByID(map.Resource_ID);
                if (obj != null)
                {
                    rslt.Add(obj);
                }
            }

            // 获取设备资源
            var EqRses = ResourceTypeObjectCollection.GetEqRs(wrkgrp);
            maps = ResourceMapCollection.GetMapsForType(EqRses);
            foreach (var map in maps)
            {
                var obj = ResourceObjectCollection.GetByID(map.Resource_ID);
                if (obj != null)
                {
                    rslt.Add(obj);
                }
            }

            return rslt;
        }

        public IList<ResourceObject> GetEqsForWrkgrp(WrkgrpObject wrkgrp)
        {
            var rslt = new List<ResourceObject>();

            // 获取设备资源
            var EqRses = ResourceTypeObjectCollection.GetEqRs(wrkgrp);
            var maps = ResourceMapCollection.GetMapsForType(EqRses);
            foreach (var map in maps)
            {
                var obj = ResourceObjectCollection.GetByID(map.Resource_ID);
                if (obj != null)
                {
                    rslt.Add(obj);
                }
            }

            return rslt;
        }


        public IList<ResourceObject> GetEmpForWorkgroup(WorkgroupObject workgroup)
        {
            // 获取人资源
            var rslt = new List<ResourceObject>();

            var empRsType = ResourceTypeObjectCollection.GetEmpRs(workgroup);
            var maps = ResourceMapCollection.GetMapsForType(empRsType);
            foreach (var map in maps)
            {
                var obj = ResourceObjectCollection.GetByID(map.Resource_ID);
                if (obj != null)
                {
                    rslt.Add(obj);
                }
            }

            return rslt;
        }

        #endregion

        #region 工组班次统计
        // 统计工组日历的产能占用情况
        public void GetUsedHour(WrkgrpObject wrkgrp, DateTime begin, DateTime end, ref double totalhour, ref double usedhour)
        {
            var RsType = ResourceTypeObjectCollection.GetEmpRs(wrkgrp); // 人力
            GetUsedHour(RsType, begin, end, ref totalhour, ref usedhour);
        }

        public void GetUsedHour(ResourceTypeObject RsType, DateTime begin, DateTime end, ref double totalhour, ref double usedhour)
        {
            totalhour = 0;
            usedhour = 0;
            var empmaps = ResourceMapCollection.GetMapsForType(RsType); // 关系
            foreach (var empmap in empmaps)
            {
                var empobj = ResourceObjectCollection.GetByID(empmap.Resource_ID);
                var empDates = ResourceDateCollection.GetDates(empobj, begin.Date); // 当天日历
                foreach (var empdate in empDates)
                {
                    if (empdate.begintime >= begin && empdate.begintime < end) // 时间范围内的日历
                    {
                        totalhour += empdate.workhour;// 当前日历安排的任务
                        usedhour += GetUsedHour(empdate);
                    }
                }
            }
        }

        public void GetWrkgrpTreeDateInfo(WrkgrpObject wrkgrp, DateTime begin, DateTime end, Dictionary<string, double> _tree_hours, Dictionary<string, double> _tree_qtys)
        {
            Dictionary<string, Dictionary<string, string>> _tree_group_obj = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, Dictionary<string, double>> _tree_group_hours = new Dictionary<string, Dictionary<string, double>>();
            Dictionary<string, Dictionary<string, bool>> _tree_group_begin = new Dictionary<string, Dictionary<string, bool>>();
            Dictionary<string, Dictionary<string, bool>> _tree_group_end = new Dictionary<string, Dictionary<string, bool>>();

            var RsType = ResourceTypeObjectCollection.GetEmpRs(wrkgrp); // 人力
            var empmaps = ResourceMapCollection.GetMapsForType(RsType); // 关系
            foreach (var empmap in empmaps) 
            {
                var empobj = ResourceObjectCollection.GetByID(empmap.Resource_ID);
                for (DateTime dt = begin.Date; dt < end; dt = dt.AddDays(1))
                {
                    var empDates = ResourceDateCollection.GetDates(empobj, dt); // 当天日历
                    foreach (var empdate in empDates)
                    {
                        if (empdate.begintime >= begin && empdate.begintime < end) // 时间范围内的日历
                        {
                            var treeDates = TreeDateCollection.GetByDate(empdate);
                            foreach (var treedate in treeDates)
                            {
                                if (!_tree_hours.ContainsKey(treedate.Tree_ID))
                                {
                                    _tree_hours[treedate.Tree_ID] = treedate.workhour;
                                }
                                else
                                {
                                    _tree_hours[treedate.Tree_ID] += treedate.workhour;
                                }

                                if (!_tree_group_obj.ContainsKey(treedate.Tree_ID))
                                {
                                    _tree_group_obj[treedate.Tree_ID] = new Dictionary<string, string>();
                                }
                                if (!_tree_group_obj[treedate.Tree_ID].ContainsKey(treedate.ResourceGroup))
                                {
                                    _tree_group_obj[treedate.Tree_ID][treedate.ResourceGroup] = treedate.Resource_ID;
                                }
                                if (treedate.Resource_ID != _tree_group_obj[treedate.Tree_ID][treedate.ResourceGroup])
                                {
                                    continue;
                                }
                                // DONE: 统计资源在时间范围内是否含开始或含结束，总时长
                                if (!_tree_group_hours.ContainsKey(treedate.Tree_ID))
                                {
                                    _tree_group_hours[treedate.Tree_ID] = new Dictionary<string, double>();
                                }
                                if (!_tree_group_hours[treedate.Tree_ID].ContainsKey(treedate.ResourceGroup))
                                {
                                    _tree_group_hours[treedate.Tree_ID][treedate.ResourceGroup] = 0;
                                }
                                _tree_group_hours[treedate.Tree_ID][treedate.ResourceGroup] += treedate.workhour;

                                var tree = TreeObjectCellection.GetByID(treedate.Tree_ID);
                                if (!_tree_group_begin.ContainsKey(treedate.Tree_ID))
                                {
                                    _tree_group_begin[treedate.Tree_ID] = new Dictionary<string, bool>();
                                }
                                if (!_tree_group_begin[treedate.Tree_ID].ContainsKey(treedate.ResourceGroup))
                                {
                                    DateTime? treebegin = TreeDateCollection.GetGroupFirstBegin(tree, treedate.ResourceGroup);
                                    _tree_group_begin[treedate.Tree_ID][treedate.ResourceGroup] = (treebegin != null && treebegin >= treedate.begin);
                                }

                                if (!_tree_group_end.ContainsKey(treedate.Tree_ID))
                                {
                                    _tree_group_end[treedate.Tree_ID] = new Dictionary<string, bool>();
                                }
                                if (!_tree_group_end[treedate.Tree_ID].ContainsKey(treedate.ResourceGroup))
                                {
                                    _tree_group_end[treedate.Tree_ID][treedate.ResourceGroup] = false;
                                }
                                DateTime? treeend = TreeDateCollection.GetGroupLastEnd(tree, treedate.ResourceGroup);
                                if (treeend != null && treedate.end >= treeend)
                                {
                                    _tree_group_end[treedate.Tree_ID][treedate.ResourceGroup] = true;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var kvp1 in _tree_group_hours)
            {
                foreach (var kvp2 in kvp1.Value)
                {
                    var tree = TreeObjectCellection.GetByID(kvp1.Key);
                    double workhour = kvp2.Value;
                    if (_tree_group_begin[kvp1.Key][kvp2.Key])
                    {
                        workhour -= tree.beforehour;
                    }
                    if (_tree_group_end[kvp1.Key][kvp2.Key])
                    {
                        workhour -= tree.afterhour;
                    }
                    double qty = Math.Round(tree.workhour > 0 ? workhour / tree.workhour : 0);
                    if (!_tree_qtys.ContainsKey(kvp1.Key))
                    {
                        _tree_qtys[kvp1.Key] = qty;
                    }
                    else
                    {
                        _tree_qtys[kvp1.Key] += qty;
                    }
                }
            }
        }
        #endregion

        #region 准备无效的方法

        // 为派工查询日期，修改WrkgrpDate_ID与WorkgroupDate_ID
        public void FindDate(TechObject tech)
        {
            var tree = TreeObjectCellection.GetParent(tech);
            tech.Wrkgrp_ID = tree.Wrkgrp_ID;
            tech.Workgroup_ID = tree.Workgroup_ID;

            var wrkgrp = WrkgrpObjectCollection.GetByID(tech.Wrkgrp_ID);
            if (wrkgrp.tasktype == Tasktype.Wrkgrp)
            {
                if (tech.begindate == null)
                {
                    tech.WorkgroupDate_ID = string.Empty;
                    tech.WrkgrpDate_ID = string.Empty;
                }
                else
                {
                    WrkgrpDateObject realdate = null;
                    DateTime dt = tech.begindate.Value.Date;
                    int cnt = 0;
                    while (realdate == null)
                    {
                        var dates = WrkgrpDateObjectCollection.GetDates(wrkgrp, dt);
                        if (dates.Count > 0)
                        {
                            foreach (var date in dates)
                            {
                                if (date.endtime > tech.begindate)
                                {
                                    realdate = date;
                                    break;
                                }
                            }
                            cnt = 0;
                        }
                        else
                        {
                            cnt++;
                            if (cnt > 100)
                            {
                                break;
                            }
                        }
                        dt = dt.AddDays(1);
                    }

                    if (realdate != null)
                    {
                        tech.WrkgrpDate_ID = realdate._ID;
                    }
                    else
                    {
                        tech.WrkgrpDate_ID = string.Empty;
                    }
                    tech.WorkgroupDate_ID = string.Empty;
                }
            }
            else
            {
                var workgroup = WorkgroupObjectCollection.GetByID(tech.Workgroup_ID);
                if (workgroup != null)
                {
                    if (tech.begindate == null)
                    {
                        tech.WorkgroupDate_ID = string.Empty;
                        tech.WrkgrpDate_ID = string.Empty;
                    }
                    else
                    {
                        WorkgroupDateObject realdate = null;
                        DateTime dt = tech.begindate.Value.Date;
                        int cnt = 0;
                        while (realdate == null)
                        {
                            var dates = WorkgroupDateObjectCollection.GetDates(workgroup, dt);
                            if (dates.Count > 0)
                            {
                                foreach (var date in dates)
                                {
                                    if (date.endtime > tech.begindate)
                                    {
                                        realdate = date;
                                        break;
                                    }
                                }
                                cnt = 0;
                            }
                            else
                            {
                                cnt++;
                                if (cnt > 100)
                                {
                                    break;
                                }
                            }
                            dt = dt.AddDays(1);
                        }

                        if (realdate != null)
                        {
                            tech.WorkgroupDate_ID = realdate._ID;
                        }
                        else
                        {
                            tech.WorkgroupDate_ID = string.Empty;
                        }
                        tech.WrkgrpDate_ID = string.Empty;
                    }
                }
                else
                {
                    tech.WorkgroupDate_ID = string.Empty;
                    tech.WrkgrpDate_ID = string.Empty;
                }
            }
        }

        // 重新计算所有派工的日历
        public void CmplDate()
        {
            var changetechs = new List<TechObject>();
            var org_wrkgrpdate_ids = new List<string>();
            var org_workgroupdate_ids = new List<string>();
            foreach (var tech in TechObjectCellection)
            {
                string org_wrkgrpdate_id = tech.WrkgrpDate_ID;
                string org_workgroupdate_id = tech.WorkgroupDate_ID;
                FindDate(tech);
                if (org_wrkgrpdate_id != tech.WrkgrpDate_ID || org_workgroupdate_id != tech.WorkgroupDate_ID)
                {
                    changetechs.Add(tech);
                    org_wrkgrpdate_ids.Add(org_wrkgrpdate_id);
                    org_workgroupdate_ids.Add(org_workgroupdate_id);
                }
            }

            for (int i = 0; i < changetechs.Count; i++)
            {
                var tech = changetechs[i];
                string new_wrkgrpdate_id = tech.WrkgrpDate_ID;
                string new_workgroupdate_id = tech.WorkgroupDate_ID;
                tech.WrkgrpDate_ID = org_wrkgrpdate_ids[i];
                tech.WorkgroupDate_ID = org_workgroupdate_ids[i];
                TechObjectCellection.Remove(tech);
                tech.WrkgrpDate_ID = new_wrkgrpdate_id;
                tech.WorkgroupDate_ID = new_workgroupdate_id;
                TechObjectCellection.Add(tech);
            }
        }

        // 获取工组日历的派工时间不包含原已用时间
        public double GetUsedHour(WrkgrpDateObject wrkgrpdate)
        {
            var techs = TechObjectCellection.GetTechs(wrkgrpdate);
            double hours = 0;
            foreach (var tech in techs)
            {
                hours += GetEmpHour(tech);
            }
            return hours;
        }

        // 获取工作中心日历的派工时间不包含原已用时间
        public double GetUsedHour(WorkgroupDateObject workgroupdate)
        {
            var techs = TechObjectCellection.GetTechs(workgroupdate);
            double hours = 0;
            foreach (var tech in techs)
            {
                hours += GetEmpHour(tech);
            }
            return hours;
        }

        public double GetUsedEqHour(EquipmentDateObject eqdate)
        {
            IEnumerable<TechObject> techs = null;
            if (eqdate.tasktype == Tasktype.Wrkgrp)
            {
                var wrkgrpdate = WrkgrpDateObjectCollection.GetByID(eqdate.WrkgrpDate_ID);
                techs = TechObjectCellection.GetTechs(wrkgrpdate);
            }
            else
            {
                var workgroupdate = WorkgroupDateObjectCollection.GetByID(eqdate.WorkgroupDate_ID);
                techs = TechObjectCellection.GetTechs(workgroupdate);
            }

            var eq = EquipmentObjectCollection.GetByID(eqdate.Equipment_ID);

            double hours = 0;
            foreach (var tech in techs)
            {
                hours += GetEqHour(eq, tech);
            }
            return hours;
        }

        // 获取派工的时间
        public double GetEmpHour(TechObject tech)
        {
            var tree = TreeObjectCellection.GetParent(tech);
            var techs = TechObjectCellection.GetTechs(tree);
            double emphour = tree.workhour * tech.qty;
            if (tech == techs[0])
            {
                emphour += tree.beforehour;
            }
            if (tech == techs[techs.Count - 1])
            {
                emphour += tree.afterhour;
            }
            return emphour;
        }

        public double GetEqHour(EquipmentObject eq, TechObject tech)
        {
            var tree = TreeObjectCellection.GetParent(tech);
            string treeEq_ID = ToTreeEq_ID(tree, eq.typeid);
            var treeEq = TreeEqObjectCollection.GetByID(treeEq_ID);
            if (treeEq == null)
            {
                return 0;
            }
            var techs = TechObjectCellection.GetTechs(tree);
            double eqhour = treeEq.eqworkhour * tech.qty;
            if (tech == techs[0])
            {
                eqhour += tree.beforehour;
            }
            if (tech == techs[techs.Count - 1])
            {
                eqhour += tree.afterhour;
            }
            return eqhour;
        }

        public string ToTreeEq_ID(TreeObject tree, int typeid)
        {
            return "treeeq:" + tree._ID + "," + typeid;
        }

        // UNDONE: 排除主计划的已占用时间
        public void ExcludeDate(MLObject ml)
        {
            foreach (var zl in ZLObjectCollection.GetChildZLs(ml))
            {
                foreach (var tree in TreeObjectCellection.GetChildTrees(zl))
                {
                    foreach (var tech in TechObjectCellection.GetTechs(tree))
                    {
                        var wrkgrp = WrkgrpObjectCollection.GetByID(tech.Wrkgrp_ID);
                        if (wrkgrp == null)
                        {
                        }
                        if (wrkgrp.tasktype == Tasktype.Wrkgrp)
                        {
                            var wrkgrpdate = WrkgrpDateObjectCollection.GetByID(tech.WrkgrpDate_ID);
                        }
                    }
                }
            }
        }

        #endregion


        internal DateTime? GetBegintime(RqMtrlTreeObject rqtree)
        {
            var zls = ZLObjectCollection.GetZls(rqtree.ML_ID, rqtree.Mtrl_ID);
            DateTime? rslt = null;
            foreach (var zl in zls)
            {
                if (zl.Begin == null)
                {
                    return null;
                }
                if (rslt == null || zl.Begin < rslt)
                {
                    rslt = zl.Begin;
                }
            }
            return rslt;
        }

        internal DateTime? GetEndTime(RqMtrlTreeObject rqtree)
        {
            var zls = ZLObjectCollection.GetZls(rqtree.ML_ID, rqtree.Mtrl_ID);
            DateTime? rslt = null;
            foreach (var zl in zls)
            {
                if (zl.End == null)
                {
                    return null;
                }
                if (rslt == null || zl.End > rslt)
                {
                    rslt = zl.End;
                }
            }
            return rslt;
        }

        public DateTime? GetZZBegin(IEnumerable<ZLObject> zls)
        {
            DateTime? rslt = null;
            foreach (var zl in zls)
            {
                var zzbegin = GetZZBegin(zl);
                if (rslt == null || zzbegin < rslt)
                {
                    rslt = zzbegin;
                }
            }
            return rslt;
        }

        public DateTime? GetZZBegin(ZLObject zl)
        {
            DateTime? rslt = null;
            DateTime? min = null;
            var trees = TreeObjectCellection.GetChildTrees(zl);
            foreach (var tree in trees)
            {
                var dt = GetBegintime(tree);
                if (min == null || dt < min)
                {
                    min = dt;
                }
                if (tree.beginMode == beginmode.all)
                {
                    if (rslt == null || dt < rslt)
                    {
                        rslt = dt;
                    }
                }
            }
            if (rslt == null)
            {
                rslt = min;
            }
            return rslt;
        }
    }
}
