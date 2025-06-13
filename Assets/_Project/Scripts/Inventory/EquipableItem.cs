using Player;
using UnityEngine;

namespace Inventory
{
    public class EquipableItem : MonoBehaviour
    {
        #region CUSTOM METHODS
        public virtual void Equip(PlayerStats stats, InventoryItem item)
        {
            EquipmentData equipmentData = item.ItemData as EquipmentData;
            if (equipmentData == null)
            {
                Debug.LogError("Item is not an EquipmentData type.");
                return;
            }

            stats.EquippedArmor = equipmentData;
        }

        public virtual void Unequip(PlayerStats stats)
        {
            if (stats.EquippedArmor == null)
            {
                Debug.LogError("No armor is currently equipped.");
                return;
            }
            stats.EquippedArmor = null;
        }
        #endregion
    }
}