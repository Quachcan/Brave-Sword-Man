using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerDashState : OldPlayerAbilitiesState
{
    public bool canDash { get; private set; } 
    private bool isHolding;
    private bool dashInputStop;
    private Vector2 dashDirection; 
    private Vector2 dashDirectionInput;

    private float lastDashTime;
    public OldPlayerDashState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        canDash = false;
        player.inputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * player.facingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.dashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(player.currentVelocity.y > 0 )
        {
            player.SetVelocityY(player.currentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {

        player.animator.SetFloat("yVelocity", player.currentVelocity.y);
        player.animator.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
 
        base.LogicUpdate();

        if(!isExitingState)
        {
            if(isHolding)
            {
                dashDirectionInput = player.inputHandler.dashDirectionInput;
                dashInputStop = player.inputHandler.dashInputStop;

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.dashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStop || Time.unscaledDeltaTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.rb.drag = playerData.drag;
                    player.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.dashDirectionIndicator.gameObject.SetActive(false);
                }
            } 
            else{
                player.SetVelocity(playerData.dashVelocity, dashDirection);

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.rb.drag = 0f;
                    isAbilitiesDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    public bool CheckCanDash()
    {
        return canDash && Time.time >= lastDashTime + playerData.dashCoolDown;
    }

    public void ResetCanDash() => canDash = true;

}
