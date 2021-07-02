using UnityEngine;
public class CustomerWaitingForInteraction : CustomerState
{
    public CustomerWaitingForInteraction(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = false;
        GenerateOrder();
    }

    public override void StateUpdate()
    {
        if (_customerStateMachine.customerController.interactionReceived) GoToWaitingForOrder();
    }

    private void GenerateOrder()
    {
        CustomerController customerController = _customerStateMachine.customerController;
        customerController.chosenType = customerController.possibleTypes[Random.Range(0, customerController.possibleTypes.Length)];
        int orderSize = Random.Range(1, 4);
        for (int i = 0; i < orderSize; i++)
        {
            customerController.chosenIngredients.Add(customerController.possibleIngredients[Random.Range(0, customerController.possibleIngredients.Length)]);
        }
        customerController.thisTable.AssignOrderToTable(customerController.thisTableId, customerController);
    }

    private void GoToWaitingForOrder()
    {
        _customerStateMachine.SetState(new CustomerWaitingForOrder(_customerStateMachine));
    }
}
