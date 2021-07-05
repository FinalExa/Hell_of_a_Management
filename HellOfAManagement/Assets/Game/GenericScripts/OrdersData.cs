using UnityEngine;

[CreateAssetMenu(fileName = "OrdersData", menuName = "ScriptableObjects/OrdersData", order = 8)]
public class OrdersData : ScriptableObject
{
    public enum OrderTypes { Dish, Drink }
    public OrderTypes[] orderTypes;
    public enum OrderIngredients { Red, Blue, Yellow, Green, Purple }
    public OrderIngredients[] orderIngredients;
}
