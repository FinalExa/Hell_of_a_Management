using UnityEngine;
public class PlayerAnimations : Animations
{
    PlayerController playerController;
    PlayerCharacter playerCharacter;
    [SerializeField] private float pauseAnimatorMaxTimer;
    private float pauseAnimatorTimer;
    private bool pauseAnimator;

    private void Awake()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        playerCharacter = this.gameObject.GetComponent<PlayerCharacter>();
    }
    private void Start()
    {
        pauseAnimator = false;
        pauseAnimatorTimer = pauseAnimatorMaxTimer;
    }
    private void Update()
    {
        if (pauseAnimator) PauseAnimatorTimer();
    }
    private void PauseAnimatorTimer()
    {
        if (pauseAnimatorTimer > 0) pauseAnimatorTimer -= Time.deltaTime;
        else
        {
            pauseAnimatorTimer = pauseAnimatorMaxTimer;
            pauseAnimator = false;
            PlayerAnimatorStateUpdate(playerCharacter.thisStateName);
        }
    }
    public void PauseAnimator()
    {
        pauseAnimator = true;
    }
    public void PlayerAnimatorStateUpdate(string statePassed)
    {
        if (!pauseAnimator)
        {
            HandsChecks();
            AnimatorStateUpdate(statePassed);
        }
    }
    private void HandsChecks()
    {
        ActiveHand();
        HandsStatus();
    }
    private void ActiveHand()
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
    private void HandsStatus()
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
