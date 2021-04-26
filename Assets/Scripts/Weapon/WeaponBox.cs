using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class WeaponBox : MonoBehaviour
    {
        [SerializeField]
        private WeaponData.Weapon weaponName = default;

        [SerializeField]
        private Weapon weaponPrototype = default;

        public WeaponData.Weapon WeaponName => weaponName;
        public Weapon WeaponPrototype => weaponPrototype;
    }
}