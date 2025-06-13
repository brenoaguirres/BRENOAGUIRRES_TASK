using UnityEngine;

namespace Inventory
{
    public abstract class ItemData : ScriptableObject
    {
        #region ITEMTYPE ENUM
        public enum ItemType
        {
            Consumable,
            Weapon,
            Equipment,
            Material,
            QuestItem,
        }
        #endregion

        #region FIELDS
        public string _id = "I_0";
        public Sprite _icon;
        public string _itemName = "New Item";
        [TextArea(3, 10)]
        public string _itemDescription = "Insert item description here.";
        public GameObject _itemPrefab;
        #endregion

        #region PROPERTIES
        public ItemType Type;
        #endregion
    }
}