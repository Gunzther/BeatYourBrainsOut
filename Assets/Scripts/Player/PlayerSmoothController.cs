using BBO.BBO.GameData;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerSmoothController : MonoBehaviour
    {
        //Player ID
        private int playerID;

        [Header("Sub Behaviours")]
        public PlayerVisualsBehaviour playerVisualsBehaviour = default;

        //Current Control Scheme
        private string currentControlScheme;

        [SerializeField]
        private Camera mainCamera = default;

        [SerializeField]
        private PlayerAnimatorController playerAnimatorController = default;

        [Header("Physics")]
        public Rigidbody playerRigidbody = default;

        [Header("Input")]
        public bool useOldInputManager = true;

        private Vector3 inputDirection;
        private bool hasCurrentInput = false;

        [Header("Movement Settings")]
        public PlayerInput playerInput;
        public float movementSpeed = 5;
        public float smoothingSpeed = 1;
        private Vector3 rawDirection = default;
        private Vector3 smoothDirection = default;
        private Vector3 movement = default;

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

        public void SetupPlayer(int newPlayerID)
        {
            playerID = newPlayerID;

            currentControlScheme = playerInput.currentControlScheme;

            playerVisualsBehaviour.SetupBehaviour(playerID, playerInput);
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

            if (!playerAnimatorController.IsInState(triggerHash.ToString()))
            {
                playerAnimatorController.SetTrigger(triggerHash);
            }
        }

        //INPUT SYSTEM AUTOMATIC CALLBACKS --------------

        //This is automatically called from PlayerInput, when the input device has changed
        //(IE: Keyboard -> Xbox Controller)
        public void OnControlsChanged()
        {

            if (playerInput.currentControlScheme != currentControlScheme)
            {
                currentControlScheme = playerInput.currentControlScheme;

                playerVisualsBehaviour.UpdatePlayerVisuals();
                RemoveAllBindingOverrides();
            }
        }

        void RemoveAllBindingOverrides()
        {
            InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
        }

        //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
        //IE: Device unplugged or has run out of batteries

        public void OnDeviceLost()
        {
            playerVisualsBehaviour.SetDisconnectedDeviceVisuals();
        }

        public void OnDeviceRegained()
        {
            StartCoroutine(WaitForDeviceToBeRegained());
        }

        IEnumerator WaitForDeviceToBeRegained()
        {
            yield return new WaitForSeconds(0.1f);
            playerVisualsBehaviour.UpdatePlayerVisuals();
        }

        //Get Data ----
        public int GetPlayerID()
        {
            return playerID;
        }

        public InputActionAsset GetActionAsset()
        {
            return playerInput.actions;
        }

        public PlayerInput GetPlayerInput()
        {
            return playerInput;
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
