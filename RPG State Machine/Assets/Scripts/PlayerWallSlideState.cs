using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _Player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_Player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (yInput < 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }
        else
        {
            player.SetVelocity(0, rb.velocity.y * .7f);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);

            player.Flip();
        }
    }
}
