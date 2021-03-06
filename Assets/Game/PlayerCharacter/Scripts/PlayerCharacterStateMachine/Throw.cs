using UnityEngine;
public class Throw : PlayerState
{
    private Vector3 clickedPos;
    public Throw(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }
    public override void Start()
    {
        _playerCharacter.playerController.playerReferences.playerAnimations.PlayerAnimatorStateUpdate(this.ToString());
        clickedPos = _playerCharacter.playerController.playerReferences.objectsOnMouse.GetMousePosition().point;
        _playerCharacter.playerController.playerReferences.rotation.rotationEnabled = false;
        _playerCharacter.playerController.playerReferences.playerRb.velocity = Vector3.zero;
        AudioManager.instance.Play("Mc_Throw");
        _playerCharacter.playerController.playerReferences.rotation.RotatePlayerToMousePosition();
        _playerCharacter.playerController.playerReferences.playerAnimations.PauseAnimatorStart();
    }
    public override void StateUpdate()
    {
        CheckHand();
    }
    #region Throw
    private void CheckHand()
    {
        if (_playerCharacter.playerController.selectedHand == PlayerController.SelectedHand.Left) ThrowLeftHand();
        else ThrowRightHand();
    }
    private void ThrowLeftHand()
    {
        if (_playerCharacter.playerController.LeftHand.transform.childCount > 0)
        {
            IThrowable iThrowable = _playerCharacter.playerController.LeftHand.transform.GetChild(0).gameObject.GetComponent<IThrowable>();
            LaunchObject(iThrowable);
        }
        SetHandFree();
    }
    private void ThrowRightHand()
    {
        if (_playerCharacter.playerController.RightHand.transform.childCount > 0)
        {
            IThrowable iThrowable = _playerCharacter.playerController.RightHand.transform.GetChild(0).gameObject.GetComponent<IThrowable>();
            LaunchObject(iThrowable);
        }
        SetHandFree();
    }
    private void LaunchObject(IThrowable iThrowable)
    {
        _playerCharacter.playerController.playerReferences.rotation.RotateObjectToLaunch(iThrowable.Self.transform, clickedPos);
        iThrowable.DetachFromPlayer(_playerCharacter.playerController.playerReferences.playerData.throwDistance, _playerCharacter.playerController.playerReferences.playerData.throwFlightTime);
    }
    private void SetHandFree()
    {
        if (_playerCharacter.playerController.selectedHand == PlayerController.SelectedHand.Left)
        {
            _playerCharacter.playerController.LeftHandOccupied = false;
            _playerCharacter.playerController.leftHandWeight = 0;
        }
        else
        {
            _playerCharacter.playerController.RightHandOccupied = false;
            _playerCharacter.playerController.rightHandWeight = 0;
        }
        Transitions();
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
