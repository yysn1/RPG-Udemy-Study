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

        if (player.IsgroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
