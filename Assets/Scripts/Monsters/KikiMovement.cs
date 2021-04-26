using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.MonsterMovement
{
    /// <summary>
    /// A movement that monster run into the closet player.
    /// </summary>
    public class KikiMovement : MonstersMovement
    {
        [SerializeField]
        private Rigidbody rb = default;

        [SerializeField]
        private float speed = default;

        [Header("Animation")]
        public Animator KikiAnimator = default;

        private const float waitSec = 1;
        private const float bounceForce = 2f;
        private const float bounceAttackedForce = 2.5f;
        private const float targetOffset = 0.05f;

        private IEnumerable<PlayerCharacter> players = default;
        private float timer = default;
        private Transform target = default;

        //sound
        SoundManager soundManager = default;

        public override void OnAttackMovement()
        {
            base.OnAttackMovement();
            var forceDirection = (target.transform.position - transform.position);

            if (forceDirection.x >= targetOffset || forceDirection.x <= -targetOffset || forceDirection.z >= targetOffset || forceDirection.z <= -targetOffset)
            {
                rb.AddForce(forceDirection * -bounceForce, ForceMode.Impulse);
            }
        }

        public override void OnAttackedMovement()
        {
            base.OnAttackedMovement();
            KikiAnimator.SetTrigger(MonstersData.HurtTriggerHash);
            rb.AddForce(target.transform.position * -bounceAttackedForce, ForceMode.Impulse);
        }

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            timer = 0;
            target = GetClosetPlayer();

            soundManager = FindObjectOfType<SoundManager>();
        }

        private void Update()
        {
            if (timer > waitSec)
            {
                target = GetClosetPlayer();
                timer = 0;
                soundManager.PlayKikiWalking();
            }

            timer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            AnimateKikiMovement(transform.position.x, transform.position.z, target.position.x, target.position.z);
        }

        private Transform GetClosetPlayer()
        {
            PlayerCharacter closetPlayer = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;

            foreach (PlayerCharacter player in players)
            {
                float dist = Vector3.Distance(player.transform.position, currentPos);

                if (dist < minDist)
                {
                    closetPlayer = player;
                    minDist = dist;
                }
            }

            return closetPlayer.transform;
        }

        private void AnimateKikiMovement(float kikiXPos, float kikiZPos, float targetXPos, float targetZPos)
        {
            int triggerHash = MonstersData.IdleTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (kikiXPos < targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (kikiXPos > targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
            }
            else
            {
                triggerHash = kikiZPos >= targetZPos ? MonstersData.WalkFrontTriggerHash : MonstersData.WalkBackTriggerHash;
            }

            if (!KikiAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                KikiAnimator.SetTrigger(triggerHash);
            }
        }
    }
}
