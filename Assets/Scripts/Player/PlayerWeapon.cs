using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon
    {
        public WeaponsData.Weapon CurrentType => type;
        public Sprite WeaponSprite => weaponSprite;
        public int WeaponHitTriggerHash => weaponHitTriggerHash;

        private WeaponsData.Weapon type = default;
        private Sprite weaponSprite = default;
        private int weaponHitTriggerHash = default;

        public void SetWeaponType(WeaponsData.Weapon type)
        {
            this.type = type;
            weaponSprite = Resources.Load<Sprite>(type.ToString());
            weaponHitTriggerHash = PlayerData.WeaponTriggerHash[(int)type];
        }

        public void SetWeaponType(string name)
        {
            if (Enum.TryParse(name, out WeaponsData.Weapon type))
            {
                SetWeaponType(type);
            }
        }
    }
}