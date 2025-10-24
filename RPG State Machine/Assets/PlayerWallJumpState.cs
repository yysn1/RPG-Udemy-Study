using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private float wallJumpDirection;
    private float controlLockTime = 0.2f;

    public PlayerWallJumpState(Player _Player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_Player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .4f;
        wallJumpDirection = -player.facingDir;
        controlLockTime = .2f;

        player.SetVelocity(wallJumpDirection * 10f, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        controlLockTime -= Time.deltaTime;

        if (controlLockTime > 0)
        {
            player.SetVelocity(wallJumpDirection * 10f, rb.velocity.y);
        }
        else if (controlLockTime < 0 && stateTimer > 0)
        {
            // 锁定结束后，如果有输入就响应，没有就保持速度
            if (xInput != 0)
            {
                player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
            }
            else
            {
                player.SetVelocity(rb.velocity.x, rb.velocity.y);
            }
        }

        if (stateTimer <= 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
