using UnityEngine;

namespace BBO.BBO.GameData
{
    public static class AnimationTriggerData
    {
        public static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        public static readonly int WalkFrontTriggerHash = Animator.StringToHash("WalkFront");
        public static readonly int WalkBackTriggerHash = Animator.StringToHash("WalkBack");
        public static readonly int WalkSideTriggerHash = Animator.StringToHash("WalkSide");
        public static readonly int DeadTriggerHash = Animator.StringToHash("Dead");
        public static readonly int HurtTriggerHash = Animator.StringToHash("Hurt");
    }
}
