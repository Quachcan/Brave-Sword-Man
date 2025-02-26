using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currenState {get; private set; }

    public void Initialize(State startingState)
    {
        currenState = startingState;
        currenState.Enter();
    }

    public void ChangeState(State newState)
    {
        currenState.Exit();
        currenState = newState;
        currenState.Enter();
    }
}
