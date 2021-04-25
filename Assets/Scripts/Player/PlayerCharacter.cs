using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
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

        // TODO: [too lazy todo] move to another class
        [SerializeField]
        private StupidWeapon[] stupidWeapons = default;

        public PlayerStats CurrentPlayerStats { get; private set; }
        public PlayerWeapon CurrentPlayerWeapon { get; private set; }

        private int playerID = default;
        private UIManager uiManager = default;
        private Team team = default;

        // object interaction
        private bool isPicking = false;
        private bool isPlacing = false;
        private bool nearCraftSlot = false;
        private CraftSlot currentCraftSlot = default;
        private bool canPick => CurrentPlayerWeapon.CurrentWeaponName == WeaponData.Weapon.NoWeapon;
        private bool canPlace => CurrentPlayerWeapon.CurrentWeaponName != WeaponData.Weapon.NoWeapon;

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

        public void OnAttack()
        {
            playerAnimatorController.ChangePlayerMainTex(PlayerData.PlayerSprite.RubberBandAttack);
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

        private void Awake()
        {
            CurrentPlayerWeapon = new PlayerWeapon();
            stupidWeaponPrototypes = new Dictionary<WeaponData.Weapon, Weapon>();

            soundManager = FindObjectOfType<SoundManager>();
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
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(weaponBox.WeaponName, weaponBox.WeaponPrototype));
                }
                else if (other.GetComponent<Weapon>() is Weapon weapon)
                {
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(weapon.WeaponName, weapon));
                    weapon.OnPicked();
                }
                else if (other.GetComponent<CraftSlot>() is CraftSlot slot && slot.CanPick)
                {
                    (WeaponData.Weapon weaponName, Weapon weapon) pickedWeapon = slot.OnPicked();
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(pickedWeapon.weaponName, pickedWeapon.weapon));
                }
                else if (other.GetComponent<CraftTable>() is CraftTable table && table.CanPick)
                {
                    (WeaponData.Weapon weaponName, Weapon weapon) pickedWeapon = table.OnPicked();
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(pickedWeapon.weaponName, pickedWeapon.weapon));
                }

                isPicking = false;
            }
            if (isPlacing && canPlace)
            {
                if (currentCraftSlot.CanPlace)
                {
                    currentCraftSlot.OnPlaced(CurrentPlayerWeapon.CurrentWeaponName, CurrentPlayerWeapon.CurrentWeapon);
                    playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(WeaponData.Weapon.NoWeapon, null));
                    isPlacing = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            nearCraftSlot = false;
            isPicking = false;
            isPlacing = false;

            if (other.GetComponent<CraftSlot>())
            {
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

            if (stupidWeaponPrototypes.TryGetValue(CurrentPlayerWeapon.CurrentWeaponName, out Weapon weaponPrototype))
            {
                Weapon newStupidWeapon = Instantiate(weaponPrototype, stupidContainer.transform);
                newStupidWeapon.transform.position = transform.position;
                SetNewWeaponValue(newStupidWeapon);
            }

            playerAnimatorController.ChangePlayerMainTex(CurrentPlayerWeapon.SetWeapon(WeaponData.Weapon.NoWeapon, null));
            isPlacing = false;
        }

        private void SetNewWeaponValue(Weapon newWeapon)
        {
            Weapon currentWeapon = CurrentPlayerWeapon.CurrentWeapon;

            switch (newWeapon.Type)
            {
                case WeaponData.Type.IntervalDamage:
                    newWeapon.SetIntervalDamageWeaponValue(currentWeapon.DamageValue, currentWeapon.IntervalSeconds);
                    break;
                case WeaponData.Type.LimitAttacksNumber:
                    newWeapon.SetLimitAttacksWeaponValue(currentWeapon.DamageValue, currentWeapon.AttacksNumber);
                    break;
                case WeaponData.Type.Protected:
                    newWeapon.SetProtectedWeaponValue(currentWeapon.HP);
                    break;

            }
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
