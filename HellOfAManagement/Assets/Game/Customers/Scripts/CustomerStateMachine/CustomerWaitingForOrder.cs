﻿using System;
public class CustomerWaitingForOrder : CustomerState
{
    public static Action<float> addScore;
    public CustomerWaitingForOrder(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = false;
        _customerStateMachine.customerController.waitingForOrder = true;
        _customerStateMachine.customerController.customerReferences.customerVignette.SetupVignette(_customerStateMachine.customerController.chosenType, _customerStateMachine.customerController.chosenIngredients);
    }

    public override void StateUpdate()
    {
        if (!_customerStateMachine.customerController.waitingForOrder) EndOrder();
    }

    private void EndOrder()
    {
        addScore(_customerStateMachine.customerController.customerReferences.customerData.orderSizesProbabilitiesAndScores[_customerStateMachine.customerController.chosenIngredients.Count - 1].scoreGivenByThisOrderSize);
        _customerStateMachine.customerController.customerReferences.customerVignette.DeactivateVignette();
        _customerStateMachine.customerController.targetedLocation = _customerStateMachine.customerController.exitDoor;
        _customerStateMachine.customerController.thisTable.TableClear(_customerStateMachine.customerController.thisTableId);
        _customerStateMachine.customerController.leave = true;
        GoToGoToLocation();
    }

    private void GoToGoToLocation()
    {
        _customerStateMachine.SetState(new CustomerGoToLocation(_customerStateMachine));
    }
}
