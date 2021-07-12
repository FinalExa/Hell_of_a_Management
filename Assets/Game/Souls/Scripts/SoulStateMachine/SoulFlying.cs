using UnityEngine;
public class SoulFlying : SoulState
{
    public SoulFlying(SoulStateMachine soulStateMachine) : base(soulStateMachine)
    {
    }
    public override void Start()
    {
        _soulStateMachine.soulController.soulReferences.playerInRange.thisTrigger.enabled = false;
        _soulStateMachine.soulController.soulReferences.soulAnimations.AnimatorStateUpdate(this.ToString());
    }
    public override void StateUpdate()
    {
        if (!_soulStateMachine.soulController.soulReferences.soulThrowableObject.isNotGrounded) StopFlight();
    }
    private void StopFlight()
    {
        _soulStateMachine.soulController.thisRigidbody.velocity = Vector3.zero;
        _soulStateMachine.soulController.thisRigidbody.angularVelocity = Vector3.zero;
        _soulStateMachine.soulController.thisNavMeshAgent.enabled = true;
        Transitions();
    }
    #region Transitions
    private void Transitions()
    {
        GoToIdle();
        GoToEscapePub();
    }
    private void GoToIdle()
    {
        if (_soulStateMachine.soulController.SoulIsInsideStorage() || !_soulStateMachine.soulController.thisNavMeshAgent.isOnNavMesh) _soulStateMachine.SetState(new SoulIdle(_soulStateMachine));
    }
    private void GoToEscapePub()
    {
        if (!_soulStateMachine.soulController.SoulIsInsideStorage()) _soulStateMachine.SetState(new SoulEscapePub(_soulStateMachine));
    }
    #endregion
}
