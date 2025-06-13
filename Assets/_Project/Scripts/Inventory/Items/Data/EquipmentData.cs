using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "EquipmentData", menuName = "Inventory/EquipmentData", order = 0)]
    public class EquipmentData : ItemData
    {
        #region FIELDS
        public float _health = 5;
        public float _defense = 2;
        #endregion

        #region PROPERTIES
        public new ItemType Type => ItemType.Equipment;
        #endregion
    }
}
