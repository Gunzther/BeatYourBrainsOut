using UnityEngine;

public class WeaponMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb = default;

    [SerializeField]
    private float force = default;

    private Vector3 defaultDirection = new Vector3(0, 0, -1);

    public void ActiveMovement(Vector3 inputDirection)
    {
        if (inputDirection.x == 0 && inputDirection.z == 0)
        {
            inputDirection = defaultDirection;
        }

        rb.AddForce(inputDirection * force, ForceMode.Impulse);
    }
}
