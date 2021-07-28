public class HighlightableMachine : Highlightable
{
    private Machine machine;
    public MiniDialogueWithText miniDialogueWithText;
    public override void Awake()
    {
        base.Awake();
        machine = this.gameObject.GetComponent<Machine>();
    }
    public override void ActivateGraphic()
    {
        if (machine.recipe.Count > 0) base.ActivateGraphic();
    }
    public override void OtherActivateBehaviour()
    {
        if (machine.recipe.Count > 0 && !miniDialogueWithText.activated) miniDialogueWithText.SetupDialogue("E");
    }
    public override void OtherDeactivateBehaviour()
    {
        if (miniDialogueWithText.activated) miniDialogueWithText.DeactivateDialogue();
    }
}
