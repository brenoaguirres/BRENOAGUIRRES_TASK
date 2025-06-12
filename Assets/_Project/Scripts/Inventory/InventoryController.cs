using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject _inventoryUI;
        #endregion

        #region CUSTOM METHODS
        public void OpenMenu()
        {
            _inventoryUI.gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            _inventoryUI.gameObject.SetActive(false);
        }
        #endregion
    }
}