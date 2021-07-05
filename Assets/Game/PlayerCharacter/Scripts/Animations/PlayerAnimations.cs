public class PlayerAnimations : Animations
{
    PlayerCharacter playerCharacter;
    private void Awake()
    {
        PlayerCharacter.playerStateChanged += UpdateAnimatorValues;
        PlayerAnimationsBehaviour.onAnimationEnd += AnimationIsOver;
        playerCharacter = this.gameObject.GetComponent<PlayerCharacter>();
    }
    public override void AnimatorStateUpdate()
    {
        HandsChecks();
        base.AnimatorStateUpdate();
    }
    private void HandsChecks()
    {
        PlayerController playerController = playerCharacter.playerController;
        ActiveHand(playerController);
        HandsStatus(playerController);
    }

    private void ActiveHand(PlayerController playerController)
    {
        if (playerController.selectedHand == PlayerController.SelectedHand.Left && !animator.GetBool("LeftHandSelected"))
        {
            animator.SetBool("RightHandSelected", false);
            animator.SetBool("LeftHandSelected", true);
        }
        else if (playerController.selectedHand == PlayerController.SelectedHand.Right && !animator.GetBool("RightHandSelected"))
        {
            animator.SetBool("LeftHandSelected", false);
            animator.SetBool("RightHandSelected", true);
        }
    }
    private void HandsStatus(PlayerController playerController)
    {
        if (!playerController.LeftHandOccupied && !playerController.RightHandOccupied && !animator.GetBool("HandsFree"))
        {
            SetAllHandsBoolFalse();
            animator.SetBool("HandsFree", true);
        }
        else if (!playerController.LeftHandOccupied && playerController.RightHandOccupied && !animator.GetBool("LeftFreeRightOccupied"))
        {
            SetAllHandsBoolFalse();
            animator.SetBool("LeftFreeRightOccupied", true);
        }
        else if (playerController.LeftHandOccupied && !playerController.RightHandOccupied && !animator.GetBool("LeftOccupiedRightFree"))
        {
            SetAllHandsBoolFalse();
            animator.SetBool("LeftOccupiedRightFree", true);
        }
        else if (playerController.LeftHandOccupied && playerController.RightHandOccupied && !animator.GetBool("HandsOccupied"))
        {
            SetAllHandsBoolFalse();
            animator.SetBool("HandsOccupied", true);
        }
    }
    private void SetAllHandsBoolFalse()
    {
        animator.SetBool("HandsFree", false);
        animator.SetBool("LeftFreeRightOccupied", false);
        animator.SetBool("LeftOccupiedRightFree", false);
        animator.SetBool("HandsOccupied", false);
    }
}
