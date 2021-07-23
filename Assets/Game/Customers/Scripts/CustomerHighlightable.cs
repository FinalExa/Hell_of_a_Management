using UnityEngine;
public class CustomerHighlightable : Highlightable
{
    [SerializeField] private MiniDialogueWithText miniDialogue;
    private CustomerController customerController;
    [SerializeField] private string stateToActivateMiniDialogue;

    public override void Awake()
    {
        base.Awake();
        customerController = this.gameObject.GetComponent<CustomerController>();
    }
    public override void ActivateGraphic()
    {
        base.ActivateGraphic();
        if (customerController.curState == stateToActivateMiniDialogue) miniDialogue.SetupDialogue(customerController.chosenIngredients.Count.ToString());
    }

    public override void DeactivateGraphic()
    {
        base.DeactivateGraphic();
        if (customerController.curState == stateToActivateMiniDialogue) miniDialogue.DeactivateDialogue();
    }
}
