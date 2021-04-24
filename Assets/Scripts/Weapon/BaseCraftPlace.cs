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
                if (item.WeaponName == weaponName)
                {
                    item.weaponModel.SetActive(true);
                }
                else
                {
                    item.weaponModel.SetActive(false);
                }
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
