public class SoulAnimations : Animations
{
    private SoulStateMachine soulStateMachine;

    private void Awake()
    {
        soulStateMachine = (SoulStateMachine)stateMachineToRead;
    }
    public void OnEnable()
    {
        animator = soulStateMachine.soulController.soulTypes[soulStateMachine.soulController.thisSoulTypeIndex].soulAnimator;
    }
}
