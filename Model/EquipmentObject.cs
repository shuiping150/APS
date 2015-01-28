
namespace APSV1.Model
{
    public class EquipmentObject : DBObject
    {
        public int typeid { get; set; }
        public string typecode { get; set; }
        public string typename { get; set; }
        public Eqtype eqtype { get; set; }
    }
}
