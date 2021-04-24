using BBO.BBO.GameData;
using BBO.BBO.PlayerManagement;
using UnityEngine;

namespace BBO.BBO.BulletManagement
{
    public class Laser : Bullet
    {
        [SerializeField]
        private LineRenderer lineRenderer = default;
        [SerializeField]
        private GameObject collider = default;

        private float timer = 0;
        private GameObject startPosition = default;
        private PlayerCharacter playerTarget = default;
        private Vector3 playerOldPos = default;

        public void SetLaserTarget(GameObject start, PlayerCharacter target)
        {
            startPosition = start;
            playerTarget = target;
            playerOldPos = target.transform.position;
        }

        protected override void MoveToTarget()
        {
            if (!lineRenderer.enabled) lineRenderer.enabled = true;
            Vector3 s = startPosition.transform.position;
            Vector3 p = playerTarget.transform.position;
            Vector3 endLine = Vector3.MoveTowards(playerOldPos, p, moveSpeed);

            lineRenderer.SetPosition(0, new Vector3(s.x, s.y, s.z));
            lineRenderer.SetPosition(1, endLine);
            playerOldPos = endLine;
            collider.transform.position = endLine;
        }

        private void FixedUpdate()
        {
            MoveToTarget();
            timer += Time.deltaTime;

            if (timer >= MonstersData.LaserLifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}