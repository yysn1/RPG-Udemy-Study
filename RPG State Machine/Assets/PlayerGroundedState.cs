using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _Player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_Player, _stateMachine, _animBoolName)
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

        if (!player.IsgroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsgroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
