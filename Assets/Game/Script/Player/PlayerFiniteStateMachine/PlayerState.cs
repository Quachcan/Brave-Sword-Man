using Game.Script.Manager;
using Game.Script.Player.Config;
using Game.Script.Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace Game.Script.Player.PlayerStates
{
    public class PlayerState
    {
        protected PlayerManager playerManager;
        protected PlayerStateMachine playerStateMachine;
        protected PlayerConfig playerConfig;

        protected float startTime;
        protected bool isAnimationFinished;

        private string animBoolName;

        public PlayerState(PlayerManager playerManager, PlayerStateMachine playerStateMachine,
            PlayerConfig playerConfig, string animBoolName)
        {
            this.playerManager = playerManager;
            this.playerStateMachine = playerStateMachine;
            this.playerConfig = playerConfig;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            // Gọi animation bool name từ animator trong playerManager
            playerManager.Anim.SetBool(animBoolName, true);
            DoChecks();
            startTime = Time.time;
            isAnimationFinished = false;
        }

        public virtual void Exit()
        {
            playerManager.Anim.SetBool(animBoolName, false);
        }

        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { DoChecks(); }
        public virtual void DoChecks() { }

        public virtual void AnimationTrigger() { }
        public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    }
}
