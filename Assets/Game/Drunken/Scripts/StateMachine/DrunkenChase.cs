public class DrunkenChase : DrunkenState
{
    public DrunkenChase(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {
    }

    public override void StateUpdate()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.SetDestination(_drunkenStateMachine.drunkenController.player.transform.position);
    }
}
