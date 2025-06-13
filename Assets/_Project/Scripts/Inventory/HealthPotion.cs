using Inventory;
using Player;
using UnityEngine;

public class HealthPotion : ConsumableItem
{
    #region CUSTOM METHODS
    public override void Use(PlayerStats stats, InventoryItem item)
    {
        ConsumableData consumableData = item.ItemData as ConsumableData;
        if (consumableData == null)
        {
            Debug.LogError("Item is not an ConsumableData type.");
            return;
        }

        stats.Health = consumableData._healthAmount;
    }
    #endregion
}
