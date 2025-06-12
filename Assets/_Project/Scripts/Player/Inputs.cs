using UnityEngine;

namespace Player
{
    public class Inputs : MonoBehaviour
    {
        #region FIELDS
        private PlayerInputs _playerInputs;

        private Vector2 _movement;
        private bool _confirm;
        private bool _back;
        private bool _menu;
        #endregion

        #region PROPERTIES
        public Vector2 Movement => _movement;
        public bool Confirm => _confirm;
        public bool Back => _back;
        public bool Menu => _menu;
        #endregion

        #region UNITY CALLBACKS
        public void Awake()
        {
            _playerInputs = new PlayerInputs();
            InitializeInputs();
        }

        public void OnEnable()
        {
            InitializeInputs();
        }

        public void OnDisable()
        {
            CleanupInputs();
        }
        #endregion

        #region CUSTOM METHODS
        public void InitializeInputs()
        {
            if (_playerInputs == null)
            {
                _playerInputs = new PlayerInputs();
            }
            _playerInputs.Enable();

            _playerInputs.Player.Movement.started += ctx => _movement = ctx.ReadValue<Vector2>();
            _playerInputs.Player.Movement.performed += ctx => _movement = ctx.ReadValue<Vector2>();
            _playerInputs.Player.Movement.canceled += ctx => _movement = ctx.ReadValue<Vector2>();

            _playerInputs.Player.Confirm.started += ctx => _confirm = ctx.ReadValueAsButton();
            _playerInputs.Player.Confirm.performed += ctx => _confirm = ctx.ReadValueAsButton();
            _playerInputs.Player.Confirm.canceled += ctx => _confirm = ctx.ReadValueAsButton();

            _playerInputs.Player.Back.started += ctx => _back = ctx.ReadValueAsButton();
            _playerInputs.Player.Back.performed += ctx => _back = ctx.ReadValueAsButton();
            _playerInputs.Player.Back.canceled += ctx => _back = ctx.ReadValueAsButton();

            _playerInputs.Player.Menu.started += ctx => _menu = ctx.ReadValueAsButton();
            _playerInputs.Player.Menu.performed += ctx => _menu = ctx.ReadValueAsButton();
            _playerInputs.Player.Menu.canceled += ctx => _menu = ctx.ReadValueAsButton();

        }

        public void CleanupInputs()
        {
            _playerInputs.Disable();
        }
        #endregion
    }
}