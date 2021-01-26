using BBO.BBO.MonsterMovement;
using UnityEngine;

namespace BBO.BBO.MonsterManagement
{
    public class MonsterCharacter : MonoBehaviour
    {
        [SerializeField]
        private MonstersMovement monsterMovement = default;

        [SerializeField]
        private int hp = default;

        public void DecreaseMonsterHp(int value)
        {
            hp -= value;

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void IncreaseMonsterHp(int value)
        {
            hp += value;
        }

        public void OnAttack()
        {
            monsterMovement.OnAttackMovement();
        }
    }
}
