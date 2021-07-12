using UnityEngine;
using UnityEngine.AI;
public class SoulEscapePub : SoulState
{
    public SoulEscapePub(SoulStateMachine soulStateMachine) : base(soulStateMachine)
    {
    }
    public override void Start()
    {
        _soulStateMachine.soulController.soulReferences.playerInRange.thisTrigger.enabled = false;
        _soulStateMachine.soulController.soulReferences.soulAnimations.AnimatorStateUpdate(this.ToString());
        SetupEscapePub();
    }
    private void SetupEscapePub()
    {
        NavMeshAgent agent = _soulStateMachine.soulController.thisNavMeshAgent;
        Vector3 exitPos = _soulStateMachine.soulController.exit.transform.position;
        agent.SetDestination(exitPos);
    }
    public override void StateUpdate()
    {
        CheckForEscapeSuccess();
        Transitions();
    }

    private void CheckForEscapeSuccess()
    {
        if (_soulStateMachine.soulController.isInsideExitDoorCollider) _soulStateMachine.gameObject.SetActive(false);
    }
    #region Transitions
    private void Transitions()
    {
        GoToGrabbed();
    }
    private void GoToGrabbed()
    {
        if (_soulStateMachine.soulController.soulReferences.soulThrowableObject.IsAttachedToHand) _soulStateMachine.SetState(new SoulGrabbed(_soulStateMachine));
    }
    #endregion
}
