﻿using BBO.BBO.GameData;
using UnityEditor;
using UnityEngine;

namespace BBO.BBO.MonsterManagement
{
    public class Weapon : MonoBehaviour
    {
        public WeaponsData.Type Type = default;
        public int DamageValue = default;
        public float IntervalSeconds = default;
        public int AttacksNumber = default;
        public int HP = default;

        private bool isIntervalDamage = false;
        private float timer = default;

        private bool isLimitAttacksNumber = false;
        private int attacksCount = default;

        public void SetDamageValue(int value)
        {
            DamageValue = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
            {
                monster.DecreaseMonsterHp(DamageValue);
                monster.OnAttacked();
            }
            if (isLimitAttacksNumber)
            {
                attacksCount++;

                if (attacksCount == AttacksNumber)
                {
                    // TODO: change to pooling objects
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
            {
                monster.DecreaseMonsterHp(DamageValue);
                monster.OnAttacked();
            }
        }

        private void Start()
        {
            isIntervalDamage = Type == WeaponsData.Type.IntervalDamage;
            isLimitAttacksNumber = Type == WeaponsData.Type.LimitAttacksNumber;
        }

        private void Update()
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

#if UNITY_EDITOR
    [CustomEditor(typeof(Weapon))]
    public class WeaponScriptEditor : Editor
    {
        private Weapon weaponScript = default;

        public override void OnInspectorGUI()
        {
            weaponScript = target as Weapon;
            weaponScript.Type = (WeaponsData.Type)EditorGUILayout.EnumPopup("Type", weaponScript.Type);

            switch (weaponScript.Type)
            {
                case WeaponsData.Type.IntervalDamage:
                    ShowDamageValue();
                    ShowIntervalSeconds();
                    break;
                case WeaponsData.Type.LimitAttacksNumber:
                    ShowDamageValue();
                    ShowAttacksNumber();
                    break;
                case WeaponsData.Type.Protected:
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