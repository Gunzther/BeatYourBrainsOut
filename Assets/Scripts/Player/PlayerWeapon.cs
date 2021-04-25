using BBO.BBO.GameData;
using BBO.BBO.WeaponManagement;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon
    {
        public WeaponData.Weapon CurrentWeaponName => weaponName;
        public Weapon CurrentWeapon => weapon;

        private WeaponData.Weapon weaponName = default;
        private Weapon weapon = default;

        public PlayerData.PlayerSprite SetWeapon(WeaponData.Weapon weaponName, Weapon interactWeapon)
        {
            this.weaponName = weaponName;
            this.weapon = interactWeapon;
            return GetPlayerSprite(weaponName);
        }

        private PlayerData.PlayerSprite GetPlayerSprite(WeaponData.Weapon weaponName)
        {
            switch (weaponName)
            {
                case WeaponData.Weapon.Baseballbat:
                    return PlayerData.PlayerSprite.Baseball;
                case WeaponData.Weapon.Nail:
                    return PlayerData.PlayerSprite.Nail;
                case WeaponData.Weapon.RubberBand:
                    return PlayerData.PlayerSprite.RubberBandDefault;
                case WeaponData.Weapon.BaseballbatWithNails:
                    return PlayerData.PlayerSprite.MorningStar;
                case WeaponData.Weapon.Shield:
                    return PlayerData.PlayerSprite.Shield;
                case WeaponData.Weapon.Sling:
                    return PlayerData.PlayerSprite.Sling;
                default:
                    return PlayerData.PlayerSprite.Default;
            }
        }
    }
}