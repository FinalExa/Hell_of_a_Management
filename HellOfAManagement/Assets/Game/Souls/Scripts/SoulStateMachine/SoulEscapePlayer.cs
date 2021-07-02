using System;
using UnityEngine;
using UnityEngine.AI;
public class SoulEscapePlayer : SoulState
{
    public static Action<SoulController> soulIsEscapingPlayer;
    public SoulEscapePlayer(SoulStateMachine soulStateMachine) : base(soulStateMachine)
    {
    }
    public override void Start()
    {
        soulIsEscapingPlayer(_soulStateMachine.soulController);
        SetupEscape(false);
    }
    public override void StateUpdate()
    {
        EscapeCheck();
    }
    private void SetupEscape(bool reversePath)
    {
        if (_soulStateMachine.soulController.playerIsInRange != null && _soulStateMachine.soulController.thisNavMeshAgent.enabled == true)
        {
            Vector3 playerPos = _soulStateMachine.soulController.playerIsInRange.transform.position;
            Vector3 soulPos = _soulStateMachine.soulController.gameObject.transform.position;
            Vector3 destination;
            if (!reversePath) destination = new Vector3(soulPos.x + (soulPos.x - playerPos.x), soulPos.y, soulPos.z + (soulPos.z - playerPos.z));
            else destination = new Vector3(soulPos.x - (soulPos.x - playerPos.x), soulPos.y, soulPos.z - (soulPos.z - playerPos.z));
            _soulStateMachine.soulController.thisNavMeshAgent.isStopped = false;
            _soulStateMachine.soulController.thisNavMeshAgent.SetDestination(destination);
        }
    }
    private void EscapeCheck()
    {
        if (_soulStateMachine.soulController.thisNavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete) SetupEscape(false);
        if (_soulStateMachine.soulController.collidedWithOther == true) SetupEscape(true);
        Transitions();
    }
    #region Transitions
    private void Transitions()
    {
        GoToIdle();
        GoToGrabbed();
    }
    private void GoToIdle()
    {
        if (_soulStateMachine.soulController.playerIsInRange == null ||
            !_soulStateMachine.soulController.thisNavMeshAgent.isOnNavMesh)
        {
            _soulStateMachine.SetState(new SoulIdle(_soulStateMachine));
        }
    }
    private void GoToGrabbed()
    {
        if (_soulStateMachine.soulController.soulReferences.soulThrowableObject.isAttachedToHand)
        {
            _soulStateMachine.SetState(new SoulGrabbed(_soulStateMachine));
        }
    }
    #endregion
}
