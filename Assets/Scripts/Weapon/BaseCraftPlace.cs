using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class BaseCraftPlace : MonoBehaviour
    {
        protected virtual void ShowWeapon(WeaponData.Weapon weapon, SlotItem[] items) 
        {
            foreach (SlotItem item in items)
            {
                item.weaponModel.SetActive(item.Weapon == weapon);
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
        public WeaponData.Weapon Weapon = default;
        public GameObject weaponModel = default;
    }
}
