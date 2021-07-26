public class DrunkenGrabbed : DrunkenState
{
    public DrunkenGrabbed(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {

    }

    public override void Start()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.enabled = false;
        //_drunkenStateMachine.gameObject.transform.localPosition += new Vector3(0, _drunkenStateMachine.drunkenController.grabHeightOffset, 0);
        _drunkenStateMachine.drunkenController.drunkenReferences.animations.AnimatorStateUpdate(this.ToString());
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
