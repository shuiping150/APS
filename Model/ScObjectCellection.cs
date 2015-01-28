
namespace APSV1.Model
{
    public class ScObjectCellection : DBObjectCollection<ScObject>
    {
        public ScObject GetParent(WkpObject wkp)
        {
            return GetByID(wkp.Sc_ID);
        }

        public string ToSC_ID(int scid)
        {
            return "sc:" + scid;
        }
    }
}
