using BBO.BBO.GameData;
using System;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerWeapon
    {
        public enum Type
        {
            baseballbat,
            stickyband,
            nailbox,
            nail
        }

        public Type CurrentType => type;
        public Sprite WeaponSprite => weaponSprite;
        public int WeaponHitTriggerHash => weaponHitTriggerHash;

        private Type type = default;
        private Sprite weaponSprite = default;
        private int weaponHitTriggerHash = default;

        public void SetWeaponType(Type type)
        {
            this.type = type;
            weaponSprite = Resources.Load<Sprite>(type.ToString());
            weaponHitTriggerHash = PlayerData.WeaponTriggerHash[(int)type];
        }

        public void SetWeaponType(string name)
        {
            if (Enum.TryParse(name, out Type type))
            {
                SetWeaponType(type);
            }
        }
    }
}