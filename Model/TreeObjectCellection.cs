using System.Collections.Generic;

namespace APSV1.Model
{
    public class TreeObjectCellection : DBObjectCollection<TreeObject>
    {
        private Dictionary<string, DBObjectCollection<TreeObject>> _zl_trees = new Dictionary<string, DBObjectCollection<TreeObject>>();

        private Dictionary<string, DBObjectCollection<TreeObject>> _tree_pres = new Dictionary<string, DBObjectCollection<TreeObject>>();

        public override void Add(TreeObject item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
                if (!_zl_trees.ContainsKey(item.ZL_ID))
                {
                    _zl_trees[item.ZL_ID] = new DBObjectCollection<TreeObject>();
                }
                _zl_trees[item.ZL_ID].Add(item);
                if (!_tree_pres.ContainsKey(item.Next_ID))
                {
                    _tree_pres[item.Next_ID] = new DBObjectCollection<TreeObject>();
                }
                _tree_pres[item.Next_ID].Add(item);
            }
        }

        public override bool Remove(TreeObject item)
        {
            if (this.Contains(item))
            {
                if (_zl_trees.ContainsKey(item.ZL_ID) && _zl_trees[item.ZL_ID].Contains(item))
                {
                    _zl_trees[item.ZL_ID].Remove(item);
                }
                if (_tree_pres.ContainsKey(item.Next_ID) && _tree_pres[item.Next_ID].Contains(item))
                {
                    _tree_pres[item.Next_ID].Remove(item);
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
            _zl_trees.Clear();
            _tree_pres.Clear();
            base.Clear();
        }

        public List<TreeObject> GetChildTrees(ZLObject zl)
        {
            List<TreeObject> rslt = new List<TreeObject>();
            if (_zl_trees.ContainsKey(zl._ID))
            {
                rslt.AddRange(_zl_trees[zl._ID]);
            }
            return rslt;
        }

        public TreeObject GetNextTree(TreeObject tree)
        {
            return GetByID(tree.Next_ID);
        }

        public DBObjectCollection<TreeObject> GetPreTrees(TreeObject tree)
        {
            if (_tree_pres.ContainsKey(tree._ID))
            {
                return _tree_pres[tree._ID];
            }
            else
            {
                return new DBObjectCollection<TreeObject>();
            }
        }

        public TreeObject GetParent(TechObject tech)
        {
            return GetByID(tech.Tree_ID);
        }

        public TreeObject GetTree(TreeEqObject treeEq)
        {
            return GetByID(treeEq.Tree_ID);
        }
    }
}
