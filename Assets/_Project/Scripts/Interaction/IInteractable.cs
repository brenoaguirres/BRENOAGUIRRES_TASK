using Characters;
using UnityEngine;

namespace Inventory
{
    public interface IInteractable
    {
        public void Interact(Interaction interaction);
    }
}