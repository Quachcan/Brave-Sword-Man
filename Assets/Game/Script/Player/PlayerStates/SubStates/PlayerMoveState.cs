using Game.Script.Manager;
using Game.Script.Player.Config;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Game.Script.Player.PlayerStates.SubStates
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerManager playerManager, PlayerStateMachine stateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, stateMachine, playerConfig, animBoolName)
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
            
            playerManager.CheckIfShouldFlip(XInput);
            
            playerManager.SetVelocityX(playerConfig.movementVelocity * XInput);
            if (XInput == 0)
            {
                playerStateMachine.ChangeState(playerManager.IdleState);
            }
        }
        
    }
}
