using System.Collections.Generic;
using UnityEngine;
public class CustomerVignetteToDoList : CustomerVignette
{
    [System.Serializable]
    private struct OrdersInfo
    {
        public OrdersData.OrderTypes type;
        public List<OrdersData.OrderIngredients> ingredients;
        public bool isFilled;
    }
    [SerializeField] private OrdersInfo[] activeOrdersInfo;

    public void AddOrder(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        int indexToActivate = SearchForFirstActiveVignette();
        activeOrdersInfo[indexToActivate] = ComposeOrderInfo(type, ingredients);
        SetupVignette(type, ingredients, indexToActivate, false);
    }

    private int SearchForFirstActiveVignette()
    {
        int indexToActivate;
        for (indexToActivate = 0; indexToActivate < activeOrdersInfo.Length; indexToActivate++)
        {
            if (!vignettes[indexToActivate].isActive) break;
        }
        return indexToActivate;
    }

    public void RemoveOrder(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        OrdersInfo orderInfo = ComposeOrderInfo(type, ingredients);
        for (int i = 0; i < activeOrdersInfo.Length; i++)
        {
            if (activeOrdersInfo[i].type == orderInfo.type && activeOrdersInfo[i].ingredients == orderInfo.ingredients)
            {
                DeactivateVignette(i);
                activeOrdersInfo[i].isFilled = false;
                break;
            }
        }
    }

    private OrdersInfo ComposeOrderInfo(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        OrdersInfo orderInfo;
        orderInfo.type = type;
        orderInfo.ingredients = ingredients;
        orderInfo.isFilled = true;
        return orderInfo;
    }
}
