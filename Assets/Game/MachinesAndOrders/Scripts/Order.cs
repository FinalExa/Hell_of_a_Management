using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public OrdersData.OrderTypes thisOrderType;
    public List<OrdersData.OrderIngredients> thisOrderIngredients;
    private ThrowableObject throwableObject;

    private void Awake()
    {
        throwableObject = this.gameObject.GetComponent<ThrowableObject>();
    }
    public void SetupOrderIngredients(List<OrdersData.OrderIngredients> ingredients)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            thisOrderIngredients.Add(ingredients[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Table") && throwableObject.isFlying)
        {
            Table otherTable = other.GetComponent<Table>();
            otherTable.RecipeCheck(this);
        }
    }
}
