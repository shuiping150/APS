
namespace APSV1.Model
{
    public class EquipmentObjectCollection : DBObjectCollection<EquipmentObject>
    {
        public EquipmentObject GetEquipment(EquipmentDateObject equipmentDate)
        {
            return GetByID(equipmentDate.Equipment_ID);
        }

        public string ToEq_ID(int typeid)
        {
            return "eq:" + typeid;
        }
    }
}
