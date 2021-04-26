using BBO.BBO.GameData;
using BBO.BBO.WeaponManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon = default;

        public WeaponData.Weapon CurrentWeaponName => weapon.WeaponName;
        public Weapon CurrentWeapon => weapon;

        public PlayerData.PlayerSprite SetWeapon(Weapon interactWeapon)
        {
            if (interactWeapon == null)
            {
                weapon.ResetWeaponValue();
            }
            else
            {
                weapon.CopyWeaponValue(interactWeapon);
                print(weapon.WeaponName);
            }

            print(CurrentWeaponName);

            return GetPlayerSprite(CurrentWeaponName);
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
                    return PlayerData.PlayerSprite.RubberBand;
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