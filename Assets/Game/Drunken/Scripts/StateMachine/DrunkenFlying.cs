public class DrunkenFlying : DrunkenState
{
    public DrunkenFlying(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    private void Transitions()
    {
        GoToChase();
    }

    private void GoToChase()
    {
        if (!_drunkenStateMachine.drunkenController.drunkenReferences.throwableObject.isFlying)
            _drunkenStateMachine.SetState(new DrunkenChase(_drunkenStateMachine));
    }
}
