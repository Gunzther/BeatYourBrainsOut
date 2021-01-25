using System;
using BBO.BBO.GameData;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerSmoothController : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera = default;

        [Header("Physics")]
        public Rigidbody playerRigidbody = default;

        [Header("Input")]
        public bool useOldInputManager = true;

        private Vector3 inputDirection;
        private bool hasCurrentInput = false;

        [Header("Movement Settings")]
        public float movementSpeed = 5;
        public float smoothingSpeed = 1;
        private Vector3 rawDirection = default;
        private Vector3 smoothDirection = default;
        private Vector3 movement = default;

        [Header("Animation")]
        public Animator PlayerAnimator = default;

        //var allGamepads = Gamepad.all;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            CalculateMovementInput();
        }

        private void FixedUpdate()
        {
            CalculateDesiredDirection();
            ConvertDirectionFromRawToSmooth();
            MoveThePlayer();
            AnimatePlayerMovement();
        }

        private void CalculateMovementInput()
        {
            if (useOldInputManager)
            {
                var v = Input.GetAxisRaw("Vertical");
                var h = Input.GetAxisRaw("Horizontal");
                inputDirection = new Vector3(h, 0, v);
            }
            
            hasCurrentInput = inputDirection != Vector3.zero;
        }

        public void OnMove(InputAction.CallbackContext value)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            inputDirection = new Vector3(inputMovement.x, 0, inputMovement.y);
            Debug.Log("moving");
        }

        private void CalculateDesiredDirection()
        {
            //Camera Direction
            var cameraForward = mainCamera.transform.forward;
            var cameraRight = mainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            rawDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
        }

        private void ConvertDirectionFromRawToSmooth()
        {
            if (hasCurrentInput)
            {
                smoothDirection = Vector3.Lerp(smoothDirection, rawDirection, Time.deltaTime * smoothingSpeed);
            }
            else
            {
                smoothDirection = Vector3.zero;
            }
        }

        private void MoveThePlayer()
        {
            if (hasCurrentInput)
            {
                movement.Set(smoothDirection.x, 0f, smoothDirection.z);
                movement = movement.normalized * movementSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + movement);
            }
        }

        private void AnimatePlayerMovement()
        {
            int triggerHash = PlayerData.IdleTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (inputDirection.z < 0)
            {
                triggerHash = PlayerData.WalkFrontTriggerHash;
            }
            else if (inputDirection.z > 0)
            {
                triggerHash = PlayerData.WalkBackTriggerHash;
            }
            else if (inputDirection.x < 0)
            {
                triggerHash = PlayerData.WalkSideTriggerHash;
            }
            else if (inputDirection.x > 0)
            {
                triggerHash = PlayerData.WalkSideTriggerHash;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (!PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                PlayerAnimator.SetTrigger(triggerHash);
            }
        }

        

        ////This is called from Player Input, when a button has been pushed, that correspons with the 'TogglePause' action
        //public void OnTogglePause(InputAction.CallbackContext value)
        //{
        //    if (value.started)
        //    {
        //        GameManager.Instance.TogglePauseState(this);
        //    }
        //}
    }
}
