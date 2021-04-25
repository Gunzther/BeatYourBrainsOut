using BBO.BBO.GameData;
using BBO.BBO.MonsterManagement;
using UnityEditor;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class Weapon : MonoBehaviour
    {
        public bool IsStupid = default; // just stupid thing on the floor
        public WeaponData.Weapon WeaponName = default;
        public WeaponData.Type Type = default;
        public int DamageValue = default;
        public float IntervalSeconds = default;
        public int AttacksNumber = default;
        public int HP = default;

        // IntervalDamage
        private bool isIntervalDamage = false;
        private float timer = default;

        // LimitAttacksNumber
        private bool isLimitAttacksNumber = false;
        private int attacksCount = default;

        public void SetLimitAttacksWeaponValue(int damageValue, int attacksNumber)
        {
            DamageValue = damageValue;
            AttacksNumber = attacksNumber;
        }

        public void SetIntervalDamageWeaponValue(int damageValue, float intervalSeconds)
        {
            DamageValue = damageValue;
            IntervalSeconds = intervalSeconds;
        }

        public void SetProtectedWeaponValue(int hp)
        {
            HP = hp;
        }

        public void OnPicked()
        {
            // TODO: change to pooling object
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsStupid)
            {
                if (!isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
                {
                    monster.DecreaseMonsterHp(DamageValue);
                    monster.OnAttacked();
                }
                if (isLimitAttacksNumber && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter)
                {
                    attacksCount++;

                    if (attacksCount == AttacksNumber)
                    {
                        // TODO: change to pooling objects
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (!IsStupid)
            {
                if (isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
                {
                    monster.DecreaseMonsterHp(DamageValue);
                    monster.OnAttacked();
                }
            }
        }

        private void Start()
        {
            isIntervalDamage = Type == WeaponData.Type.IntervalDamage;
            isLimitAttacksNumber = Type == WeaponData.Type.LimitAttacksNumber;
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
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Weapon))]
    public class WeaponScriptEditor : Editor
    {
        private Weapon weaponScript = default;

        public override void OnInspectorGUI()
        {
            weaponScript = target as Weapon;
            weaponScript.IsStupid = EditorGUILayout.Toggle("Is stupid weapon", weaponScript.IsStupid);
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
