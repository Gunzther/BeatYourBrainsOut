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

        public bool CanPick => currentWeaponName != WeaponData.Weapon.NoWeapon;
        public bool CanCraft => currentWeaponName == WeaponData.Weapon.NoWeapon;

        private Dictionary<WeaponData.Weapon, int> slotItems = default;
        private WeaponData.Weapon currentWeaponName = default;

        public void AddSlotItems(WeaponData.Weapon weaponKey, int amount)
        {
            if (slotItems == null)
            {
                slotItems = new Dictionary<WeaponData.Weapon, int>();
            }

            if (slotItems.TryGetValue(weaponKey, out int currentAmount))
            {
                slotItems[weaponKey] = currentAmount + amount;
            }
            else
            {
                slotItems.Add(weaponKey, amount);
            }
        }

        public void RemoveSlotItems(WeaponData.Weapon weaponKey, int amount)
        {
            if (slotItems == null)
            {
                return;
            }

            if (slotItems.TryGetValue(weaponKey, out int currentAmount))
            {
                slotItems[weaponKey] = currentAmount - amount;
            }
        }

        /// <returns>craft score</returns>
        public int Craft()
        {
            foreach (CraftedWeaponRecipe recipe in WeaponData.CraftedWeaponRecipes)
            {
                if (IsMatchedRecipe(recipe.Recipe, slotItems))
                {
                    print($"[{nameof(CraftTable)}] craft: {recipe.WeaponName}");
                    ShowWeapon(recipe.WeaponName, items);
                    currentWeaponName = recipe.WeaponName;
                    ClearSlots();

                    return recipe.CraftScore;
                }
            }

            return 0;
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

        public Weapon OnPicked()
        {
            Weapon pickedWeapon = GetWeapon(currentWeaponName, items);
            Clear();

            return pickedWeapon;
        }

        public void Clear()
        {
            currentWeaponName = default;
            HideWeapon(items);
        }

        protected override void ShowWeapon(WeaponData.Weapon weaponName, SlotItem[] items)
        {
            noWeaponObject.SetActive(false);
            base.ShowWeapon(weaponName, items);
        }

        protected override void HideWeapon(SlotItem[] items)
        {
            base.HideWeapon(items);
            noWeaponObject.SetActive(true);
        }

        private void ClearSlots()
        {
            foreach (CraftSlot slot in craftSlots)
            {
                slot.Clear();
            }

            slotItems.Clear();
        }
    }
}