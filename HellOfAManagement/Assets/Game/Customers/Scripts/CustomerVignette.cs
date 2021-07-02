using System.Collections.Generic;
using UnityEngine;
public class CustomerVignette : MonoBehaviour
{
    [SerializeField] private float fixRotationX;
    [SerializeField] private float fixRotationY;
    [SerializeField] private float fixPositionX;
    [SerializeField] private float fixPositionZ;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject typeOfOrder;
    [SerializeField] private GameObject[] orderSizes;

    public void SetupVignette(Order.OrderType type, List<SoulType.SoulColor> ingredients)
    {
        canvas.transform.rotation = Quaternion.Euler(fixRotationX, fixRotationY, 0f);
        canvas.transform.position = new Vector3(this.gameObject.transform.position.x + fixPositionX, canvas.transform.position.y, this.gameObject.transform.position.z + fixPositionZ);
        typeOfOrder.transform.Find(type.ToString()).gameObject.SetActive(true);
        int size = ingredients.Count - 1;
        int i = 0;
        foreach (Transform child in orderSizes[size].transform)
        {
            child.gameObject.transform.Find(ingredients[i].ToString()).gameObject.SetActive(true);
            i++;
        }
        orderSizes[size].SetActive(true);
        background.SetActive(true);
    }

    public void DeactivateVignette()
    {
        background.SetActive(false);
        foreach (Transform child in typeOfOrder.transform) child.gameObject.SetActive(false);
        for (int i = 0; i < orderSizes.Length; i++)
        {
            orderSizes[i].SetActive(false);
            foreach (Transform ingredientImageContainer in orderSizes[i].transform)
            {
                foreach (Transform ingredientImage in ingredientImageContainer) ingredientImage.gameObject.SetActive(false);
            }
        }
    }
}
