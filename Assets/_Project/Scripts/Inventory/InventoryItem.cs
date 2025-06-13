using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        #region FIELDS
        [Header("UI References")]
        private Button _button;
        [SerializeField] private Image _refreshableIcon;
        [SerializeField] private Image _selectionIndicator;

        private ItemData _itemData;
        private InventoryController _inventoryController;

        private Color _deselectedColor;
        private Color _selectedColor = Color.green;
        #endregion

        #region PROPERTIES
        public ItemData ItemData 
        { 
            get => _itemData;
            set
            {
                if (_itemData != null) return;

                _itemData = value;
            }
        }
        public InventoryController InventoryController
        {
            get => _inventoryController;
            set
            {
                if (_inventoryController != null) return;

                _inventoryController = value;
            }
        }
        #endregion

        #region UNITY CALLBACKS
        public void Start()
        {
            _deselectedColor = _selectionIndicator.color;
            _refreshableIcon.sprite = _itemData._icon;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Select);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(Select);
        }
        #endregion

        #region CUSTOM METHODS
        public void Select()
        {
            _inventoryController.DeselectAll();
            _inventoryController.CurrentSelection = this;
            ToggleSelectionIndicator(true);
        }

        public void Deselect()
        {
            ToggleSelectionIndicator(false);
        }

        public void ToggleSelectionIndicator(bool selected)
        {
            if (selected && _inventoryController.CurrentSelection == this)
                _selectionIndicator.color = _selectedColor;
            else
                _selectionIndicator.color = _deselectedColor;
        }
        #endregion
    }
}