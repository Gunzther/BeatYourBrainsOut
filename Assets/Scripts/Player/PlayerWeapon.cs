using BBO.BBO.GameData;
using BBO.BBO.WeaponManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter playerCharacter = default;

        [SerializeField]
        private Weapon weapon = default;

        [SerializeField]
        private Weapon[] activeWeapons = default;

        public WeaponData.Weapon CurrentWeaponName => weapon.WeaponName;
        public Weapon CurrentWeapon => weapon;

        private Dictionary<WeaponData.Weapon, Weapon> activeWeaponsDict = default;

        public PlayerData.PlayerSprite SetWeapon(Weapon interactWeapon)
        {
            if (interactWeapon == null)
            {
                weapon.ResetWeaponValue();
            }
            else
            {
                weapon.CopyWeaponValue(interactWeapon);
            }

            return GetPlayerSprite(CurrentWeaponName);
        }

        public void ActiveWeapon(Vector3 inputDirection)
        {
            if (activeWeaponsDict.TryGetValue(CurrentWeaponName, out Weapon activeWeapon))
            {
                Weapon newActiveWeapon = Instantiate(activeWeapon, transform.position, Quaternion.identity);

                if (newActiveWeapon.GetComponent<WeaponMovement>() is WeaponMovement weaponMovement)
                {
                    weaponMovement.ActiveMovement(inputDirection);
                }

                CurrentWeapon.OnAttack();
            }
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

        private void Awake()
        {
            activeWeaponsDict = new Dictionary<WeaponData.Weapon, Weapon>();

            foreach (Weapon weapon in activeWeapons)
            {
                activeWeaponsDict.Add(weapon.WeaponName, weapon);
            }
        }
    }
}