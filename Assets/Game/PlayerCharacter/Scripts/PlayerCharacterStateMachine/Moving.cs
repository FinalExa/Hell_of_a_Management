using UnityEngine;
public class Moving : PlayerState
{
    public Moving(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }
    public override void Start()
    {
        AudioManager.instance.Play("Mc_Movement");
        _playerCharacter.playerController.playerReferences.playerAnimations.PlayerAnimatorStateUpdate(this.ToString());
        _playerCharacter.playerController.playerReferences.rotation.rotationEnabled = true;
        UpdateSpeedValue();
    }
    public override void StateUpdate()
    {
        Movement();
        Transitions();
    }

    #region Movement
    private void UpdateSpeedValue()
    {
        PlayerData playerData = _playerCharacter.playerController.playerReferences.playerData;
        PlayerController playerController = _playerCharacter.playerController;
        playerController.actualSpeed = playerData.movementSpeed - (_playerCharacter.playerController.leftHandWeight + _playerCharacter.playerController.rightHandWeight);
        if (playerController.actualSpeed < playerData.minSpeedValue) playerController.actualSpeed = playerData.minSpeedValue;
    }
    private void Movement()
    {
        Rigidbody playerRb = _playerCharacter.playerController.playerReferences.playerRb;
        PlayerController playerController = _playerCharacter.playerController;
        Vector3 movementWithDirection = MovementInitialization();
        playerRb.velocity = new Vector3(movementWithDirection.x, movementWithDirection.y, movementWithDirection.z) * playerController.actualSpeed;
    }

    private Vector3 MovementInitialization()
    {
        Camera mainCamera = _playerCharacter.playerController.playerReferences.mainCamera;
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        return (playerInputs.MovementInput.x * forward) + (playerInputs.MovementInput.z * right);
    }
    #endregion

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        GoToIdleState(playerInputs);
        GoToDashState(playerInputs);
        GoToHandsState(playerInputs);
        GoToInteractState(playerInputs);
    }
    #region ToIdleState
    private void GoToIdleState(PlayerInputs playerInputs)
    {
        if (playerInputs.MovementInput == Vector3.zero)
        {
            StopMovementSound();
            _playerCharacter.SetState(new Idle(_playerCharacter));
        }
    }
    #endregion
    #region ToDashState
    private void GoToDashState(PlayerInputs playerInputs)
    {
        if (playerInputs.DashInput && !_playerCharacter.playerController.LeftHandOccupied && !_playerCharacter.playerController.RightHandOccupied)
        {
            StopMovementSound();
            _playerCharacter.SetState(new Dash(_playerCharacter));
        }
    }
    #endregion
    #region ToHandsState
    private void GoToHandsState(PlayerInputs playerInputs)
    {
        if (playerInputs.LeftHandInput || playerInputs.RightHandInput)
        {
            StopMovementSound();
            if (playerInputs.LeftHandInput) _playerCharacter.playerController.selectedHand = PlayerController.SelectedHand.Left;
            else _playerCharacter.playerController.selectedHand = PlayerController.SelectedHand.Right;
            _playerCharacter.SetState(new Hands(_playerCharacter));
        }
    }
    #endregion
    #region ToInteractState
    private void GoToInteractState(PlayerInputs playerInputs)
    {
        if (playerInputs.InteractionInput)
        {
            StopMovementSound();
            _playerCharacter.SetState(new Interact(_playerCharacter));
        }
    }

    #endregion
    private void StopMovementSound()
    {
        //AudioManager.instance.Stop("Mc_Throw");
    }
    #endregion
}
