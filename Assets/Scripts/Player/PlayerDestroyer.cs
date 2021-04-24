using BBO.BBO.MonsterManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerDestroyer : MonoBehaviour
    {
        [SerializeField]
        private int damageValue = default;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerCharacter>() is PlayerCharacter player)
            {
                player.CurrentPlayerStats.DecreasePlayerHealth(damageValue);
                player.UpdateHpUI();
                player.TriggerHurtAnimation();
                player.PlayHurtSound();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerCharacter>() is PlayerCharacter)
            {
                if (gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
                {
                    monster.OnAttack();
                }
            }
        }
    }
}
