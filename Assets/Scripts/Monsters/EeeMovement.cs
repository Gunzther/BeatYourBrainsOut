using BBO.BBO.BulletManagement;
using BBO.BBO.GameData;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.MonsterMovement
{
    /// <summary>
    /// A movement that monster run toward players and stop in some distance then start shooting players with beams.
    /// </summary>
    public class EeeMovement : MonstersMovement
    {
        [SerializeField]
        private Rigidbody rb = default;

        [SerializeField]
        private float speed = default;

        [SerializeField]
        private float stop_distance = default;

        [Header("Animation")]
        [SerializeField]
        private Animator EeeAnimator = default;

        [Header("Bullet")]
        [SerializeField]
        private GameObject firePoint = default;
        [SerializeField]
        private List<GameObject> vfx = new List<GameObject>();
        [SerializeField]
        public float bulletChargeSecond = 2f;
        private GameObject effectToSpawn;
        [SerializeField] 
        private GameObject muzzlePrefab;

        private const float waitSec = 1;
        private const float bounceForce = 2f;
        private const float bounceAttackedForce = 2.5f;
        private const float targetOffset = 0.05f;

        private IEnumerable<PlayerCharacter> players = default;
        private float timer = default;
        private Transform target = default;
        private bool timeToFire = default;
        private int bulletStorage = 0;
        private float bulletTimer = default;
        
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
            EeeAnimator.SetTrigger(MonstersData.HurtTriggerHash);
            rb.AddForce(target.transform.position * -bounceAttackedForce, ForceMode.Impulse);
        }

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            timer = 0;
            target = GetClosetPlayer();
            timeToFire = false;
            effectToSpawn = vfx[0];
        }

        private void Update()
        {
            if (timer > waitSec)
            {
                target = GetClosetPlayer();
                timer = 0;
            }

            if (bulletTimer >= bulletChargeSecond && bulletStorage == 0)
            {
                bulletStorage++;
                bulletTimer = 0;
            }

            timer += Time.deltaTime;
            bulletTimer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            MoveToTarget();

            if (timeToFire)
            {
                fire();
                timeToFire = false;
            }
        }

        private void fire()
        {
            GameObject vfx;
            vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
            Bullet b = vfx.GetComponent<Bullet>();
            b.Target = target.transform.position;
            // var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            // muzzleVFX.transform.forward = gameObject.transform.forward;
        }

        private void MoveToTarget()
        {
            float step = speed * Time.deltaTime;
            float moveDistance = Vector3.Distance(transform.position, target.position);
            if (moveDistance > stop_distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            else
            {
                if (bulletStorage > 0)
                {
                    timeToFire = true;
                    bulletStorage = 0;
                }
            }
            
            AnimateEeeMovement(transform.position.x, transform.position.z, target.position.x, target.position.z);
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

        private void AnimateEeeMovement(float eeeXPos, float eeeZPos, float targetXPos, float targetZPos)
        {
            int triggerHash = MonstersData.IdleTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (Vector3.Distance(transform.position, target.position) <= stop_distance)
            {
                triggerHash = MonstersData.IdleTriggerHash;
            }
            else if (eeeXPos < targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (eeeXPos > targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
            }
            else
            {
                triggerHash = eeeZPos >= targetZPos ? MonstersData.WalkFrontTriggerHash : MonstersData.WalkBackTriggerHash;
            }

            if (!EeeAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                EeeAnimator.SetTrigger(triggerHash);
            }
        }
    }
}
