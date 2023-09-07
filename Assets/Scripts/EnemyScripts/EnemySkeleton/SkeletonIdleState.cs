using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemySkeleton) : base(_enemy, _stateMachine, _animBoolName, _enemySkeleton)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime ;
    }   

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer > 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
