
namespace APSV1.Model
{
    public class MLObjectCollection : DBObjectCollection<MLObject>
    {
        public MLObject GetParent(ZLObject zl)
        {
            return GetByID(zl.ML_ID);
        }
    }
}
