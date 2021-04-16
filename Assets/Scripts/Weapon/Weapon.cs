using BBO.BBO.GameData;
using BBO.BBO.MonsterManagement;
using UnityEditor;
using UnityEngine;

namespace BBO.BBO.WeaponManagement
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData.Weapon WeaponGO = default;
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

        // Stupid
        private bool stupid = false;

        public void SetDamageValue(int value)
        {
            DamageValue = value;
        }

        public void OnPicked()
        {
            // TODO: change to pooling object
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!stupid && !isIntervalDamage && other.gameObject.GetComponent<MonsterCharacter>() is MonsterCharacter monster)
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
            isIntervalDamage = Type == WeaponData.Type.IntervalDamage;
            isLimitAttacksNumber = Type == WeaponData.Type.LimitAttacksNumber;
            stupid = Type == WeaponData.Type.Stupid;
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
            weaponScript.WeaponGO = (WeaponData.Weapon)EditorGUILayout.EnumPopup("Weapon", weaponScript.WeaponGO);
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
                case WeaponData.Type.Stupid:
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
