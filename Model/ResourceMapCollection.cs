using System.Collections.Generic;

namespace APSV1.Model
{
    // 资源类与资源关系，多对多关系
    public class ResourceMapCollection : DBObjectCollection<ResourceMap>
    {
        private Dictionary<string, DBObjectCollection<ResourceMap>> _type_Resources = new Dictionary<string, DBObjectCollection<ResourceMap>>();

        public override void Add(ResourceMap item)
        {
            if (!Contains(item))
            {
                base.Add(item);
                if (!_type_Resources.ContainsKey(item.ResourceType_ID))
                {
                    _type_Resources[item.ResourceType_ID] = new DBObjectCollection<ResourceMap>();
                }
                _type_Resources[item.ResourceType_ID].Add(item);
            }
        }

        public override bool Remove(ResourceMap item)
        {
            if (Contains(item))
            {
                if (_type_Resources.ContainsKey(item.ResourceType_ID) && _type_Resources[item.ResourceType_ID].Contains(item))
                {
                    _type_Resources[item.ResourceType_ID].Remove(item);
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
            _type_Resources.Clear();
            base.Clear();
        }

        public ResourceMap AddMap(ResourceTypeObject type, ResourceObject resource)
        {
            string _ID = ToMapID(type, resource);
            var rslt = GetByID(_ID);
            if (rslt == null)
            {
                rslt = new ResourceMap();
                rslt._ID = _ID;
                rslt.ResourceType_ID = type._ID;
                rslt.Resource_ID = resource._ID;
                Add(rslt);
            }
            return rslt;
        }

        public string ToMapID(ResourceTypeObject type, ResourceObject resource)
        {
            return "map:" + type._ID + "," + resource._ID;
        }

        public List<ResourceMap> GetMapsForType(ResourceTypeObject type)
        {
            List<ResourceMap> rslt = new List<ResourceMap>();
            if (_type_Resources.ContainsKey(type._ID))
            {
                rslt.AddRange(_type_Resources[type._ID]);
            }
            return rslt;
        }

        public bool IsMap(string Type_ID, string Resource_ID)
        {
            if (_type_Resources.ContainsKey(Type_ID))
            {
                foreach (var map in _type_Resources[Type_ID])
                {
                    if (map.Resource_ID == Resource_ID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
