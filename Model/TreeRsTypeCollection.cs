using System.Collections.Generic;

namespace APSV1.Model
{
    // 工艺所需要资源类
    public class TreeRsTypeCollection : DBObjectCollection<TreeRsType>
    {
        // 按工艺查询
        private Dictionary<string, DBObjectCollection<TreeRsType>> _tree_RsTypes = new Dictionary<string, DBObjectCollection<TreeRsType>>();

        public override void Add(TreeRsType item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_tree_RsTypes.ContainsKey(item.Tree_ID))
                {
                    _tree_RsTypes[item.Tree_ID] = new DBObjectCollection<TreeRsType>();
                }
                _tree_RsTypes[item.Tree_ID].Add(item);
            }
        }

        public override bool Remove(TreeRsType item)
        {
            if (Contains(item))
            {
                if (_tree_RsTypes.ContainsKey(item.Tree_ID) && _tree_RsTypes[item.Tree_ID].Contains(item))
                {
                    _tree_RsTypes[item.Tree_ID].Remove(item);
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
            _tree_RsTypes.Clear();
            base.Clear();
        }

        // 给工艺添加资源类
        public TreeRsType AddTreeRsType(TreeObject tree, ResourceTypeObject ResourceType, int num)
        {
            string _ID = ToID(tree, ResourceType);
            var rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new TreeRsType();
                rslt._ID = _ID;
                rslt.Tree_ID = tree._ID;
                rslt.ResourceType_ID = ResourceType._ID;
                rslt.num = num;
                Add(rslt);
            }
            return rslt;
        }

        public string ToID(TreeObject tree, ResourceTypeObject ResourceType)
        {
            return "treeRs:" + tree._ID + "," + ResourceType._ID;
        }

        // 工艺下的资源类需求
        public List<TreeRsType> GetResources(TreeObject tree)
        {
            List<TreeRsType> rslt = new List<TreeRsType>();
            if (_tree_RsTypes.ContainsKey(tree._ID))
            {
                rslt.AddRange(_tree_RsTypes[tree._ID]);
            }
            return rslt;
        }
    }
}
