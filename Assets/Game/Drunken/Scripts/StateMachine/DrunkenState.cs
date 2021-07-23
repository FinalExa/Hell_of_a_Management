using UnityEngine;
public class DrunkenState : State
{
    protected DrunkenStateMachine _drunkenStateMachine;
    public DrunkenState(DrunkenStateMachine drunkenStateMachine)
    {
        _drunkenStateMachine = drunkenStateMachine;
        Debug.Log(this);
    }
}
