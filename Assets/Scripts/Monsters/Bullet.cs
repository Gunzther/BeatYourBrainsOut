using UnityEngine;
namespace BBO.BBO.BulletManagement
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = default;
        [SerializeField]
        private GameObject hitPrefab = default;

        protected Vector3 target = default;
        private Quaternion rot;

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
                if (hitPrefab != null)
                {
                    var hitVFX = Instantiate(hitPrefab, target, rot);
                }
                Destroy(gameObject);
            }

            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
        }

        private void OnCollisionEnter(Collision co)
        {
            ContactPoint contact = co.contacts[0];
            Quaternion tempRot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            rot = tempRot;
        }
    }
}