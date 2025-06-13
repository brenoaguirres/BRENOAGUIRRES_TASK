using Player;
using UnityEngine;

namespace Inventory
{
    public abstract class ConsumableItem : MonoBehaviour
    {
        #region CUSTOM METHODS
        public abstract void Use(PlayerStats stats, InventoryItem item);
        #endregion
    }
}