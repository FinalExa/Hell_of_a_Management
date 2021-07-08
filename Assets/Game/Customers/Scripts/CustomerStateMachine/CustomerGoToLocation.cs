using UnityEngine;
public class CustomerGoToLocation : CustomerState
{
    private bool movingIssued;
    private bool timerIsOver;
    private bool timerStart;
    private float timer;
    public CustomerGoToLocation(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.customerReferences.animations.animator.SetBool("OrderIsReceived", false);
        _customerStateMachine.customerController.customerReferences.animations.animator.SetBool("Release", false);
        _customerStateMachine.customerController.customerReferences.animations.AnimatorStateUpdate(this.ToString());
        timerIsOver = false;
        movingIssued = false;
        timerStart = false;
        timer = _customerStateMachine.navMeshPathTimer;
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = true;
    }
    public override void StateUpdate()
    {
        if (!movingIssued) MoveToTarget();
        if (timerStart) NavMeshTimer();
        if (_customerStateMachine.customerController.thisNavMeshAgent.destination == _customerStateMachine.gameObject.transform.position && movingIssued && timerIsOver) DestinationReached();
    }

    private void MoveToTarget()
    {
        _customerStateMachine.customerController.thisNavMeshAgent.SetDestination(_customerStateMachine.customerController.targetedLocation.transform.position);
        movingIssued = true;
        timerStart = true;
    }

    private void NavMeshTimer()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            timerStart = false;
            timerIsOver = true;
        }
    }

    private void DestinationReached()
    {
        _customerStateMachine.gameObject.transform.LookAt(_customerStateMachine.customerController.thisTable.gameObject.transform.position);
        GoToWaitingForInteraction();
    }

    private void GoToWaitingForInteraction()
    {
        _customerStateMachine.SetState(new CustomerWaitingForInteraction(_customerStateMachine));
    }
}
