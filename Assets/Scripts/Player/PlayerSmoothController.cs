using UnityEngine;

public class PlayerSmoothController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [Header("Physics")]
    public Rigidbody playerRigidbody;

    [Header("Input")]
    public bool useOldInputManager = true;

    private Vector3 inputDirection;
    private bool hasCurrentInput = false;

    [Header("Movement Settings")]
    public float movementSpeed = 5;
    public float smoothingSpeed = 1;
    private Vector3 rawDirection;
    private Vector3 smoothDirection;
    private Vector3 movement;

    void Update()
    {
        CalculateMovementInput();
    }

    void FixedUpdate()
    {
        CalculateDesiredDirection();
        ConvertDirectionFromRawToSmooth();
        MoveThePlayer();
    }

    void CalculateMovementInput()
    {

        if (useOldInputManager)
        {
            var v = Input.GetAxisRaw("Vertical");
            var h = Input.GetAxisRaw("Horizontal");
            inputDirection = new Vector3(h, 0, v);
        }

        hasCurrentInput = inputDirection != Vector3.zero;
    }

    void CalculateDesiredDirection()
    {
        //Camera Direction
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        rawDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
    }

    void ConvertDirectionFromRawToSmooth()
    {
        if (hasCurrentInput)
        {
            smoothDirection = Vector3.Lerp(smoothDirection, rawDirection, Time.deltaTime * smoothingSpeed);
        }
        else if (hasCurrentInput)
        {
            smoothDirection = Vector3.zero;
        }

    }

    void MoveThePlayer()
    {
        if (hasCurrentInput)
        {
            movement.Set(smoothDirection.x, 0f, smoothDirection.z);
            movement = movement.normalized * movementSpeed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }

    }
}
