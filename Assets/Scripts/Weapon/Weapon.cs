using BBO.BBO.GameData;
using BBO.BBO.MonsterManagement;
using BBO.BBO.PlayerManagement;
using System;
using UnityEditor;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class Weapon : MonoBehaviour
    {
        public PlayerCharacter PlayerIdentify = default;
        public bool IsStupid = false; // just stupid thing on the floor
        public bool IsPlayerWeaponDataCache = false;
        public WeaponData.Weapon WeaponName = default;
        public WeaponData.Type Type = default;
        public int DamageValue = default;
        public float IntervalSeconds = default;
        public int AttacksNumber = default;
        public int HP = default;

        public Action OnSetPlayerMainTexToDefault = default;
        public Action OnAttackMonster = default;

        // IntervalDamage
        private bool isIntervalDamage = false;
        private float timer = default;

        // LimitAttacksNumber
        private bool isLimitAttacksNumber = false;

        public void CopyWeaponValue(Weapon weapon)
        {
            if (weapon != null)
            {
                IsStupid = weapon.IsStupid;
                WeaponName = weapon.WeaponName;
                Type = weapon.Type;
                DamageValue = weapon.DamageValue;
                IntervalSeconds = weapon.IntervalSeconds;
                AttacksNumber = weapon.AttacksNumber;
                HP = weapon.HP;

                ReloadValue();
            }
            else
            {
                ResetWeaponValue();
            }
        }

        public void OnPicked()
        {
            // TODO: change to pooling object
            Destroy(gameObject);
        }

        public void OnAttack()
        {
            if (isLimitAttacksNumber)
            {
                AttacksNumber--;

                if (AttacksNumber == 0)
                {
                    DestroyWeapon();
                }
            }
            else if (isIntervalDamage)
            {
                DestroyWeapon();
            }
        }

        public void SetIsStupidValue(bool isStupid)
        {
            IsStupid = isStupid;
        }

        public void ResetWeaponValue()
        {
            IsStupid = false;
            WeaponName = default;
            Type = default;
            DamageValue = default;
            IntervalSeconds = default;
            AttacksNumber = default;
            HP = default;

            ReloadValue();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsStupid)
            {
                if (other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
                {
                    UpdatePlayerDealDamageScore(monster.DecreaseMonsterHp(DamageValue));
                    monster.OnAttacked();
                }
                if (isLimitAttacksNumber && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter)
                {
                    AttacksNumber--;

                    if (AttacksNumber == 0)
                    {
                        DestroyWeapon();
                    }
                }
                else if (transform.CompareTag(WeaponData.OneTimeWeaponTag) && !other.CompareTag(PlayerData.PlayerTag))
                {
                    print($"destroy by: {other.name}");
                    Destroy(gameObject);  // hit anything except monster, should be destroyed
                }
            }
        }

        private void Start()
        {
            ReloadValue();
        }

        private void Update()
        {
            if (!IsStupid)
            {
                if (isIntervalDamage)
                {
                    timer += Time.deltaTime;

                    if (timer > IntervalSeconds)
                    {
                        // TODO: change to pooling objects
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void ReloadValue()
        {
            isIntervalDamage = Type == WeaponData.Type.IntervalDamage;
            isLimitAttacksNumber = Type == WeaponData.Type.LimitAttacksNumber;
        }

        private void DestroyWeapon()
        {
            if (IsPlayerWeaponDataCache)
            {
                ResetWeaponValue();
                OnSetPlayerMainTexToDefault?.Invoke();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void UpdatePlayerDealDamageScore(int damageValue)
        {
            if (PlayerIdentify != null)
            {
                PlayerIdentify.CurrentPlayerStats.UpdateDamageDealScore(damageValue);
            }

            OnAttackMonster?.Invoke();
            print($"[{nameof(Weapon)}] damage score +{damageValue}");
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Weapon))]
    public class WeaponScriptEditor : Editor
    {
        private Weapon weaponScript = default;

        public override void OnInspectorGUI()
        {
            weaponScript = target as Weapon;
            weaponScript.PlayerIdentify = (PlayerCharacter)EditorGUILayout.ObjectField("Player Identify", weaponScript.PlayerIdentify, typeof(PlayerCharacter), true);
            weaponScript.IsStupid = EditorGUILayout.Toggle("Is Stupid Weapon", weaponScript.IsStupid);
            weaponScript.IsPlayerWeaponDataCache = EditorGUILayout.Toggle("Is Player Weapon Data Cache", weaponScript.IsPlayerWeaponDataCache);
            weaponScript.WeaponName = (WeaponData.Weapon)EditorGUILayout.EnumPopup("Weapon", weaponScript.WeaponName);
            weaponScript.Type = (WeaponData.Type)EditorGUILayout.EnumPopup("Type", weaponScript.Type);

            switch (weaponScript.Type)
            {
                case WeaponData.Type.IntervalDamage:
                    ShowDamageValue();
                    ShowIntervalSeconds();
                    break;
                case WeaponData.Type.LimitAttacksNumber:
                    ShowDamageValue();
                    ShowAttacksNumber();
                    break;
                case WeaponData.Type.Protected:
                    ShowHP();
                    break;
                default:
                    ShowDamageValue();
                    break;
            }

            if (GUI.changed) EditorUtility.SetDirty(weaponScript);
        }

        private void ShowDamageValue() => weaponScript.DamageValue = EditorGUILayout.IntField("Damage Value", weaponScript.DamageValue);
        private void ShowIntervalSeconds() => weaponScript.IntervalSeconds = EditorGUILayout.FloatField("Interval Seconds", weaponScript.IntervalSeconds);
        private void ShowAttacksNumber() => weaponScript.AttacksNumber = EditorGUILayout.IntField("Attacks Number", weaponScript.AttacksNumber);
        private void ShowHP() => weaponScript.HP = EditorGUILayout.IntField("HP", weaponScript.HP);
    }
#endif
}
