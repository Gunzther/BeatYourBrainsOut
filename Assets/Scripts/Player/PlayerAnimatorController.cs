using BBO.BBO.GameData;
using BBO.BBO.WeaponManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform = default;

        [SerializeField]
        private Texture[] spriteList = default;

        [SerializeField]
        private Texture[] spriteSecondList = default;

        [SerializeField]
        private Animator playerAnimator = default;

        [SerializeField]
        private Weapon weapon = default;

        private Renderer spriteRenderer = default;
        private WeaponData.Weapon currentWeaponName = default;

        private enum AnimationDirection
        {
            NoMovement,
            Front,
            Back,
            SideLeft,
            SideRight
        }

        public void SetTrigger(int triggerHash)
        {
            playerAnimator.SetTrigger(triggerHash);
        }

        public bool IsInState(string stateName)
        {
            return playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public void BeginAttackAction()
        {
            weapon.SetIsStupidValue(false);

            if (WeaponData.IsCloseRangeWeapon(currentWeaponName))
            {
                weapon.gameObject.SetActive(true);
            }
        }

        public void EndAttackAction()
        {
            weapon.SetIsStupidValue(true);
            weapon.gameObject.SetActive(false);
            UpdatePlayerIdleMainTex(currentWeaponName);
        }

        public void UpdatePlayerMovementAnimation(Vector3 inputDirection)
        {
            int triggerHash = PlayerData.IdleTriggerHash;
            playerTransform.localScale = new Vector3(1, 1, 1);

            switch (GetAnimationDirection(inputDirection))
            {
                case AnimationDirection.Front:
                    triggerHash = PlayerData.WalkFrontTriggerHash;
                    break;
                case AnimationDirection.Back:
                    triggerHash = PlayerData.WalkBackTriggerHash;
                    break;
                case AnimationDirection.SideLeft:
                    triggerHash = PlayerData.WalkSideTriggerHash;
                    break;
                case AnimationDirection.SideRight:
                    triggerHash = PlayerData.WalkSideTriggerHash;
                    playerTransform.localScale = new Vector3(-1, 1, 1);
                    break;
            }

            if (!IsInState(triggerHash.ToString()))
            {
                SetTrigger(triggerHash);
            }
        }

        public void TriggerAttackAnimation(WeaponData.Weapon weaponName, Vector3 inputDirection)
        {
            int triggerHash = PlayerData.IdleTriggerHash;
            playerTransform.localScale = new Vector3(1, 1, 1);

            if (WeaponData.IsDirectionalWeapon(weaponName))
            {
                switch (GetAnimationDirection(inputDirection))
                {
                    case AnimationDirection.Front:
                        triggerHash = PlayerData.ShotFrontTriggerHash;
                        break;
                    case AnimationDirection.Back:
                        triggerHash = PlayerData.ShotBackTriggerHash;
                        break;
                    case AnimationDirection.SideLeft:
                        triggerHash = PlayerData.ShotSideTriggerHash;
                        break;
                    case AnimationDirection.SideRight:
                        triggerHash = PlayerData.ShotSideTriggerHash;
                        playerTransform.localScale = new Vector3(-1, 1, 1);
                        break;
                    default:
                        triggerHash = PlayerData.ShotFrontTriggerHash;
                        break;
                }
            }
            else if (WeaponData.IsCastAttackWeapon(weaponName))
            {
                triggerHash = PlayerData.CastAttackTriggerHash;
            }

            if (!IsInState(triggerHash.ToString()))
            {
                SetTrigger(triggerHash);
            }
        }

        public void UpdatePlayerAttackingMainTex(WeaponData.Weapon weaponName)
        {
            switch (weaponName)
            {
                case WeaponData.Weapon.RubberBand:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.RubberBandAttack);
                    break;
                case WeaponData.Weapon.Shield:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.ShieldActive);
                    break;
                case WeaponData.Weapon.Sling:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.SlingActive);
                    break;
            }
        }

        public void UpdatePlayerIdleMainTex(WeaponData.Weapon weaponName)
        {
            // print($"UpdatePlayerIdleMainTex: {weaponName}");
            currentWeaponName = weaponName;

            switch (weaponName)
            {
                case WeaponData.Weapon.Baseballbat:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.Baseball);
                    break;
                case WeaponData.Weapon.Nail:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.Nail);
                    break;
                case WeaponData.Weapon.RubberBand:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.RubberBand);
                    break;
                case WeaponData.Weapon.BaseballbatWithNails:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.MorningStar);
                    break;
                case WeaponData.Weapon.Shield:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.Shield);
                    break;
                case WeaponData.Weapon.Sling:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.Sling);
                    break;
                default:
                    ChangePlayerMainTex(PlayerData.PlayerSprite.Default);
                    break;
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<Renderer>();
        }

        // Change sprite with this fn() 
        private void ChangePlayerMainTex(PlayerData.PlayerSprite playerSprite)
        {
            int spriteNumber = (int)playerSprite;
            spriteRenderer.material.SetTexture("_MainTex", spriteList[spriteNumber]);
            spriteRenderer.material.SetTexture("_SecondTex", spriteSecondList[spriteNumber]);
        }

        private AnimationDirection GetAnimationDirection(Vector3 inputDirection)
        {
            if (inputDirection.z < 0)
            {
                return AnimationDirection.Front;
            }
            else if (inputDirection.z > 0)
            {
                return AnimationDirection.Back;
            }
            else if (inputDirection.x < 0)
            {
                return AnimationDirection.SideLeft;
            }
            else if (inputDirection.x > 0)
            {
                return AnimationDirection.SideRight;
            }

            return AnimationDirection.NoMovement;
        }
    }
}
