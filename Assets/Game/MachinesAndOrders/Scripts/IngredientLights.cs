using UnityEngine;

public class IngredientLights : MonoBehaviour, IHaveIngredientLights
{
    [SerializeField] private Renderer[] lights;
    [SerializeField] private Color baseColor;
    [SerializeField] private IngredientColors[] colors;

    private void Start()
    {
        ResetAllLights();
    }

    public void ActivateLight(Machine machine)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            if (machine.recipe[machine.recipe.Count - 1] == colors[i].soulColor)
            {
                lights[machine.recipe.Count - 1].material.SetColor("_EmissionColor", colors[i].color);
                lights[machine.recipe.Count - 1].material.color = colors[i].color;
            }
        }
    }

    public void ResetAllLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].material.SetColor("_EmissionColor", baseColor);
            lights[i].material.color = baseColor;
        }
    }
}
[System.Serializable]
public class IngredientColors
{
    public OrdersData.OrderIngredients soulColor;
    public Color color;
}
