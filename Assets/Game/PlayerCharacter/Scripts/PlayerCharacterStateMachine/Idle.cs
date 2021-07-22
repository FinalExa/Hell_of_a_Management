public class Idle : PlayerState
{
    public Idle(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }

    public override void Start()
    {
        PlayerReferences playerReferences = _playerCharacter.playerController.playerReferences;
        playerReferences.playerAnimations.PlayerAnimatorStateUpdate(this.ToString());
        if (!playerReferences.playerAnimations.PauseAnimator) playerReferences.rotation.rotationEnabled = true;
    }

    public override void StateUpdate()
    {
        Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        GoToMovementState(playerInputs);
        GoToDashState(playerInputs);
        GoToHandsState(playerInputs);
        GoToInteractState(playerInputs);
    }
    #region ToMovementState
    private void GoToMovementState(PlayerInputs playerInputs)
    {
        if ((playerInputs.MovementInput != UnityEngine.Vector3.zero)) _playerCharacter.SetState(new Moving(_playerCharacter));
    }
    #endregion
    #region ToDashState
    private void GoToDashState(PlayerInputs playerInputs)
    {
        if (playerInputs.DashInput && !_playerCharacter.playerController.LeftHandOccupied && !_playerCharacter.playerController.RightHandOccupied && !_playerCharacter.playerController.DashLocked) _playerCharacter.SetState(new Dash(_playerCharacter));
    }
    #endregion
    #region ToHandsStates
    private void GoToHandsState(PlayerInputs playerInputs)
    {
        if (playerInputs.LeftHandInput || playerInputs.RightHandInput)
        {
            if (playerInputs.LeftHandInput) _playerCharacter.playerController.selectedHand = PlayerController.SelectedHand.Left;
            else _playerCharacter.playerController.selectedHand = PlayerController.SelectedHand.Right;
            _playerCharacter.SetState(new Hands(_playerCharacter));
        }
    }
    #endregion
    #region ToInteractState
    private void GoToInteractState(PlayerInputs playerInputs)
    {
        if (playerInputs.InteractionInput) _playerCharacter.SetState(new Interact(_playerCharacter));
    }
    #endregion
    #endregion
}
