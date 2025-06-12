using UnityEngine;

namespace Inventory
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        #region FIELDS
        [SerializeField] private ItemData itemData;
        #endregion

        public void Interact()
        {
            Debug.Log($"Picked up item: {itemData.itemName}");
            Destroy(transform.parent.gameObject);
        }
    }
}