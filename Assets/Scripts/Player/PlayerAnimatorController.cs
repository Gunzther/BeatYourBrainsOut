using BBO.BBO.MonsterManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Texture[] spriteList = default;

        [SerializeField]
        private int spriteNumber = 0;

        [SerializeField]
        private Animator playerAnimator = default;

        [SerializeField]
        private GameObject weaponSprite = default;

        [SerializeField]
        private MonsterDestroyer weaponCollider = default;

        private Renderer renderer = default;

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

            weaponSprite.SetActive(false);
        }

        public void EndAttackAction(int isShortWeapon)
        {
            if (isShortWeapon == 1)
            {
                weaponCollider.gameObject.SetActive(false);
            }

            weaponSprite.SetActive(true);
        }

        private void Awake()
        {
            renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            renderer.material.SetTexture("_MainTex", spriteList[spriteNumber < spriteList.Length - 1 ? spriteNumber : spriteList.Length - 1]);
        }
    }
}
