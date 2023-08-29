using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currenState { get; private set; }

    public void Initialize(PlayerState _startState)
    {
        currenState = _startState;
        currenState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currenState.Exit();
        currenState = _newState;
        currenState.Enter();
    }
}
