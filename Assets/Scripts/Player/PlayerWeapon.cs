using BBO.BBO.GameData;
using System;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon
    {
        public WeaponData.Weapon CurrentWeapon => weapon;

        private WeaponData.Weapon weapon = default;

        public PlayerData.PlayerSprite SetWeapon(WeaponData.Weapon weapon)
        {
            this.weapon = weapon;
            return GetPlayerSprite(weapon);
        }

        public void SetWeapon(string name)
        {
            if (Enum.TryParse(name, out WeaponData.Weapon weapon))
            {
                SetWeapon(weapon);
            }
        }

        private PlayerData.PlayerSprite GetPlayerSprite(WeaponData.Weapon weapon)
        {
            switch (this.weapon)
            {
                case WeaponData.Weapon.Baseballbat:
                    return PlayerData.PlayerSprite.Baseball;
                case WeaponData.Weapon.Nail:
                    return PlayerData.PlayerSprite.Nail;
                case WeaponData.Weapon.RubberBand:
                    return PlayerData.PlayerSprite.RubberBandDefault;
                default:
                    return PlayerData.PlayerSprite.Default;
            }
        }
    }
}