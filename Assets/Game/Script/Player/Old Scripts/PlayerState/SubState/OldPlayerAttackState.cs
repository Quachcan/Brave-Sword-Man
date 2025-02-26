using System.Collections;
using System.Collections.Generic;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using UnityEngine;

public class OldPlayerAttackState : OldPlayerAbilitiesState
{

    private Weapon weapon;  
    public OldPlayerAttackState(Player player, OldPlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName) : base(player, stateMachine, playerData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
