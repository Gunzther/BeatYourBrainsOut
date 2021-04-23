using UnityEngine;

namespace BBO.BBO.BulletManagement
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = default;
        protected Vector3 target = default;
        public Vector3 Target
        {
            get => target;
            set => target = value;
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        protected virtual void MoveToTarget()
        {
            Vector3 currentPos = transform.position;

            if (currentPos == target)
            {
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
        }
    }
}