﻿using System.Collections.Generic;
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
        int indexToActivate = SearchForFirstActiveVignette();
        SetupVignette(type, ingredients, indexToActivate, false);
    }

    private int SearchForFirstActiveVignette()
    {
        int indexToActivate;
        for (indexToActivate = 0; indexToActivate < activeOrdersInfo.Count; indexToActivate++)
        {
            if (!vignettes[indexToActivate].isActive) break;
        }
        return indexToActivate;
    }

    private void RemoveOrder(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients)
    {
        OrdersInfo orderInfo = ComposeOrderInfo(type, ingredients);
        for (int i = 0; i < activeOrdersInfo.Count; i++)
        {
            if (activeOrdersInfo[i].type == orderInfo.type && activeOrdersInfo[i].ingredients == orderInfo.ingredients)
            {
                print(i);
                DeactivateVignette(i);
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
