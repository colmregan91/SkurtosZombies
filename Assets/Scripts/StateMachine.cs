using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    // COLLECTION OF STATES
    public List<StateTransition> Transitions = new List<StateTransition>();

    public IState CurrentState;

    public List<StateTransition> TransitionsFromCurrentState = new List<StateTransition>();
    public List<StateTransition> TransitionsToAnyState = new List<StateTransition>();

    public Action<IState> OnStateChanged;

    public void AddTransition(IState to, IState from, Func<bool> condition)
    {
        var newTransition = new StateTransition(to, from, condition);

        if (Transitions.Contains(newTransition))
        {
            Debug.LogError("state machine already contains transition into this state");
            return ;
        }
        Debug.Log("added!");
        Transitions.Add(newTransition);
    }

    public void Init(IState startState)
    {
        TransitionsToAnyState = Transitions.Where(T => T.From == null).ToList();
        SetState(startState);
    }


    // SETSTATE METHOD
    public void SetState(IState newState)
    {
        CurrentState?.OnExit();
        Debug.Log($"changing state from {CurrentState} to {newState}");
        CurrentState = newState;
        TransitionsFromCurrentState = Transitions.Where(T => T.From == CurrentState).ToList(); // todo : set up in start
        Debug.Log(Transitions.Count);
        Debug.Log($"checking {TransitionsFromCurrentState.Count} transitions from {CurrentState}");

        OnStateChanged?.Invoke(CurrentState);
        CurrentState.OnEnter();
        // on state changed
    }

    public void Tick()
    {
        if (!CheckForTransition()) // todo : research how to set up via event subscription rather than checking conditions every frame
        {
            CurrentState.OnUpdate();
        }
    }

    public bool CheckForTransition()
    {
        foreach (var cond in TransitionsToAnyState)
        {
            if (cond.Condition())
            {
                SetState(cond.To);
                return true;
            }
        }

        foreach (var cond in TransitionsFromCurrentState)
        {
            if (cond.Condition())
            {
                SetState(cond.To);
                return true;
            }
        }

        return false;
    }
}
