using UnityEngine;
using Characters;
using UnityEngine.Assertions;
using Inventory;

namespace Player
{
    public class PlayerContext : MonoBehaviour
    {
        #region FIELDS
        [Header("Settings")]
        [SerializeField] private string InputsTag = "Inputs";

        [Space]
        [Header("Components")]
        private Inputs _inputs;
        private Movement _movement;
        private Interaction _interaction;
        private InventoryController _inventory;

        private Rigidbody _rb;
        private Animator _anim;

        private PlayerFSM _playerStateMachine;
        #endregion
            
        #region PROPERTIES
        public Inputs Inputs
        {
            get
            {
                if (_inputs == null) _inputs = FindInputs();
                return _inputs;
            }
        }
        public Movement Movement => _movement;
        public Interaction Interaction => _interaction;
        public InventoryController Inventory => _inventory;
        public Rigidbody Rb => _rb;
        public Animator Anim => _anim;
        public PlayerFSM PlayerStateMachine
        {
            get => _playerStateMachine;
            set
            {
                if (_playerStateMachine != null)
                {
                    Debug.LogError($"{nameof(PlayerFSM)} on {nameof(PlayerContext)} already assigned. Please DO NOT modify the State Machine reference in runtime.");
                    return;
                }
                _playerStateMachine = value;
            }
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            InitializeInternal();
        }
        private void Start()
        {
            InitializeExternal();
            Validate();
        }
        #endregion

        #region CUSTOM METHODS
        private void InitializeInternal()
        {
            _rb = GetComponent<Rigidbody>();
            _anim = GetComponent<Animator>();
        }
        private void InitializeExternal()
        {
            _inputs = FindInputs();
            _movement = GetComponentInChildren<Movement>();
            _interaction = GetComponentInChildren<Interaction>();
            _inventory = GetComponentInChildren<InventoryController>();
        }
        private Inputs FindInputs()
        {
            Inputs inputs = GameObject.FindWithTag(InputsTag).GetComponent<Inputs>();
            if (inputs == null)
            {
                Debug.LogError($"{nameof(Inputs)} component not found in the scene. Please ensure it is assigned to a GameObject with the '{InputsTag}' tag.");
                return null;
            }

            return inputs;
        }
        private void Validate()
        {
            Assert.IsNotNull(_rb, $"{nameof(Rigidbody)} component is null in {nameof(PlayerContext)}.");
            Assert.IsNotNull(_anim, $"{nameof(Animator)} component is null in {nameof(PlayerContext)}.");
            Assert.IsNotNull(_inputs, $"{nameof(Inputs)} component is null in {nameof(PlayerContext)}.");
            Assert.IsNotNull(_movement, $"{nameof(Movement)} component is null in {nameof(PlayerContext)}.");
            Assert.IsNotNull(_interaction, $"{nameof(Interaction)} component is null in {nameof(PlayerContext)}.");
            Assert.IsNotNull(_inventory, $"{nameof(InventoryController)} component is null in {nameof(PlayerContext)}.");
        }
        #endregion
    }
}