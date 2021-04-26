using BBO.BBO.GameManagement;
using BBO.BBO.PlayerInputSystem;
using BBO.BBO.TeamManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerSmoothController : MonoBehaviour
    {
        //Player ID
        private int playerID = default;
        private int deviceID = default;
        public int DeviceId => deviceID;

        [SerializeField]
        private PlayerVisualsBehaviour playerVisualsBehaviour = default;

        [SerializeField]
        private Camera mainCamera = default;

        [SerializeField]
        private PlayerAnimatorController playerAnimatorController = default;

        [SerializeField]
        private PlayerCharacter playerCharacter = default;

        [SerializeField]
        private PlayerInput playerInput = default;

        [SerializeField]
        private float movementSpeed = 5;

        [SerializeField]
        private float smoothingSpeed = 1;

        private Vector3 rawDirection = default;
        private Vector3 smoothDirection = default;
        private Vector3 movement = default;

        private Vector3 inputDirection;
        private bool hasCurrentInput = false;

        [Header("Physics")]
        [SerializeField]
        private Rigidbody playerRigidbody = default;

        [Header("Input")]
        public bool useOldInputManager = true;

        //Current Control Scheme
        private string currentControlScheme = default;

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

        public void SetupPlayer(int newDeviceID)
        {
            playerID = newDeviceID;
            deviceID = newDeviceID;
            currentControlScheme = playerInput.currentControlScheme;
            playerVisualsBehaviour.SetupBehaviour(playerID, playerInput);
            playerCharacter.SetPlayerId(newDeviceID);
        }

        public void OnAttack(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Debug.Log($"[{nameof(PlayerSmoothController)}] attack!");
                playerCharacter.OnAttack(inputDirection);
            }
            else if (value.canceled)
            {
                Debug.Log($"[{nameof(PlayerSmoothController)}] finished attack!");
                playerCharacter.OnFinishedAttack();
            }
        }

        public void OnPickObject(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Debug.Log($"[{nameof(PlayerSmoothController)}] pick!");
                playerCharacter.OnPick();
            }
        }

        public void OnPlaceObject(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Debug.Log($"[{nameof(PlayerSmoothController)}] place!");
                playerCharacter.OnPlace();
            }
        }

        public void OnCraft(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                Debug.Log($"[{nameof(PlayerSmoothController)}] craft!");
                playerCharacter.OnCraft();
            }
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
                SoundManager.Instance.PlayPlayerWalking();
                movement.Set(smoothDirection.x, 0f, smoothDirection.z);
                movement = movement.normalized * movementSpeed * Time.deltaTime;
                playerRigidbody.MovePosition(transform.position + movement);
            }
        }

        private void AnimatePlayerMovement()
        {
            playerAnimatorController.UpdatePlayerMovementAnimation(inputDirection);
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

        private void RemoveAllBindingOverrides()
        {
            InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
        }

        //This is automatically called from PlayerInput, when the input device has been disconnected and can not be identified
        //IE: Device unplugged or has run out of batteries

        public void OnDeviceLost()
        {
            TeamManager.Instance.RemovePlayer(this);
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
