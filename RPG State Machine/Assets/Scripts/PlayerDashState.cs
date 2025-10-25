using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _Player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_Player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0f, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected() && !player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        player.SetVelocity(player.dashDir * player.dashSpeed, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
