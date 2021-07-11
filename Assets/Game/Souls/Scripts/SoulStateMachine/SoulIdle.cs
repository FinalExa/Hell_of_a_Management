using System;
using UnityEngine;
using UnityEngine.AI;
public class SoulIdle : SoulState
{
    public static Action<SoulController> soulIsIdle;
    public SoulIdle(SoulStateMachine soulStateMachine) : base(soulStateMachine)
    {
    }
    public override void Start()
    {
        _soulStateMachine.soulController.soulReferences.soulAnimations.AnimatorStateUpdate(this.ToString());
        soulIsIdle(_soulStateMachine.soulController);
        StopMovements();
    }
    public override void StateUpdate()
    {
        Transitions();
    }

    private void StopMovements()
    {
        NavMeshAgent thisNavMeshAgent = _soulStateMachine.soulController.thisNavMeshAgent;
        Rigidbody thisRigidbody = _soulStateMachine.soulController.thisRigidbody;
        if (thisNavMeshAgent.isOnNavMesh)
        {
            thisNavMeshAgent.isStopped = true;
            thisNavMeshAgent.velocity = Vector3.zero;
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
        }
    }

    #region Transitions
    private void Transitions()
    {
        GoToGrabbed();
        GoToEscapeThePlayer();
    }
    private void GoToGrabbed()
    {
        if (_soulStateMachine.soulController.soulReferences.soulThrowableObject.IsAttachedToHand) _soulStateMachine.SetState(new SoulGrabbed(_soulStateMachine));
    }
    private void GoToEscapeThePlayer()
    {
        if (_soulStateMachine.soulController.playerIsInRange) _soulStateMachine.SetState(new SoulEscapePlayer(_soulStateMachine));
    }
    #endregion
}
