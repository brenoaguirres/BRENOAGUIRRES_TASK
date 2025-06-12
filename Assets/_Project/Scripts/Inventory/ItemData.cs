using UnityEditor;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public enum ItemType
        {
            Consumable,
            Equipment,
            Material,
            QuestItem
        }

        public int id = 0;
        public Sprite icon;
        public string itemName = "New Item";
        public ItemType itemType = ItemType.Consumable;
    }
}