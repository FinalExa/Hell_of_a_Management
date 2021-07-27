using UnityEngine;
public class CustomerHighlightable : Highlightable
{
    [SerializeField] private MiniDialogueWithText miniDialogue;
    private CustomerController customerController;
    [SerializeField] private string stateToActivateHighlight;
    private bool tutorialDoItOnce;
    [SerializeField] private bool isTutorial;

    public override void Awake()
    {
        base.Awake();
        customerController = this.gameObject.GetComponent<CustomerController>();
    }
    public override void ActivateGraphic()
    {
        if (customerController.curState == stateToActivateHighlight)
        {
            base.ActivateGraphic();
            miniDialogue.SetupDialogue(customerController.chosenIngredients.Count.ToString());
            if (!tutorialDoItOnce && isTutorial)
            {
                tutorialDoItOnce = true;
                Tutorial.instance.ShowTutorialScreen();
            }
        }
    }

    public override void DeactivateGraphic()
    {
        base.DeactivateGraphic();
        if (customerController.curState == stateToActivateHighlight) miniDialogue.DeactivateDialogue();
    }
}
