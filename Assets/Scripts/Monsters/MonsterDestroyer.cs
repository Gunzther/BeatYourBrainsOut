using BBO.BBO.GameData;
using UnityEngine;

namespace BBO.BBO.MonsterManagement
{
    public class MonsterDestroyer : MonoBehaviour
    {
        [SerializeField]
        private int damageValue = default;

        [SerializeField]
        private WeaponsData.Type type = default;

        [SerializeField]
        private float intervalSeconds = default;

        private bool isIntervalDamage = false;
        private float time = default;

        public void SetDamageValue(int value)
        {
            damageValue = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
            {
                monster.DecreaseMonsterHp(damageValue);
                monster.OnAttacked();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
            {
                monster.DecreaseMonsterHp(damageValue);
                monster.OnAttacked();
            }
        }

        private void Start()
        {
            isIntervalDamage = type == WeaponsData.Type.IntervalDamage;
        }

        private void Update()
        {
            if (isIntervalDamage)
            {
                time += Time.deltaTime;

                if (time > intervalSeconds)
                {
                    // TODO: change to pooling objects
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
