using BBO.BBO.MonsterMovement;
using System;
using UnityEngine;

namespace BBO.BBO.MonsterManagement
{
    public class MonsterCharacter : MonoBehaviour
    {
        [SerializeField]
        private MonstersMovement monsterMovement = default;

        [SerializeField]
        private int hp = default;

        public Action Dead = default;

        public int DecreaseMonsterHp(int value)
        {
            hp -= value;

            if (hp <= 0)
            {
                Dead?.Invoke();
                Destroy(gameObject);
                return value + hp;
            }

            return value;
        }

        public void IncreaseMonsterHp(int value)
        {
            hp += value;
        }

        public void OnAttack()
        {
            monsterMovement.OnAttackMovement();
        }

        public void OnAttacked()
        {
            monsterMovement.OnAttackedMovement();
        }
    }
}
