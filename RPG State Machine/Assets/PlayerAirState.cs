using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _Player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_Player, _stateMachine, _animBoolName)
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

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() && xInput == player.facingDir)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed * .8f, player.rb.velocity.y);
        }
        else
        {
            // 如果没有水平输入，逐渐减速
            player.SetVelocity(Mathf.Lerp(rb.velocity.x, 0, 0.2f), rb.velocity.y);
        }


        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
