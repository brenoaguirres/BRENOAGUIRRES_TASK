using Inventory;
using UnityEngine;
using static Inventory.ItemData;

namespace Inventory
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Inventory/WeaponData", order = 0)]
    public class WeaponData : ItemData
    {
        #region FIELDS
        public float _atkDamage = 10;
        public float _atkSpeed = 10;
        #endregion

        #region PROPERTIES
        public new ItemType Type => ItemType.Weapon;
        #endregion
    }
}