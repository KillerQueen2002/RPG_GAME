using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        player.setVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallSideDetected())
            stateMachine.ChangeState(player.wallSide);

        player.setVelocity(player.dashSpeed * player.dashDir, 0);

        Debug.Log("Im going Dash");
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
