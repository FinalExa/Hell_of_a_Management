using UnityEngine;
public class Grab : PlayerState
{
    public Grab(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }

    public override void Start()
    {
        _playerCharacter.playerController.playerReferences.playerAnimations.PlayerAnimatorStateUpdate(this.ToString());
        _playerCharacter.playerController.playerReferences.rotation.rotationEnabled = false;
        _playerCharacter.playerController.playerReferences.playerRb.velocity = Vector3.zero;
        _playerCharacter.playerController.playerReferences.playerAnimations.waitForAnimation = true;
        AudioManager.instance.Play("Mc_Grab");
        _playerCharacter.playerController.playerReferences.rotation.RotatePlayerToMousePosition();
    }

    public override void StateUpdate()
    {
        PlayerAnimations playerAnimations = _playerCharacter.playerController.playerReferences.playerAnimations;
        if (!playerAnimations.waitForAnimation)
            CheckHand();
    }

    #region Grab
    private void CheckHand()
    {
        PlayerController.SelectedHand selectedHand = _playerCharacter.playerController.selectedHand;
        if (selectedHand == PlayerController.SelectedHand.Left) SetHandOccupied(selectedHand);
        else if (selectedHand == PlayerController.SelectedHand.Right) SetHandOccupied(selectedHand);
    }
    private void SetHandOccupied(PlayerController.SelectedHand selectedHand)
    {

        IThrowable iThrowable = _playerCharacter.playerController.objectClicked.GetComponent<IThrowable>();
        if (selectedHand == PlayerController.SelectedHand.Left) LeftHand(iThrowable);
        else RightHand(iThrowable);
        Transitions();
    }
    private void LeftHand(IThrowable iThrowable)
    {
        iThrowable.AttachToPlayer(_playerCharacter.playerController.LeftHand);
        _playerCharacter.playerController.LeftHandOccupied = true;
        _playerCharacter.playerController.leftHandWeight = iThrowable.Weight;
    }
    private void RightHand(IThrowable iThrowable)
    {
        iThrowable.AttachToPlayer(_playerCharacter.playerController.RightHand);
        _playerCharacter.playerController.RightHandOccupied = true;
        _playerCharacter.playerController.rightHandWeight = iThrowable.Weight;
    }
    #endregion

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        if (playerInputs.MovementInput == Vector3.zero) ReturnToIdle();
        else ReturnToMovement();
    }
    private void ReturnToIdle()
    {
        _playerCharacter.SetState(new Idle(_playerCharacter));
    }
    private void ReturnToMovement()
    {
        _playerCharacter.SetState(new Moving(_playerCharacter));
    }
    #endregion
}
