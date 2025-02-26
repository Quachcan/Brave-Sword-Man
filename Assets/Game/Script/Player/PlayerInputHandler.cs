using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Script.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 RawMovementInput {get; private set;}
        public int NormalizeInputX {get; private set;}
        public int NormalizeInputY {get; private set;}
        public bool JumpInput {get; private set;}
        public bool JumpInputStop {get; private set;}

        [SerializeField] private float inputHoldTime = 0.2f;
        
        private float jumpInputStartTime;

        private void Update()
        {
            CheckJumpInputHoldTime();
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();
            
            NormalizeInputX = (int)(RawMovementInput * Vector2.right).x;
            NormalizeInputY = (int)(RawMovementInput * Vector2.left).y;
            Debug.Log(RawMovementInput);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }

        private void CheckJumpInputHoldTime()
        {
            if (Time.time > jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }

        public void UseJumpInput() => JumpInput = false;
    }
}
