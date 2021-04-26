using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class BaseCraftPlace : MonoBehaviour
    {
        protected virtual Weapon GetWeapon(WeaponData.Weapon weaponName, SlotItem[] items)
        {
            foreach (SlotItem item in items)
            {
                if (item.WeaponName == weaponName)
                {
                    return item.WeaponModel;
                }
            }

            return null;
        }

        protected virtual void ShowWeapon(WeaponData.Weapon weaponName, SlotItem[] items)
        {
            foreach (SlotItem item in items)
            {
                item.WeaponModel.gameObject.SetActive(item.WeaponName == weaponName);
            }
        }

        protected virtual void HideWeapon(SlotItem[] items)
        {
            foreach (SlotItem item in items)
            {
                item.WeaponModel.gameObject.SetActive(false);
            }
        }
    }

    [Serializable]
    public class SlotItem
    {
        public WeaponData.Weapon WeaponName = default;
        public Weapon WeaponModel = default;
    }
}
