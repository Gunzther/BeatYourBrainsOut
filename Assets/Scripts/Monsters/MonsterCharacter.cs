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

        [SerializeField] 
        private GameObject bullet;

        [SerializeField]
        private Transform BulletSpawnPoint;
        
        public Action Dead = default;

        public void DecreaseMonsterHp(int value)
        {
            hp -= value;

            if (hp <= 0)
            {
                Dead?.Invoke();
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

        public void OnAttacked()
        {
            monsterMovement.OnAttackedMovement();
        }
    }
}
