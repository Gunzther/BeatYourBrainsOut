using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class WeaponBox : MonoBehaviour
    {
        [SerializeField]
        private WeaponData.Weapon weapon = default;
        public WeaponData.Weapon Weapon => weapon;
    }
}