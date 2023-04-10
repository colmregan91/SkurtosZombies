using System;

public class StateTransition
{
    public IState _to;
    public IState _from;
    public Func<bool> _condition;

    public StateTransition(IState to, IState from, Func<bool> condition)
    {
        _to = to;
        _from = from;
        _condition = condition;
    }
}