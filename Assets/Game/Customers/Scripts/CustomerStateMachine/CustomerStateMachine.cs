using UnityEngine;
public class CustomerStateMachine : StateMachine
{
    [HideInInspector] public CustomerController customerController;
    public float navMeshPathTimer;

    private void Awake()
    {
        customerController = this.gameObject.GetComponent<CustomerController>();
    }

    private void OnEnable()
    {
        customerController.targetedLocation = customerController.seatToTake;
        SetState(new CustomerGoToLocation(this));
    }
    public override void SetState(State state)
    {
        base.SetState(state);
        customerController.curState = state.ToString();
    }
}
