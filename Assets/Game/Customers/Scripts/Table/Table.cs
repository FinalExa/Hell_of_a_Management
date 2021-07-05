using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    public SeatInfo[] seatInfo;

    private void Start()
    {
        AssignSelf();
    }
    private void AssignSelf()
    {
        for (int i = 0; i < seatInfo.Length; i++)
        {
            seatInfo[i].thisTable = this;
            seatInfo[i].thisId = i;
        }
    }
    public void AssignOrderToTable(int id, CustomerController customer)
    {
        seatInfo[id].orderType = customer.chosenType;
        seatInfo[id].ingredients = customer.chosenIngredients;
    }
    public void TableClear(int id)
    {
        seatInfo[id].ingredients.Clear();
    }

    public void RecipeCheck(Order order)
    {
        for (int i = 0; i < seatInfo.Length; i++)
        {
            if (seatInfo[i].orderType == order.thisOrderType && seatInfo[i].ingredients.Count == order.thisOrderIngredients.Count && seatInfo[i].customer.waitingForOrder)
            {
                if (ArrayContentsAreTheSame(i, order))
                {
                    seatInfo[i].customer.waitingForOrder = false;
                    order.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    private bool ArrayContentsAreTheSame(int index, Order order)
    {
        bool contentsAreTheSame = false;
        if (Enumerable.SequenceEqual(seatInfo[index].ingredients.OrderBy(e => e), order.thisOrderIngredients.OrderBy(e => e))) contentsAreTheSame = true;
        return contentsAreTheSame;
    }
}
[System.Serializable]
public class SeatInfo
{
    public GameObject seatTarget;
    public OrdersData.OrderTypes orderType;
    public List<OrdersData.OrderIngredients> ingredients;
    public CustomerController customer;
    public Table thisTable;
    public int thisId;
}
