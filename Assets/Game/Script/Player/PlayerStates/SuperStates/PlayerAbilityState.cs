using Game.Script.Player.Config;
using Game.Script.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace Game.Script.Player.PlayerStates.SuperStates
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool IsAbilityDone;
        private bool isGrounded;
        public PlayerAbilityState(PlayerManager playerManager, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, playerStateMachine, playerConfig, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            IsAbilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (IsAbilityDone)
            {
                if (isGrounded && playerManager.CurrentVelocity.y < 0.01f)
                {
                    playerStateMachine.ChangeState(playerManager.IdleState);
                }
                else
                {
                    playerStateMachine.ChangeState(playerManager.InAirState);
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
    }
}
