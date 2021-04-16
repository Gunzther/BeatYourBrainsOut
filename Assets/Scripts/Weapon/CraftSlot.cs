using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class CraftSlot : MonoBehaviour
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
            currentWeapon = default;
            HideWeapon();

            return pickedWeapon;
        }

        public void OnPlaced(WeaponData.Weapon weapon)
        {
            currentWeapon = weapon;
            ShowWeapon(weapon);
        }

        private void ShowWeapon(WeaponData.Weapon weapon)
        {
            foreach (SlotItem item in items)
            {
                if (item.Weapon == weapon)
                {
                    item.weaponModel.SetActive(true);
                }
                else
                {
                    item.weaponModel.SetActive(false);
                }
            }
        }

        private void HideWeapon()
        {
            foreach (SlotItem item in items)
            {
                item.weaponModel.SetActive(false);
            }
        }
    }

    [Serializable]
    public class SlotItem
    {
        public WeaponData.Weapon Weapon = default;
        public GameObject weaponModel = default;
    }
}
