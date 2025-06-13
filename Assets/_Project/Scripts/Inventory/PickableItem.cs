using UnityEngine;
using Characters;

namespace Inventory
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        #region FIELDS
        [SerializeField] private ItemData itemData;
        #endregion

        public void Interact(Interaction interaction)
        {
            interaction.ForceClearClosestInteractable();
            interaction.RootObject.GetComponentInChildren<InventoryController>().AddItem(itemData);
            Destroy(transform.parent.gameObject);
        }
    }
}