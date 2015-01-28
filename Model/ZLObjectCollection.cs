using System.Collections.Generic;

namespace APSV1.Model
{
    public class ZLObjectCollection : DBObjectCollection<ZLObject>
    {
        private Dictionary<string, DBObjectCollection<ZLObject>> _ml_zls = new Dictionary<string, DBObjectCollection<ZLObject>>();

        private Dictionary<string, DBObjectCollection<ZLObject>> _zl_pres = new Dictionary<string, DBObjectCollection<ZLObject>>();

        private Dictionary<string, Dictionary<string, DBObjectCollection<ZLObject>>> _ml_mtrl_zls = new Dictionary<string, Dictionary<string, DBObjectCollection<ZLObject>>>();

        private Dictionary<string, DBObjectCollection<ZLObject>> _wkp_zls = new Dictionary<string, DBObjectCollection<ZLObject>>();

        public override void Add(ZLObject item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
                if (!_ml_zls.ContainsKey(item.ML_ID))
                {
                    _ml_zls[item.ML_ID] = new DBObjectCollection<ZLObject>();
                }
                _ml_zls[item.ML_ID].Add(item);
                foreach (var next_id in item.Next_ID)
                {
                    if (!_zl_pres.ContainsKey(next_id))
                    {
                        _zl_pres[next_id] = new DBObjectCollection<ZLObject>();
                    }
                    _zl_pres[next_id].Add(item);
                }
                if (!_ml_mtrl_zls.ContainsKey(item.ML_ID))
                {
                    _ml_mtrl_zls[item.ML_ID] = new Dictionary<string, DBObjectCollection<ZLObject>>();
                }
                if (!_ml_mtrl_zls[item.ML_ID].ContainsKey(item.Mtrl_ID))
                {
                    _ml_mtrl_zls[item.ML_ID][item.Mtrl_ID] = new DBObjectCollection<ZLObject>();
                }
                _ml_mtrl_zls[item.ML_ID][item.Mtrl_ID].Add(item);
                if (!_wkp_zls.ContainsKey(item.Wkp_ID))
                {
                    _wkp_zls[item.Wkp_ID] = new DBObjectCollection<ZLObject>();
                }
                _wkp_zls[item.Wkp_ID].Add(item);
            }
        }

        public override bool Remove(ZLObject item)
        {
            if (this.Contains(item))
            {
                if (_ml_zls.ContainsKey(item.ML_ID) && _ml_zls[item.ML_ID].Contains(item))
                {
                    _ml_zls[item.ML_ID].Remove(item);
                }
                foreach (var next_id in item.Next_ID)
                {
                    if (_zl_pres.ContainsKey(next_id) && _zl_pres[next_id].Contains(item))
                    {
                        _zl_pres[next_id].Remove(item);
                    }
                }
                if (_ml_mtrl_zls.ContainsKey(item.ML_ID)
                    && _ml_mtrl_zls[item.ML_ID].ContainsKey(item.Mtrl_ID)
                    && _ml_mtrl_zls[item.ML_ID][item.Mtrl_ID].Contains(item))
                {
                    _ml_mtrl_zls[item.ML_ID][item.Mtrl_ID].Remove(item);
                }
                if (_wkp_zls.ContainsKey(item.Wkp_ID) && _wkp_zls[item.Wkp_ID].Contains(item))
                {
                    _wkp_zls[item.Wkp_ID].Remove(item);
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
            _ml_zls.Clear();
            _zl_pres.Clear();
            _ml_mtrl_zls.Clear();
            base.Clear();
        }

        public List<ZLObject> GetChildZLs(MLObject ml)
        {
            List<ZLObject> rslt = new List<ZLObject>();
            if (_ml_zls.ContainsKey(ml._ID))
            {
                rslt.AddRange(_ml_zls[ml._ID]);
            }
            return rslt;
        }

        public List<ZLObject> GetNextZL(ZLObject zl)
        {
            List<ZLObject> rslt = new List<ZLObject>();
            foreach (var next_id in zl.Next_ID)
            {
                rslt.Add(GetByID(next_id));
            }
            return rslt;
        }

        public DBObjectCollection<ZLObject> GetPreZLs(ZLObject zl)
        {
            if (_zl_pres.ContainsKey(zl._ID))
            {
                return _zl_pres[zl._ID];
            }
            else
            {
                return new DBObjectCollection<ZLObject>();
            }
        }

        public ZLObject GetParent(TreeObject tree)
        {
            return GetByID(tree.ZL_ID);
        }

        public List<ZLObject> GetZls(string ML_ID, string Mtrl_ID)
        {
            List<ZLObject> rslt = new List<ZLObject>();
            if (_ml_mtrl_zls.ContainsKey(ML_ID) && _ml_mtrl_zls[ML_ID].ContainsKey(Mtrl_ID))
            {
                foreach (var zl in _ml_mtrl_zls[ML_ID][Mtrl_ID])
                {
                    rslt.Add(zl);
                }
            }
            return rslt;
        }

        public List<ZLObject> GetZlsByWkp(WkpObject wkp)
        {
            List<ZLObject> rslt = new List<ZLObject>();
            if (_wkp_zls.ContainsKey(wkp._ID))
            {
                foreach (var zl in _wkp_zls[wkp._ID])
                {
                    rslt.Add(zl);
                }
            }
            return rslt;
        }
    }
}
