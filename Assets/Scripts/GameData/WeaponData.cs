using System.Collections.Generic;

namespace BBO.BBO.GameData
{
    public static class WeaponData
    {
        public enum Type
        {
            Normal, // weapon that attack object
            IntervalDamage, // weapon that attack for a certain time, then disappear
            LimitAttacksNumber, // weapon that has limit number of attacks
            Protected, // weapon for protection
        }

        public enum Weapon
        {
            NoWeapon,
            Baseballbat,
            Nail,
            RubberBand,
            BaseballbatWithNails,
            Shield,
            Sling
        }

        public static bool IsDirectionalWeapon(Weapon weaponName)
        {
            return weaponName == Weapon.RubberBand ||
                   weaponName == Weapon.Shield ||
                   weaponName == Weapon.Sling;
        }

        public static bool IsCastAttackWeapon(Weapon weaponName)
        {
            return weaponName == Weapon.Baseballbat ||
                   weaponName == Weapon.BaseballbatWithNails ||
                   weaponName == Weapon.Nail;
        }

        public static bool IsCloseRangeWeapon(Weapon weaponName)
        {
            return weaponName == Weapon.Baseballbat ||
                   weaponName == Weapon.BaseballbatWithNails;
        }

        public static CraftedWeaponRecipe[] CraftedWeaponRecipes =
        {
            new CraftedWeaponRecipe(Weapon.BaseballbatWithNails, new Dictionary<Weapon, int>()
            {
                { Weapon.Baseballbat, 1 },
                { Weapon.Nail, 1 }
            }),
            new CraftedWeaponRecipe(Weapon.Shield, new Dictionary<Weapon, int>()
            {
                { Weapon.Baseballbat, 2 },
                { Weapon.Nail, 3 }
            }),
            new CraftedWeaponRecipe(Weapon.Sling, new Dictionary<Weapon, int>()
            {
                { Weapon.Baseballbat, 1 },
                { Weapon.RubberBand, 1},
                { Weapon.Nail, 1 }
            })
        };
    }

    public class CraftedWeaponRecipe
    {
        public WeaponData.Weapon WeaponName { get; private set; }
        public Dictionary<WeaponData.Weapon, int> Recipe { get; private set; }

        public CraftedWeaponRecipe(WeaponData.Weapon weaponName, Dictionary<WeaponData.Weapon, int> recipe)
        {
            WeaponName = weaponName;
            Recipe = recipe;
        }
    }
}
