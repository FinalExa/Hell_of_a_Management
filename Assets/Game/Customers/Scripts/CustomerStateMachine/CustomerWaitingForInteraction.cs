using UnityEngine;
public class CustomerWaitingForInteraction : CustomerState
{

    public CustomerWaitingForInteraction(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.customerReferences.animations.AnimatorStateUpdate(this.ToString());
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = false;
        GenerateOrder();
    }

    public override void StateUpdate()
    {
        if (_customerStateMachine.customerController.interactionReceived && _customerStateMachine.customerController.customerReferences.customerData.activeOrders < _customerStateMachine.customerController.customerReferences.customerData.maxActiveOrdersAtATime)
        {

            _customerStateMachine.customerController.customerReferences.customerData.activeOrders++;
            GoToWaitingForOrder();
        }
    }

    private void GenerateOrder()
    {
        CustomerController customerController = _customerStateMachine.customerController;
        float totalProbability = CalculateTotalProbability();
        float roll = Random.Range(0, totalProbability + 1);
        int orderSize = CheckForRollResult(totalProbability, roll);
        customerController.chosenType = customerController.possibleTypes[Random.Range(0, customerController.possibleTypes.Length)];
        for (int i = 0; i < orderSize; i++)
        {
            customerController.chosenIngredients.Add(customerController.possibleIngredients[Random.Range(0, customerController.possibleIngredients.Length)]);
        }
        customerController.thisTable.AssignOrderToTable(customerController.thisTableSeatId, customerController);
    }

    private float CalculateTotalProbability()
    {
        CustomerData customerData = _customerStateMachine.customerController.customerReferences.customerData;
        float totalProbability = 0;
        for (int i = 0; i < customerData.orderSizesProbabilitiesAndScores.Length; i++)
        {
            totalProbability += customerData.orderSizesProbabilitiesAndScores[i].probabilityOfThisOrderSize;
        }
        return totalProbability;
    }

    private int CheckForRollResult(float totalProbability, float roll)
    {
        CustomerData customerData = _customerStateMachine.customerController.customerReferences.customerData;
        int rollResult = 0;
        float calculator = 0;
        for (int i = 0; i < customerData.orderSizesProbabilitiesAndScores.Length; i++)
        {
            rollResult = i + 1;
            if (roll >= calculator && roll <= calculator + customerData.orderSizesProbabilitiesAndScores[i].probabilityOfThisOrderSize) break;
            else calculator += customerData.orderSizesProbabilitiesAndScores[i].probabilityOfThisOrderSize;
        }
        return rollResult;
    }

    private void GoToWaitingForOrder()
    {
        _customerStateMachine.SetState(new CustomerWaitingForOrder(_customerStateMachine));
    }
}
