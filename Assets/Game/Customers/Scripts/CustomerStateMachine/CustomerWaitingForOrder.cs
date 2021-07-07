using System;
using System.Collections.Generic;
public class CustomerWaitingForOrder : CustomerState
{
    public static Action<float> addScore;
    public static Action<OrdersData.OrderTypes, List<OrdersData.OrderIngredients>> SendOrderInfosToUI;
    public static Action<OrdersData.OrderTypes, List<OrdersData.OrderIngredients>> RemoveOrderInfosFromUI;
    public CustomerWaitingForOrder(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = false;
        _customerStateMachine.customerController.waitingForOrder = true;
        _customerStateMachine.customerController.customerReferences.customerVignette.SetupVignette(_customerStateMachine.customerController.chosenType, _customerStateMachine.customerController.chosenIngredients, 0, true);
        //SendOrderInfosToUI(_customerStateMachine.customerController.chosenType, _customerStateMachine.customerController.chosenIngredients);
    }

    public override void StateUpdate()
    {
        if (!_customerStateMachine.customerController.waitingForOrder) EndOrder();
    }

    private void EndOrder()
    {
        addScore(_customerStateMachine.customerController.customerReferences.customerData.orderSizesProbabilitiesAndScores[_customerStateMachine.customerController.chosenIngredients.Count - 1].scoreGivenByThisOrderSize);
        _customerStateMachine.customerController.customerReferences.customerVignette.DeactivateVignette(0);
        //RemoveOrderInfosFromUI(_customerStateMachine.customerController.chosenType, _customerStateMachine.customerController.chosenIngredients);
        _customerStateMachine.customerController.targetedLocation = _customerStateMachine.customerController.exitDoor;
        _customerStateMachine.customerController.thisTable.TableClear(_customerStateMachine.customerController.thisTableId);
        _customerStateMachine.customerController.leave = true;
        _customerStateMachine.customerController.customerReferences.customerData.activeOrders--;
        GoToGoToLocation();
    }

    private void GoToGoToLocation()
    {
        _customerStateMachine.SetState(new CustomerGoToLocation(_customerStateMachine));
    }
}
