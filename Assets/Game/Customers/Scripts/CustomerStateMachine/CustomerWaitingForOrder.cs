using System;
public class CustomerWaitingForOrder : CustomerState
{
    public static Action<float> addScore;
    private bool orderReceived;
    public CustomerWaitingForOrder(CustomerStateMachine customerStateMachine) : base(customerStateMachine)
    {
    }

    public override void Start()
    {
        _customerStateMachine.customerController.customerReferences.animations.AnimatorStateUpdate(this.ToString());
        _customerStateMachine.customerController.thisNavMeshAgent.enabled = false;
        _customerStateMachine.customerController.waitingForOrder = true;
        _customerStateMachine.customerController.customerReferences.customerVignette.SetupVignette(_customerStateMachine.customerController.chosenType, _customerStateMachine.customerController.chosenIngredients, 0, true);
        _customerStateMachine.customerController.SendInfoToToDoList();
    }
    public override void StateUpdate()
    {
        if (!_customerStateMachine.customerController.waitingForOrder && !orderReceived) EndOrder();
        if (orderReceived && _customerStateMachine.customerController.customerReferences.animations.animator.GetBool("Release")) FinishEating();
    }

    private void EndOrder()
    {
        PlaySoundDependingOnOrder();
        addScore(_customerStateMachine.customerController.customerReferences.customerData.orderSizesProbabilitiesAndScores[_customerStateMachine.customerController.chosenIngredients.Count - 1].scoreGivenByThisOrderSize);
        _customerStateMachine.customerController.targetedLocation = _customerStateMachine.customerController.exitDoor;
        _customerStateMachine.customerController.thisTable.TableClear(_customerStateMachine.customerController.thisTableSeatId);
        _customerStateMachine.customerController.customerReferences.customerVignette.DeactivateVignette(0);
        _customerStateMachine.customerController.RemoveInfoFromToDoList();
        _customerStateMachine.customerController.customerReferences.animations.animator.SetBool("OrderIsReceived", true);
        _customerStateMachine.customerController.leave = true;
        _customerStateMachine.customerController.customerReferences.customerData.activeOrders--;
        orderReceived = true;
    }

    private void FinishEating()
    {
        RollForTerrain();
        GoToGoToLocation();
    }

    private void RollForTerrain()
    {
        if (_customerStateMachine.customerController.chosenType == OrdersData.OrderTypes.Dish && UnityEngine.Random.Range(1, 100) <= _customerStateMachine.customerController.customerReferences.customerData.mudTerrainSpawnChance)
            SurfaceManager.self.GeneratesSurfaceFromThrownPlate(SurfaceManager.SurfaceType.MUD, _customerStateMachine.gameObject.transform);
        else if (_customerStateMachine.customerController.chosenType == OrdersData.OrderTypes.Drink && UnityEngine.Random.Range(1, 100) <= _customerStateMachine.customerController.customerReferences.customerData.icyTerrainSpawnChance)
            SurfaceManager.self.GeneratesSurfaceFromThrownPlate(SurfaceManager.SurfaceType.ICE, _customerStateMachine.gameObject.transform);
    }
    private void PlaySoundDependingOnOrder()
    {
        if (_customerStateMachine.customerController.chosenType == OrdersData.OrderTypes.Dish) AudioManager.instance.Play("Customer_Consume_dish");
        else AudioManager.instance.Play("Customer_Consume_drink");
    }

    private void GoToGoToLocation()
    {
        _customerStateMachine.SetState(new CustomerGoToLocation(_customerStateMachine));
    }
}
