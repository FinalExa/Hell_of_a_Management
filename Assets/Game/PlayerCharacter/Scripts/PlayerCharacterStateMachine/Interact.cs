using System.Collections.Generic;
using UnityEngine;
public class Interact : PlayerState
{
    public Interact(PlayerCharacter playerCharacter) : base(playerCharacter)
    {
    }

    public override void Start()
    {
        _playerCharacter.playerController.objectClicked = _playerCharacter.playerController.playerReferences.objectsOnMouse.PassPointedObject().GetComponent<Collider>();
        CheckIfInteractableIsSelected();
    }
    private void CheckIfInteractableIsSelected()
    {
        ObjectsOnMouse objectsOnMouse = _playerCharacter.playerController.playerReferences.objectsOnMouse;
        if (objectsOnMouse.CheckForInteractableObject(_playerCharacter.playerController.objectClicked)) CheckIfObjectIsInPlayerRange(_playerCharacter.playerController.interactablesInPlayerRange);
        else Transitions();
    }

    private void CheckIfObjectIsInPlayerRange(List<Collider> listToCheck)
    {
        foreach (var collider in listToCheck)
        {
            if (collider == _playerCharacter.playerController.objectClicked)
            {
                collider.gameObject.GetComponent<ICanBeInteracted>().Interaction();
                break;
            }
        }
        Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        PlayerInputs playerInputs = _playerCharacter.playerController.playerReferences.playerInputs;
        if (playerInputs.MovementInput == Vector3.zero) GoToIdle();
        else GoToMoving();
    }
    private void GoToIdle()
    {
        _playerCharacter.SetState(new Idle(_playerCharacter));
    }
    private void GoToMoving()
    {
        _playerCharacter.SetState(new Moving(_playerCharacter));
    }
    #endregion
}
