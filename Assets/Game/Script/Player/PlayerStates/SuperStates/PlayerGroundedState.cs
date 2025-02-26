using Game.Script.Manager;
using Game.Script.Player.Config;
using Game.Script.Player.PlayerFiniteStateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Script.Player.PlayerStates.SuperStates
{
    public class PlayerGroundedState : PlayerState
    {
        protected int XInput;

        private bool jumpInput;
        
        private bool isGrounded;
        public PlayerGroundedState(PlayerManager playerManager, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, playerStateMachine, playerConfig, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter(); 
            playerManager.JumpState.ResetAmountOfJumpsLeft();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            XInput = playerManager.InputHandler.NormalizeInputX; 
            jumpInput = playerManager.InputHandler.JumpInput;

            if (jumpInput && playerManager.JumpState.CanJump())
            {
                playerManager.InputHandler.UseJumpInput();
                playerStateMachine.ChangeState(playerManager.JumpState);
            }else if(!isGrounded)

            {
                playerManager.JumpState.DecreaseAmountOfJumpsLeft();
                playerStateMachine.ChangeState(playerManager.InAirState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            isGrounded = playerManager.CheckIfGrounded();
        }
    }
}
