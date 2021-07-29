public class DrunkenChase : DrunkenState
{
    public DrunkenChase(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {
    }

    public override void Start()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.enabled = true;
        _drunkenStateMachine.drunkenController.navMeshAgent.isStopped = false;
        _drunkenStateMachine.drunkenController.drunkenReferences.animations.AnimatorStateUpdate(this.ToString());
    }

    public override void StateUpdate()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.SetDestination(_drunkenStateMachine.drunkenController.player.transform.position);
        Transitions();
    }

    private void Transitions()
    {
        GoToGrabbed();
        GoToDeny();
    }

    private void GoToGrabbed()
    {
        if (_drunkenStateMachine.drunkenController.drunkenReferences.throwableObject.IsAttachedToHand)
        {
            _drunkenStateMachine.SetState(new DrunkenGrabbed(_drunkenStateMachine));
        }
    }

    private void GoToDeny()
    {
        if (_drunkenStateMachine.drunkenController.playerInRange)
        {
            _drunkenStateMachine.SetState(new DrunkenDeny(_drunkenStateMachine));
        }
    }
}
