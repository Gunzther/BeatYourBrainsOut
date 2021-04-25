using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class CraftSlot : BaseCraftPlace
    {
        [SerializeField]
        private CraftTable craftTable = default;

        [SerializeField]
        private SlotItem[] items = default;

        public bool CanPick => currentWeaponName != WeaponData.Weapon.NoWeapon;
        public bool CanPlace => currentWeaponName == WeaponData.Weapon.NoWeapon;

        private WeaponData.Weapon currentWeaponName = default;
        private Weapon currentWeapon = default;

        public (WeaponData.Weapon, Weapon) OnPicked()
        {
            WeaponData.Weapon pickedWeaponName = currentWeaponName;
            Weapon pickedWeapon = currentWeapon;
            craftTable.RemoveSlotItems(pickedWeaponName, 1);
            Clear();

            return (pickedWeaponName, pickedWeapon);
        }

        public void OnPlaced(WeaponData.Weapon weaponName, Weapon weapon)
        {
            currentWeaponName = weaponName;
            currentWeapon = weapon;
            base.ShowWeapon(currentWeaponName, items);
            craftTable.AddSlotItems(currentWeaponName, 1);
        }

        public void Clear()
        {
            currentWeaponName = default;
            currentWeapon = default;
            base.HideWeapon(items);
        }
    }
}
