using UnityEngine;

namespace BBO.BBO.MonsterManagement
{
    public class MonsterDestroyer : MonoBehaviour
    {
        [SerializeField]
        private int damageValue = default;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
            {
                monster.DecreaseMonsterHp(damageValue);
            }
        }
    }
}
