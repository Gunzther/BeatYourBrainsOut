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

        public WeaponData.Weapon OnPicked()
        {
            WeaponData.Weapon pickedWeaponName = currentWeaponName;
            Clear();

            return pickedWeaponName;
        }

        public void OnPlaced(WeaponData.Weapon weaponName)
        {
            currentWeaponName = weaponName;
            base.ShowWeapon(weaponName, items);
            craftTable.AddSlotItems(weaponName, 1);
        }

        public void Clear()
        {
            currentWeaponName = default;
            base.HideWeapon(items);
        }
    }
}
