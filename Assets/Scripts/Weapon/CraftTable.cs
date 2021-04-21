using BBO.BBO.GameData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class CraftTable : BaseCraftPlace
    {
        [SerializeField]
        private CraftSlot[] craftSlots = default;

        [SerializeField]
        private GameObject noWeaponObject = default;

        [SerializeField]
        private SlotItem[] items = default;

        public bool CanPick => currentWeapon != WeaponData.Weapon.NoWeapon;
        public bool CanCraft => currentWeapon == WeaponData.Weapon.NoWeapon;

        private Dictionary<WeaponData.Weapon, int> slotItems = default;
        private WeaponData.Weapon currentWeapon = default;

        public void AddSlotItems(WeaponData.Weapon weaponKey, int amount)
        {
            if (slotItems.TryGetValue(weaponKey, out int currentAmount))
            {
                slotItems[weaponKey] = currentAmount + amount;
            }
            else
            {
                slotItems.Add(weaponKey, amount);
            }
        }

        public void Craft()
        {
            foreach (CraftedWeaponRecipe recipe in WeaponData.CraftedWeaponRecipes)
            {
                if (IsMatchedRecipe(recipe.Recipe, slotItems))
                {
                    ShowWeapon(recipe.Weapon, items);
                    currentWeapon = recipe.Weapon;
                }
            }
        }

        public bool IsMatchedRecipe(Dictionary<WeaponData.Weapon, int> dict1, Dictionary<WeaponData.Weapon, int> dict2)
        {
            if (dict1.Count == dict2.Count && !dict1.Except(dict2).Any())
            {
                foreach (KeyValuePair<WeaponData.Weapon, int> entry in dict1)
                {
                    if (entry.Value != dict2[entry.Key])
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public WeaponData.Weapon OnPicked()
        {
            WeaponData.Weapon pickedWeapon = currentWeapon;
            Clear();

            return pickedWeapon;
        }

        public void Clear()
        {
            currentWeapon = default;
            HideWeapon(items);
        }

        protected override void ShowWeapon(WeaponData.Weapon weapon, SlotItem[] items)
        {
            noWeaponObject.SetActive(false);
            base.ShowWeapon(weapon, items);
        }

        protected override void HideWeapon(SlotItem[] items)
        {
            base.HideWeapon(items);
            noWeaponObject.SetActive(true);
        }
    }
}
