using Game.Script.Player.Config;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Game.Script.Player.PlayerStates.SubStates
{
    public class PlayerJumpState : PlayerAbilityState
    {
        public int amountOfJumpsLeft;
        
        public PlayerJumpState(PlayerManager playerManager, PlayerStateMachine playerStateMachine, PlayerConfig playerConfig, string animBoolName) : base(playerManager, playerStateMachine, playerConfig, animBoolName)
        {
            amountOfJumpsLeft = playerConfig.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            
            playerManager.SetVelocityY(playerConfig.jumpVelocity);
            IsAbilityDone = true;
            amountOfJumpsLeft--;
            playerManager.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            if(amountOfJumpsLeft > 0) return true;
            return false;
        }
        
        public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerConfig.amountOfJumps;
        
        public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
    }
}
