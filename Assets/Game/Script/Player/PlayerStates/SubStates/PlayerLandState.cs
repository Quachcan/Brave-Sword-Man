using Game.Script.Player.Config;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SuperStates;

namespace Game.Script.Player.PlayerStates.SubStates
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(PlayerManager playerManager, PlayerStateMachine stateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, stateMachine, playerConfig, animBoolName)
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

            if (XInput != 0)
            {
                playerStateMachine.ChangeState(playerManager.MoveState);
            }
            else if (isAnimationFinished)
            {
                playerStateMachine.ChangeState(playerManager.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
