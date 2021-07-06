using System.Collections.Generic;
using UnityEngine;
public class CustomerVignetteToDoList : CustomerVignette
{
    [System.Serializable]
    private struct OrdersInfo
    {
        public OrdersData.OrderTypes type;
        public List<OrdersData.OrderIngredients> ingredients;
    }
    [SerializeField] private List<OrdersInfo> activeOrdersInfo;

    private void Awake()
    {
        CustomerWaitingForOrder.SendOrderInfosToUI += AddOrder;
        CustomerWaitingForOrder.RemoveOrderInfosFromUI += RemoveOrder;
    }

    private void AddOrder(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {

        activeOrdersInfo.Add(ComposeOrderInfo(type, ingredients));
        SetupVignette(type, ingredients);
    }

    private void RemoveOrder(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        OrdersInfo orderInfo = ComposeOrderInfo(type, ingredients);
        for (int i = 0; i < activeOrdersInfo.Count; i++)
        {
            if (activeOrdersInfo[i].type == orderInfo.type && activeOrdersInfo[i].ingredients == orderInfo.ingredients)
            {
                activeOrdersInfo.RemoveAt(i);
                break;
            }
        }
    }

    private OrdersInfo ComposeOrderInfo(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        OrdersInfo orderInfo;
        orderInfo.type = type;
        orderInfo.ingredients = ingredients;
        return orderInfo;
    }
}
