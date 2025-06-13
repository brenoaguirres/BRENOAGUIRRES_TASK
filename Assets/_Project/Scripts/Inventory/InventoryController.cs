using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using UnityEngine.UI;
using Player;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        #region FIELDS
        [Header("Inventory Settings")]
        [SerializeField] private GameObject _inventoryUI;
        [SerializeField] private GameObject _contentBoxUI;
        [SerializeField] private InventoryItem _inventoryItemPrefab;

        [Space]
        [Header("Selection Settings")]
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private TextMeshProUGUI _typeText;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [Space]
        [Header("Menu Actions Settings")]
        [SerializeField] private GameObject _dropSpawnPosition;
        [SerializeField] private PlayerStats _stats;
        [Space]
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _removeButton;
        [SerializeField] private Button _swapButton;

        private List<ItemData> _storedItems = new();
        private List<InventoryItem> _inventoryItems = new();

        private InventoryItem _currentSelection;
        private InventoryItem _swappingSelection;
        #endregion

        #region PROPERTIES
        public InventoryItem CurrentSelection 
        { 
            get => _currentSelection;
            set
            {
                if (value == null)
                {
                    _currentSelection = null;
                    _labelText.text = "Item Name: ";
                    _typeText.text = "Item Category: ";
                    _descriptionText.text = "Item Description: ";
                    return;
                }

                _currentSelection = value;
                _labelText.text = "Item Name: " + value.ItemData._itemName;
                _typeText.text = "Item Category: " + nameof(value.ItemData.Type);
                _descriptionText.text = "Item Description: " + value.ItemData._itemDescription;

                if (_swappingSelection != null)
                {
                    SwapItem();
                    UntagItemForSwap();
                    DeselectAll();
                    return;
                }
            }
        }
        #endregion

        #region UNITY CALLBACKS
        public void Awake()
        {
            Validate();
        }

        public void Start()
        {
            if (_inventoryUI.activeSelf) CloseMenu();

            _useButton.onClick.AddListener(OnUseClick);
            _removeButton.onClick.AddListener(OnDropClick);
            _swapButton.onClick.AddListener(OnSwapClick);
        }

        public void OnDestroy()
        {
            _useButton.onClick.RemoveListener(OnUseClick);
            _removeButton.onClick.RemoveListener(OnDropClick);
            _swapButton.onClick.RemoveListener(OnSwapClick);
        }
        #endregion

        #region CUSTOM METHODS
        private void Validate()
        {
            Assert.IsNotNull(_inventoryUI, "Inventory UI is not assigned in the inspector.");
            Assert.IsNotNull(_contentBoxUI, "Content Box UI is not assigned in the inspector.");
            Assert.IsNotNull(_inventoryItemPrefab, "UI Item Prefab is not assigned in the inspector.");

            Assert.IsNotNull(_labelText, "Label Text is not assigned in the inspector.");
            Assert.IsNotNull(_typeText, "Type Text is not assigned in the inspector.");
            Assert.IsNotNull(_descriptionText, "Description Text is not assigned in the inspector.");

            Assert.IsNotNull(_dropSpawnPosition, "Root Character Position is not assigned in the inspector.");

            Assert.IsNotNull(_swapButton, "Swap Button is not assigned in the inspector.");
            Assert.IsNotNull(_useButton, "Use Button is not assigned in the inspector.");
            Assert.IsNotNull(_removeButton, "Remove Button is not assigned in the inspector.");
        }

        public void OpenMenu()
        {
            _inventoryUI.gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            DeselectAll();
            UntagItemForSwap();
            _inventoryUI.gameObject.SetActive(false);
        }
        public void AddItem(ItemData item)
        {
            _storedItems.Add(item);
            SpawnUIItem(item);
        }
        private void RemoveItem(ItemData item)
        {
            _storedItems.Remove(item);

            InventoryItem inventoryItem = _inventoryItems.Find(i => i.ItemData == item);
            _inventoryItems.Remove(inventoryItem);

            Destroy(inventoryItem.gameObject);
        }
        private void DropItem(InventoryItem item)
        {
            Instantiate(item.ItemData._itemPrefab, _dropSpawnPosition.transform.position, Quaternion.identity);
            UnequipUponRemoval(item);
            RemoveItem(item.ItemData);
        }
        private void TagItemForSwap(InventoryItem item)
        {
            DeselectAll();
            _swappingSelection = item;
        }
        private void UntagItemForSwap()
        {
            _swappingSelection = null;
        }
        private void SwapItem()
        {
            int index1 = _inventoryItems.IndexOf(_swappingSelection);
            int index2 = _inventoryItems.IndexOf(_currentSelection);

            if (index1 < 0 || index2 < 0 || index1 == index2)
                return;

            var tempStoredItem = _storedItems[index1];
            _storedItems[index1] = _storedItems[index2];
            _storedItems[index2] = tempStoredItem;

            var tempInventoryItem = _inventoryItems[index1];
            _inventoryItems[index1] = _inventoryItems[index2];
            _inventoryItems[index2] = tempInventoryItem;

            _inventoryItems[index1].transform.SetSiblingIndex(index1);
            _inventoryItems[index2].transform.SetSiblingIndex(index2);
        }

        private void UseByType(InventoryItem item)
        {
            switch(item.ItemData.Type)
            {
                case ItemData.ItemType.Consumable:
                    ConsumableItem consumable = item.ItemData._itemPrefab.GetComponentInChildren<ConsumableItem>();
                    consumable.Use(_stats, item);
                    RemoveItem(item.ItemData);
                    break;
                case ItemData.ItemType.Weapon:
                    WeaponData weaponData = item.ItemData as WeaponData;
                    WeaponItem weapon = item.ItemData._itemPrefab.GetComponentInChildren<WeaponItem>();
                    if (_stats.EquippedWeapon == null)
                        weapon.Equip(_stats, item);
                    else if (_stats.EquippedWeapon._id != weaponData._id)
                    {
                        weapon.Unequip(_stats);
                        weapon.Equip(_stats, item);
                    }
                    break;
                case ItemData.ItemType.Equipment:
                    EquipmentData equipmentData = item.ItemData as EquipmentData;
                    EquipableItem equipable = item.ItemData._itemPrefab.GetComponentInChildren<EquipableItem>();
                    if (_stats.EquippedArmor == null)
                        equipable.Equip(_stats, item);
                    else if (_stats.EquippedArmor._id != equipmentData._id)
                    {
                        equipable.Unequip(_stats);
                        equipable.Equip(_stats, item);
                    }
                    break;
                default:
                    break;
            }

            DeselectAll();
        }

        private void UnequipUponRemoval(InventoryItem item)
        {
            switch (item.ItemData.Type)
            {
                case ItemData.ItemType.Weapon:
                    WeaponData weaponData = item.ItemData as WeaponData;
                    WeaponItem weapon = item.ItemData._itemPrefab.GetComponentInChildren<WeaponItem>();
                    if (_stats.EquippedWeapon._id == weaponData._id)
                    {
                        weapon.Unequip(_stats);
                    }
                    break;
                case ItemData.ItemType.Equipment:
                    EquipmentData equipmentData = item.ItemData as EquipmentData;
                    EquipableItem equipable = item.ItemData._itemPrefab.GetComponentInChildren<EquipableItem>();
                    if (_stats.EquippedArmor._id == equipmentData._id)
                    {
                        equipable.Unequip(_stats);
                    }
                    break;
                default:
                    break;
            }

            DeselectAll();
        }

        private void SpawnUIItem(ItemData item)
        {
            InventoryItem inventoryItem = Instantiate(_inventoryItemPrefab, _contentBoxUI.transform);
            inventoryItem.ItemData = item;
            inventoryItem.InventoryController = this;

            _inventoryItems.Add(inventoryItem);
        }

        public void DeselectAll()
        {
            if (_swappingSelection != null) return;

            CurrentSelection = null;

            foreach (InventoryItem item in _inventoryItems)
            {
                item.Deselect();
            }
        }

        private void OnUseClick()
        {
            if (_currentSelection != null)
            {
                UseByType(_currentSelection);
            }
        }
        private void OnDropClick()
        {
            if (_currentSelection != null)
            {
                DropItem(_currentSelection);
                DeselectAll();
            }
        }
        private void OnSwapClick()
        {
            if (_currentSelection != null)
            {
                TagItemForSwap(_currentSelection);
            }
        }
        #endregion
    }
}