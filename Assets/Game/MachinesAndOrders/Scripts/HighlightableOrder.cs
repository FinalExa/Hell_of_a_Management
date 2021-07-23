using UnityEngine;
public class HighlightableOrder : Highlightable
{
    [SerializeField] private Order order;
    [SerializeField] private CustomerVignette objectToActivate;

    public override void ActivateGraphic()
    {
        base.ActivateGraphic();
        objectToActivate.SetupVignette(order.thisOrderType, order.thisOrderIngredients, 0, true);
    }

    public override void DeactivateGraphic()
    {
        base.DeactivateGraphic();
        objectToActivate.DeactivateVignette(0);
    }
}
