using System;

public class StateTransition
{
    public IState To;
    public IState From;
    public Func<bool> Condition;

    public StateTransition(IState to, IState from, Func<bool> condition)
    {
        To = to;
        From = from;
        Condition = condition;
    }
}