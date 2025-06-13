using UnityEngine;
using Inventory;
using UnityEngine.Assertions;

namespace Characters
{
    public class Interaction : MonoBehaviour
    {
        #region FIELDS
        [Header("Settings")]
        [SerializeField] private GameObject _rootObject;
        private bool _isCurrentlyInteracting = false;
        private IInteractable _closestInteractable;
        #endregion

        #region PROPERTIES
        public GameObject RootObject => _rootObject;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            Validate();
        }
      
        private void OnTriggerEnter(Collider other)
        {
            GetClosestInteractable(other);
        }

        private void OnTriggerExit(Collider other)
        {
            ClearClosestInteractable(other);
        }
        #endregion

        #region CUSTOM METHODS
        private void Validate()
        {
            Assert.IsNotNull(_rootObject, "Root object must be assigned in the Interaction component.");
        }

        private void GetClosestInteractable(Collider other)
        {
            // Check if the collider has an IInteractable component and if it is not the current closest interactable
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable == null || interactable == _closestInteractable) return;

            // If the closest interactable is null, set it to the current interactable
            if (_closestInteractable == null)
            {
                _closestInteractable = interactable;
                return;
            }

            // If the closest interactable is a MonoBehaviour (just for error check; but it shouldn't occur), check if the new interactable is closer
            MonoBehaviour interactableMono = _closestInteractable as MonoBehaviour;
            if (interactableMono == null)
            {
                Debug.LogError("Closest interactable is not a MonoBehaviour.");
                return;
            }

            if (Vector3.Distance(transform.position, other.transform.position) <
                Vector3.Distance(transform.position, interactableMono.transform.position))
            {
                _closestInteractable = interactable;
            }
        }

        private void ClearClosestInteractable(Collider other)
        {
            // If the collider is an interactable and is the current closest interactable, clear it
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable == null || interactable != _closestInteractable) return;

            _closestInteractable = null;
        }

        public void ForceClearClosestInteractable()
        {
            _closestInteractable = null;
        }

        public void Interact()
        {
            if (_closestInteractable != null && !_isCurrentlyInteracting)
            {
                _isCurrentlyInteracting = true;
                _closestInteractable.Interact(this);
                _isCurrentlyInteracting = false;
            }
        }
        #endregion
    }
}