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

        public bool CanPick => currentWeapon != WeaponData.Weapon.NoWeapon;
        public bool CanPlace => currentWeapon == WeaponData.Weapon.NoWeapon;

        private WeaponData.Weapon currentWeapon = default;

        public WeaponData.Weapon OnPicked()
        {
            WeaponData.Weapon pickedWeapon = currentWeapon;
            Clear();

            return pickedWeapon;
        }

        public void OnPlaced(WeaponData.Weapon weapon)
        {
            currentWeapon = weapon;
            base.ShowWeapon(weapon, items);
            craftTable.AddSlotItems(weapon, 1);
        }

        public void Clear()
        {
            currentWeapon = default;
            base.HideWeapon(items);
        }
    }
}
