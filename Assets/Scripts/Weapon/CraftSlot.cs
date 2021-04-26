using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class CraftSlot : BaseCraftPlace
    {
        [SerializeField]
        private CraftTable craftTable = default;

        [SerializeField]
        private Weapon currentWeapon = default;

        [SerializeField]
        private Weapon pickedWeapon = default;

        [SerializeField]
        private SlotItem[] items = default;

        public bool CanPick => currentWeaponName != WeaponData.Weapon.NoWeapon;
        public bool CanPlace => currentWeaponName == WeaponData.Weapon.NoWeapon;

        private WeaponData.Weapon currentWeaponName => currentWeapon.WeaponName;

        public Weapon OnPicked()
        {
            craftTable.RemoveSlotItems(pickedWeapon.WeaponName, 1);
            Clear();

            return pickedWeapon;
        }

        public void OnPlaced(Weapon weapon)
        {
            currentWeapon.CopyWeaponValue(weapon);
            pickedWeapon.CopyWeaponValue(weapon);
            base.ShowWeapon(currentWeaponName, items);
            craftTable.AddSlotItems(currentWeaponName, 1);
        }

        public void Clear()
        {
            currentWeapon.ResetWeaponValue();
            base.HideWeapon(items);
        }
    }
}
