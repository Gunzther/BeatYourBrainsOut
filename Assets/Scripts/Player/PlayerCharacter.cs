using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using BBO.BBO.TeamManagement;
using BBO.BBO.WeaponManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimatorController playerAnimatorController = default;

        [SerializeField]
        private PlayerWeapon currentPlayerWeapon = default;

        // TODO: [too lazy todo] move to another class
        [SerializeField]
        private StupidWeapon[] stupidWeapons = default;

        public PlayerStats CurrentPlayerStats { get; private set; }

        private int playerID = default;
        private UIManager uiManager = default;
        private Team team = default;

        // object interaction
        private bool isPicking = false;
        private bool isPlacing = false;
        private bool nearCraftSlot = false;
        private CraftSlot currentCraftSlot = default;
        private bool canPick => currentPlayerWeapon.CurrentWeaponName == WeaponData.Weapon.NoWeapon;
        private bool canPlace => currentPlayerWeapon.CurrentWeaponName != WeaponData.Weapon.NoWeapon;
        private bool canAttack => currentPlayerWeapon.CurrentWeaponName != WeaponData.Weapon.NoWeapon;
        private bool isNotHurt = true;

        // stupid weapon
        private Dictionary<WeaponData.Weapon, Weapon> stupidWeaponPrototypes = default;
        private GameObject stupidContainer = default;

        // crafting
        private CraftTable craftTable = default;

        //sound
        SoundManager soundManager = default;

        public void Reload()
        {
            CurrentPlayerStats.Reset();
            uiManager = FindObjectOfType<UIManager>();
            uiManager.SetTeamHpMaxValue(team.CurrentTeamHealth);
        }

        public void SetTeam(Team team)
        {
            this.team = team;
        }

        public void SetPlayerId(int playerID)
        {
            this.playerID = playerID;
            CurrentPlayerStats = new PlayerStats(playerID);
        }

        public void UpdateHpUI()
        {
            uiManager.SetTeamHpValue(team.CurrentTeamHealth);
        }

        public void OnAttack(Vector3 inputDirection)
        {
            if (canAttack)
            {
                playerAnimatorController.UpdatePlayerAttackingMainTex(currentPlayerWeapon.CurrentWeaponName);
                playerAnimatorController.TriggerAttackAnimation(currentPlayerWeapon.CurrentWeaponName, inputDirection);
            }
        }

        public void OnFinishedAttack()
        {
            playerAnimatorController.UpdatePlayerIdleMainTex(currentPlayerWeapon.CurrentWeaponName);
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
                    PlaceStupidWeapon();
                }
            }
        }

        public void OnCraft()
        {
            if (craftTable != null && craftTable.CanCraft)
            {
                craftTable.Craft();
            }
        }

        public void TriggerHurtAnimation()
        {
            playerAnimatorController.SetTrigger(PlayerData.HurtTriggerHash);
        }

        public void PlayHurtSound()
        {
            soundManager.PlayPlayerHurt();
        }

        public void SetIsNotHurt(bool value)
        {
            this.isNotHurt = value;
        }

        public bool GetIsnotHurt()
        {
            return isNotHurt;
        }

        private void Awake()
        {
            stupidWeaponPrototypes = new Dictionary<WeaponData.Weapon, Weapon>();
            soundManager = FindObjectOfType<SoundManager>();

            currentPlayerWeapon.CurrentWeapon.OnSetPlayerMainTexToDefault += SetPlayerMainTexToDefault;
        }

        private void SetPlayerMainTexToDefault()
        {
            playerAnimatorController.UpdatePlayerIdleMainTex(WeaponData.Weapon.NoWeapon);
        }

        private void OnEnable()
        {
            GenerateStupidWeaponDictionary();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CraftSlot>() is CraftSlot craftSlot)
            {
                currentCraftSlot = craftSlot;
                nearCraftSlot = true;
            }
            if (other.GetComponent<CraftTable>() is CraftTable craftTable)
            {
                this.craftTable = craftTable;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isPicking && canPick)
            {
                if (other.GetComponent<WeaponBox>() is WeaponBox weaponBox)
                {
                    currentPlayerWeapon.SetWeapon(weaponBox.WeaponPrototype);
                }
                else if (other.GetComponent<Weapon>() is Weapon weapon)
                {
                    currentPlayerWeapon.SetWeapon(weapon);
                    weapon.OnPicked();
                }
                else if (other.GetComponent<CraftSlot>() is CraftSlot slot && slot.CanPick)
                {
                    Weapon pickedWeapon = slot.OnPicked();
                    print(pickedWeapon);
                    currentPlayerWeapon.SetWeapon(pickedWeapon);
                }
                else if (other.GetComponent<CraftTable>() is CraftTable table && table.CanPick)
                {
                    Weapon pickedWeapon = table.OnPicked();
                    currentPlayerWeapon.SetWeapon(pickedWeapon);
                }

                playerAnimatorController.UpdatePlayerIdleMainTex(currentPlayerWeapon.CurrentWeaponName);
                isPicking = false;
            }
            if (isPlacing && canPlace)
            {
                if (currentCraftSlot.CanPlace)
                {
                    currentCraftSlot.OnPlaced(currentPlayerWeapon.CurrentWeapon);
                    currentPlayerWeapon.SetWeapon(null);
                }

                playerAnimatorController.UpdatePlayerIdleMainTex(currentPlayerWeapon.CurrentWeaponName);
                isPlacing = false;
            }

            // TODO: should not check in trigger stay loop
            if (currentCraftSlot == null && other.GetComponent<CraftSlot>() is CraftSlot craftSlot)
            {
                currentCraftSlot = craftSlot;
                nearCraftSlot = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isPicking = false;
            isPlacing = false;

            if (other.GetComponent<CraftSlot>())
            {
                nearCraftSlot = false;
                currentCraftSlot = null;
            }
            if (other.GetComponent<CraftTable>())
            {
                craftTable = null;
            }
        }

        private void GenerateStupidWeaponDictionary()
        {
            foreach (StupidWeapon weapon in stupidWeapons)
            {
                stupidWeaponPrototypes.Add(weapon.Weapon, weapon.StupidPrefab);
            }
        }

        private void PlaceStupidWeapon()
        {
            if (stupidContainer == null)
            {
                stupidContainer = new GameObject("StupidContainer");
            }

            if (stupidWeaponPrototypes.TryGetValue(currentPlayerWeapon.CurrentWeaponName, out Weapon weaponPrototype))
            {
                Weapon newStupidWeapon = Instantiate(weaponPrototype, stupidContainer.transform);
                newStupidWeapon.transform.position = transform.position;
                SetNewWeaponValue(newStupidWeapon);
            }

            currentPlayerWeapon.SetWeapon(null);
            playerAnimatorController.UpdatePlayerIdleMainTex(currentPlayerWeapon.CurrentWeaponName);
            isPlacing = false;
        }

        private void SetNewWeaponValue(Weapon newWeapon)
        {
            newWeapon.CopyWeaponValue(currentPlayerWeapon.CurrentWeapon);
        }
    }

    // TODO: [too lazy todo] move to another class
    [Serializable]
    public class StupidWeapon
    {
        public WeaponData.Weapon Weapon = default;
        public Weapon StupidPrefab = default;
    }
}
