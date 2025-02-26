using Game.Script.Player.Config;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Game.Script.Player.PlayerStates.SubStates
{
    public class PlayerInAirState : PlayerState
    {
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");
        private int xInput;
        private bool isGrounded;
        private bool jumpInput;
        private bool jumpInputStop;
        private bool isJumping;


        public PlayerInAirState(PlayerManager playerManager, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, playerStateMachine, playerConfig, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            xInput = playerManager.InputHandler.NormalizeInputX;
            jumpInput = playerManager.InputHandler.JumpInput;
            jumpInputStop = playerManager.InputHandler.JumpInputStop;
            
            CheckJumpMultiplier();

            if (isGrounded && playerManager.CurrentVelocity.y < 0.01f)
            {
                playerStateMachine.ChangeState(playerManager.LandState);
            }
            else if (jumpInput && playerManager.JumpState.CanJump())
            {
                playerStateMachine.ChangeState(playerManager.JumpState);
            }
            else
            {
                playerManager.CheckIfShouldFlip(xInput);
                playerManager.SetVelocityX(playerConfig.movementVelocity * xInput);
                
                playerManager.Anim.SetFloat(YVelocity, playerManager.CurrentVelocity.y);
                playerManager.Anim.SetFloat(XVelocity, Mathf.Abs(playerManager.CurrentVelocity.x) );
            }
        }

        private void CheckJumpMultiplier()
        {
            if (isJumping)
            {
                if (jumpInputStop)
                {
                    playerManager.SetVelocityY(playerManager.CurrentVelocity.y * playerConfig.variableJumpHeightMultiplier);
                    isJumping = false;
                }
                else if(playerManager.CurrentVelocity.y < 0f)
                {
                    isJumping = false;
                }
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
        
        public void SetIsJumping() => isJumping = true;
    }
}
