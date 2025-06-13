using Inventory;
using Player;
using UnityEngine;

namespace Inventory
{
    public class WeaponItem : MonoBehaviour
    {
        #region CUSTOM METHODS
        public virtual void Equip(PlayerStats stats, InventoryItem item)
        {
            WeaponData weaponData = item.ItemData as WeaponData;
            if (weaponData == null)
            {
                Debug.LogError("Item is not an WeaponData type.");
                return;
            }

            stats.EquippedWeapon = weaponData;
        }

        public virtual void Unequip(PlayerStats stats)
        {
            if (stats.EquippedWeapon == null)
            {
                Debug.LogError("No weapon is currently equipped.");
                return;
            }
            stats.EquippedWeapon = null;
        }
        #endregion
    }
}