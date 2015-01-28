using System.Collections.Generic;

namespace APSV1.Model
{
    public class MtrlObjectCollection : DBObjectCollection<MtrlObject>
    {
        private Dictionary<string, MtrlObject> _mtrls = new Dictionary<string, MtrlObject>();

        public override void Add(MtrlObject item)
        {
            if (!Contains(item))
            {
                _mtrls[item.mtrlcode] = item;
                base.Add(item);
            }
        }

        public override bool Remove(MtrlObject item)
        {
            if (Contains(item))
            {
                _mtrls.Remove(item.mtrlcode);
                return base.Remove(item);
            }
            else
            {
                return false;
            }
        }

        public override void Clear()
        {
            _mtrls.Clear();
            base.Clear();
        }

        public MtrlObject GetByCode(string mtrlcode)
        {
            if (_mtrls.ContainsKey(mtrlcode))
            {
                return _mtrls[mtrlcode];
            }
            else
            {
                return null;
            }
        }
    }
}
