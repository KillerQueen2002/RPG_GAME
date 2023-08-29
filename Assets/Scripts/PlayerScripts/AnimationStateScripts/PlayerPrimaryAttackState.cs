using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //dem combo attack
        if(comboCounter > 2 || Time.time >= lastTimeAttacked +comboWindow)
            comboCounter = 0;
        player.anim.SetInteger("ComboCounter", comboCounter);

        //xac dinh attack direction
        float attackDirection = player.facingdir;
        if(xInput!=0)
            attackDirection = xInput;

        //chinh sua de nhan vat khi tan cong se muot hon
        player.setVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y  );
        stateTimer = .1f;
       
    }

    public override void Exit()
    {
        base.Exit();
        //delay runstate neu nhu nguoi choi dang trong attack state
        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        //khong cho player di chuyen khi dang tan cong
        if (stateTimer < 0)
            player.ZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
