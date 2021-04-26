using UnityEngine;

namespace BBO.BBO.GameData
{
    public static class PlayerData
    {
        public const int DefaultHealth = 100;
        public const string PlayerTag = "Player";

        public static readonly int IdleTriggerHash = Animator.StringToHash("Idle");
        public static readonly int WalkFrontTriggerHash = Animator.StringToHash("WalkFront");
        public static readonly int WalkBackTriggerHash = Animator.StringToHash("WalkBack");
        public static readonly int WalkSideTriggerHash = Animator.StringToHash("WalkSide");
        public static readonly int CastAttackTriggerHash = Animator.StringToHash("CastAttack");
        public static readonly int ShotFrontTriggerHash = Animator.StringToHash("ShotFront");
        public static readonly int ShotBackTriggerHash = Animator.StringToHash("ShotBack");
        public static readonly int ShotSideTriggerHash = Animator.StringToHash("ShotSide");
        public static readonly int DeadTriggerHash = Animator.StringToHash("Dead");
        public static readonly int HurtTriggerHash = Animator.StringToHash("Hurt");

        public enum PlayerSprite
        {
            Default = 0,
            Baseball,
            Nail,
            RubberBand,
            RubberBandAttack,
            MorningStar,
            Shield,
            ShieldActive,
            Sling,
            SlingActive
        }

        public enum PlayerColor
        {
            White,
            Red,
            Blue,
            Yellow
        }
    }
}
