using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class WeaponBox : MonoBehaviour
    {
        [SerializeField]
        private WeaponsData.Weapon weapon = default;
        public WeaponsData.Weapon Weapon => weapon;
    }
}