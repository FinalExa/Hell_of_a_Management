public class CustomerState : State
{
    protected CustomerStateMachine _customerStateMachine;
    public CustomerState(CustomerStateMachine customerStateMachine)
    {
        _customerStateMachine = customerStateMachine;
        customerStateMachine.stateRef = this.ToString();
    }
}
