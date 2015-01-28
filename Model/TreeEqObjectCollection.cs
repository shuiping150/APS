using System.Collections.Generic;

namespace APSV1.Model
{
    public class TreeEqObjectCollection : DBObjectCollection<TreeEqObject>
    {
        private Dictionary<string, DBObjectCollection<TreeEqObject>> _tree_eqs = new Dictionary<string, DBObjectCollection<TreeEqObject>>();

        public override void Add(TreeEqObject item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_tree_eqs.ContainsKey(item.Tree_ID))
                {
                    _tree_eqs[item.Tree_ID] = new DBObjectCollection<TreeEqObject>();
                }
                _tree_eqs[item.Tree_ID].Add(item);
            }
        }

        public override bool Remove(TreeEqObject item)
        {
            if (Contains(item))
            {
                if (_tree_eqs.ContainsKey(item.Tree_ID) && _tree_eqs[item.Tree_ID].Contains(item))
                {
                    _tree_eqs[item.Tree_ID].Remove(item);
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
            _tree_eqs.Clear();
            base.Clear();
        }

        public List<TreeEqObject> GetEqs(TreeObject tree)
        {
            List<TreeEqObject> rslt = new List<TreeEqObject>();
            if (_tree_eqs.ContainsKey(tree._ID))
            {
                rslt.AddRange(_tree_eqs[tree._ID]);
            }
            return rslt;
        }
    }
}
