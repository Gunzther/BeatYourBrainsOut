﻿using UnityEngine;

namespace BBO.BBO.GameData
{
    public static class MonstersData
    {
        public static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        public static readonly int WalkFrontTriggerHash = Animator.StringToHash("WalkFront");
        public static readonly int WalkBackTriggerHash = Animator.StringToHash("WalkBack");
        public static readonly int WalkSideTriggerHash = Animator.StringToHash("WalkSide");
        public static readonly int DeadTriggerHash = Animator.StringToHash("Dead");
        public static readonly int HurtTriggerHash = Animator.StringToHash("Hurt");
        public static readonly int AttackTriggerHash = Animator.StringToHash("Attack");

        public static readonly float LaserLifeTime = 5f;
    }
}
