using UnityEngine;

public class DrunkenGrabbed : DrunkenState
{
    public DrunkenGrabbed(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {

    }

    public override void Start()
    {
        _drunkenStateMachine.drunkenController.navMeshAgent.enabled = false;
        _drunkenStateMachine.gameObject.transform.localPosition = new Vector3(_drunkenStateMachine.gameObject.transform.localPosition.x, _drunkenStateMachine.gameObject.transform.localPosition.y + _drunkenStateMachine.drunkenController.grabHeightOffset, _drunkenStateMachine.gameObject.transform.localPosition.z);
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
