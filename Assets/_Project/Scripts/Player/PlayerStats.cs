using Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        #region FIELDS
        [Header("Display Settings")]
        [SerializeField] private TextMeshProUGUI _displayTextBox;

        private float _health = 60;
        private float _maxHealth = 100;
        private float _attackDamage = 15;
        private float _defense = 5;
        private float _attackSpeed = 5;
        private WeaponData _equippedWeapon;
        private EquipmentData _equippedArmor;
        #endregion

        #region PROPERTIES
        public float Health
        {
            get => _health;
            set
            {
                float newHealth = _health + value;
                _health = Mathf.Clamp(newHealth, 0, MaxHealth);
            }
        }
        public float MaxHealth
        {
            get
            {
                return _maxHealth + (_equippedArmor != null ? _equippedArmor._health : 0);
            }
        }
        public float AttackDamage
        {
            get
            {
                return _attackDamage + (_equippedWeapon != null ? _equippedWeapon._atkDamage : 0);
            }
        }
        public float Defense
        {
            get
            {
                return _defense + (_equippedArmor != null ? _equippedArmor._defense : 0);
            }
        }
        public float AttackSpeed
        {
            get
            {
                return _attackSpeed + (_equippedWeapon != null ? _equippedWeapon._atkSpeed : 0);
            }
        }
        public WeaponData EquippedWeapon
        {
            get => _equippedWeapon;
            set => _equippedWeapon = value;
        }
        public EquipmentData EquippedArmor
        {
            get => _equippedArmor;
            set => _equippedArmor = value;
        }
        #endregion

        #region UNITY CALLBACKS
        public void Update()
        {
            if (_displayTextBox != null && _displayTextBox.gameObject.activeInHierarchy)
            {
                string weaponName = _equippedWeapon != null ? _equippedWeapon._itemName : "N/A";
                string itemName = _equippedArmor != null ? _equippedArmor._itemName : "N/A";
                _displayTextBox.text = $"Player Stats\r\n" +
                    $"Health: {Health}\r\n" +
                    $"Max Health: {MaxHealth}\r\n" +
                    $"Attack Damage: {AttackDamage}\r\n" +
                    $"Attack Speed: {AttackSpeed}\r\n" +
                    $"Defense: {Defense}\r\n" +
                    $"Equipped Weapon: {weaponName}\r\n" +
                    $"Equipped Item: {itemName}\r"
                    ;
            }
        }
        #endregion
    }
}