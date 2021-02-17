using BBO.BBO.GameData;
using BBO.BBO.MonsterManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Texture[] spriteList = default;

        [SerializeField]
        private Animator playerAnimator = default;

        [SerializeField]
        private MonsterDestroyer weaponCollider = default;

        private Renderer spriteRenderer = default;

        public void SetTrigger(int triggerHash)
        {
            playerAnimator.SetTrigger(triggerHash);
        }

        public bool IsInState(string stateName)
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public void BeginAttackAction(int shortWeaponDamageValue)
        {
            if (shortWeaponDamageValue > 0)
            {
                //set damage value depands on weapon
                weaponCollider.SetDamageValue(shortWeaponDamageValue);
                weaponCollider.gameObject.SetActive(true);
            }
        }

        public void EndAttackAction(int isShortWeapon)
        {
            if (isShortWeapon == 1)
            {
                weaponCollider.gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<Renderer>();
        }

        // Change sprite with this fn() 
        public void ChangePlayerMainTex(PlayerSprite player)
        {
            int spriteNumber = (int) player;
            spriteRenderer.material.SetTexture("_MainTex", spriteList[spriteNumber]);
        }
    }

    public enum PlayerSprite
    {
        Default = 0,
        Baseball,
        Nail,
        RubberBandDefault,
        RubberBandAttack
    }
}
