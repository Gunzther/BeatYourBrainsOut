using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class BaseCraftPlace : MonoBehaviour
    {
        protected virtual void ShowWeapon(WeaponData.Weapon weaponName, SlotItem[] items)
        {
            foreach (SlotItem item in items)
            {
                item.weaponModel.SetActive(item.WeaponName == weaponName);
            }
        }

        protected virtual void HideWeapon(SlotItem[] items)
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
        public WeaponData.Weapon WeaponName = default;
        public GameObject weaponModel = default;
    }
}
