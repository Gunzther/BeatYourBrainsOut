using BBO.BBO.GameData;
using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
using BBO.BBO.WeaponManagement;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimatorController playerAnimatorController = default;

        public PlayerStats CurrentPlayerStats { get; private set; }
        public PlayerWeapon CurrentPlayerWeapon { get; private set; }

        private int playerID = default;
        private UIManager uiManager = default;
        private Team team = default;

        // object interaction
        private bool isPicking = false;
        private bool isPlacing = false;
        private bool nearCraftSlot = false;
        private bool canPick => CurrentPlayerWeapon.CurrentWeapon == WeaponData.Weapon.NoWeapon;
        private bool canPlace => CurrentPlayerWeapon.CurrentWeapon != WeaponData.Weapon.NoWeapon;

        public void Reload()
        {
            uiManager = FindObjectOfType<UIManager>();
            uiManager.SetTeamHpMaxValue(team.CurrentTeamHealth);
        }

        public void SetTeam(Team team)
        {
            this.team = team;
        }

        public void UpdateHpUI()
        {
            uiManager.SetTeamHpValue(team.CurrentTeamHealth);
        }

        public void OnPick()
        {
            isPicking = true;
        }

        public void OnPlace()
        {
            if (canPlace)
            {
                if (nearCraftSlot)
                {
                    isPlacing = true;
                }
                else
                {
                    // TODO: place weapon on the floor
                }
            }
        }

        public void TriggerHurtAnimation()
        {
            playerAnimatorController.SetTrigger(PlayerData.HurtTriggerHash);
        }

        private void Awake()
        {
            // TODO: generate player id and assign value to playerID variable
            playerID = 0;
            CurrentPlayerStats = new PlayerStats(playerID);
            CurrentPlayerWeapon = new PlayerWeapon();
        }

        private void OnTriggerStay(Collider other)
        {
            if (isPicking && canPick)
            {
                if (other.GetComponent<WeaponBox>() is WeaponBox weaponBox)
                {
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(weaponBox.Weapon));
                }
                else if (other.GetComponent<Weapon>() is Weapon weapon)
                {
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(weapon.WeaponGO));
                    weapon.OnPicked();
                }
                else if (other.GetComponent<CraftSlot>() is CraftSlot slot && slot.CanPick)
                {
                    WeaponData.Weapon pickedWeapon = slot.OnPicked();
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(pickedWeapon));
                }

                isPicking = false;
            }
            if (other.GetComponent<CraftSlot>() is CraftSlot craftSlot)
            {
                nearCraftSlot = true;

                if (isPlacing && craftSlot.CanPlace && CurrentPlayerWeapon.CurrentWeapon != WeaponData.Weapon.NoWeapon)
                {
                    craftSlot.OnPlaced(CurrentPlayerWeapon.CurrentWeapon);
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(WeaponData.Weapon.NoWeapon));
                    isPlacing = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            nearCraftSlot = false;
            isPicking = false;
            isPlacing = false;
        }
    }
}
