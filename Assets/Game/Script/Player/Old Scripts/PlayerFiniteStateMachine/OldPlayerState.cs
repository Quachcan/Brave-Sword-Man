using UnityEngine;

namespace Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine
{
    public class OldPlayerState
    {
        protected global::Player player;
        protected OldPlayerStateMachine stateMachine;
        protected PlayerData playerData;

        protected float startTime;

        private string animationBoolName;

        protected bool isAnimationFinish;
        protected bool isExitingState;

    

        public OldPlayerState(global::Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
            this.animationBoolName = animationBoolName;
        }

        public virtual void Enter()
        {
            DoCheck();
            player.animator.SetBool(animationBoolName, true);
            startTime = Time.time;
            Debug.Log (animationBoolName);
            isAnimationFinish = false;
            isExitingState = false;
        }

        public virtual void Exit()
        {
            player.animator.SetBool(animationBoolName, false);
            isExitingState = true;
        }

        public virtual void LogicUpdate()
        {

        }

        public virtual void PhysicUpdate()
        {
            DoCheck();
        }

        public virtual void DoCheck()
        {

        }

        public virtual void AnimationTrigger() { }

        public virtual void AnimationFinishTrigger() => isAnimationFinish = true;
    }
}
