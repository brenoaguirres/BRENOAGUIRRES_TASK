using Inventory;
using UnityEngine;
using static Inventory.ItemData;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ConsumableData", menuName = "Inventory/ConsumableData", order = 0)]
    public class ConsumableData : ItemData
    {
        #region FIELDS
        public float _healthAmount = 20;
        #endregion

        #region PROPERTIES
        public new ItemType Type => ItemType.Consumable;
        #endregion
    }
}