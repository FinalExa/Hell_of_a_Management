using System.Collections.Generic;
using UnityEngine;
public class CustomerVignette : MonoBehaviour
{
    [SerializeField] protected float fixRotationX;
    [SerializeField] protected float fixRotationY;
    [SerializeField] protected float fixPositionX;
    [SerializeField] protected float fixPositionZ;
    [System.Serializable]
    protected struct Vignette
    {
        public GameObject container;
        public GameObject background;
        public GameObject typeOfOrder;
        public GameObject[] orderSizes;
        public bool isActive;
    }
    [SerializeField] protected Vignette[] vignettes;

    public virtual void SetupVignette(OrdersData.OrderTypes type, List<OrdersData.OrderIngredients> ingredients, int index, bool needsPositionOrRotationFix)
    {
        vignettes[index].isActive = true;
        if (needsPositionOrRotationFix)
        {
            vignettes[index].container.transform.rotation = Quaternion.Euler(fixRotationX, fixRotationY, 0f);
            vignettes[index].container.transform.position = new Vector3(this.gameObject.transform.position.x + fixPositionX, vignettes[index].container.transform.position.y, this.gameObject.transform.position.z + fixPositionZ);
        }
        if (vignettes[index].typeOfOrder != null) vignettes[index].typeOfOrder.transform.Find(type.ToString()).gameObject.SetActive(true);
        int size = ingredients.Count - 1;
        int i = 0;
        foreach (Transform child in vignettes[index].orderSizes[size].transform)
        {
            child.gameObject.transform.Find(ingredients[i].ToString()).gameObject.SetActive(true);
            i++;
        }
        vignettes[index].orderSizes[size].SetActive(true);
        vignettes[index].background.SetActive(true);
    }

    public virtual void DeactivateVignette(int index)
    {
        vignettes[index].isActive = false;
        vignettes[index].background.SetActive(false);
        if (vignettes[index].typeOfOrder != null) foreach (Transform child in vignettes[index].typeOfOrder.transform) child.gameObject.SetActive(false);
        for (int i = 0; i < vignettes[index].orderSizes.Length; i++)
        {
            vignettes[index].orderSizes[i].SetActive(false);
            foreach (Transform ingredientImageContainer in vignettes[index].orderSizes[i].transform)
            {
                foreach (Transform ingredientImage in ingredientImageContainer) ingredientImage.gameObject.SetActive(false);
            }
        }
    }
}
