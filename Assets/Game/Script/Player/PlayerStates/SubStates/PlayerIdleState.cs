using Game.Script.Manager;
using Game.Script.Player.Config;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SuperStates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Script.Player.PlayerStates.SubStates
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerManager playerManager, PlayerStateMachine stateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, stateMachine, playerConfig, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerManager.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (XInput != 0)
            {
                playerStateMachine.ChangeState(playerManager.MoveState);
            }
        }
        
        
    }
}
