public class SoulState : State
{
    protected SoulStateMachine _soulStateMachine;
    public SoulState(SoulStateMachine soulStateMachine)
    {
        _soulStateMachine = soulStateMachine;
        soulStateMachine.stateRef = this.ToString();
        //soulStateMachine.StateNamePrint();
    }
}
