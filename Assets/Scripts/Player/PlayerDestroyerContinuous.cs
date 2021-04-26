using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerDestroyerContinuous : PlayerDestroyer
    {
        [SerializeField]
        private float damageSecond = 1f;

        private float timer = 0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerCharacter>() is PlayerCharacter player)
            {
                player.CurrentPlayerStats.DecreasePlayerHealth(damageValue);
                player.UpdateHpUI();
                player.TriggerHurtAnimation();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerCharacter>() is PlayerCharacter player)
            {
                timer += Time.deltaTime;
                if (timer >= damageSecond)
                {
                    player.CurrentPlayerStats.DecreasePlayerHealth(damageValue);
                    player.UpdateHpUI();
                    player.TriggerHurtAnimation();
                    timer = 0;
                }
            }
        }
    }
}
