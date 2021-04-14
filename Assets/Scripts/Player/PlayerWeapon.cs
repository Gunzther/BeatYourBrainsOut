using BBO.BBO.GameData;
using System;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon
    {
        public WeaponsData.Weapon CurrentType => weapon;

        private WeaponsData.Weapon weapon = default;

        public PlayerData.PlayerSprite SetWeapon(WeaponsData.Weapon weapon)
        {
            this.weapon = weapon;
            return GetPlayerSprite(weapon);
        }

        public void SetWeapon(string name)
        {
            if (Enum.TryParse(name, out WeaponsData.Weapon weapon))
            {
                SetWeapon(weapon);
            }
        }

        private PlayerData.PlayerSprite GetPlayerSprite(WeaponsData.Weapon weapon)
        {
            switch (this.weapon)
            {
                case WeaponsData.Weapon.Baseballbat:
                    return PlayerData.PlayerSprite.Baseball;
                case WeaponsData.Weapon.Nail:
                    return PlayerData.PlayerSprite.Nail;
                case WeaponsData.Weapon.RubberBand:
                    return PlayerData.PlayerSprite.RubberBandDefault;
                default:
                    return PlayerData.PlayerSprite.Default;
            }
        }
    }
}