using System;

public class DrunkenGrabbed : DrunkenState
{
    public DrunkenGrabbed(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {

    }

    public override void Start()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.enabled = false;
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    private void Transitions()
    {
        GoToFlying();
    }

    private void GoToFlying()
    {
        if (_drunkenStateMachine.drunkenController.drunkenReferences.throwableObject.isFlying)
        {
            _drunkenStateMachine.SetState(new DrunkenFlying(_drunkenStateMachine));
        }
    }
}
