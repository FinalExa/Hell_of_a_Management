using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action changeMovementData;
    [HideInInspector] public PlayerReferences playerReferences;
    public List<Collider> throwablesInPlayerRange;
    public List<Collider> interactablesInPlayerRange;
    public GameObject LeftHand;
    public GameObject RightHand;
    public bool LeftHandOccupied { get; set; }
    public bool RightHandOccupied { get; set; }
    public bool DashLocked { get; private set; }
    [HideInInspector] public float leftHandWeight;
    [HideInInspector] public float rightHandWeight;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public Collider objectClicked;
    public bool isTutorial;
    [HideInInspector] public bool tutorialInteractionDone;
    public enum SelectedHand
    {
        Left,
        Right
    }
    [HideInInspector] public SelectedHand selectedHand;
    [HideInInspector] public string curState;

    private void Awake()
    {
        DrunkenDeny.removeItemFromHand += RemoveFromHand;
        playerReferences = this.gameObject.GetComponent<PlayerReferences>();
    }

    private void FixedUpdate()
    {
        TerrainDashCheck();
    }

    private void RemoveFromHand()
    {
        IThrowable objectToDetach;
        if (LeftHandOccupied && RightHandOccupied)
        {
            int randHand = UnityEngine.Random.Range(0, 1);
            if (randHand == 0)
            {
                objectToDetach = LeftHand.GetComponentInChildren<IThrowable>();
                LeftHandOccupied = false;
            }
            else
            {
                objectToDetach = RightHand.GetComponentInChildren<IThrowable>();
                RightHandOccupied = false;
            }
        }
        else if (LeftHandOccupied && !RightHandOccupied)
        {
            objectToDetach = LeftHand.GetComponentInChildren<IThrowable>();
            LeftHandOccupied = false;
        }
        else if (!LeftHandOccupied && RightHandOccupied)
        {
            objectToDetach = RightHand.GetComponentInChildren<IThrowable>();
            RightHandOccupied = false;
        }
        else objectToDetach = null;
        if (objectToDetach != null)
        {
            AudioManager.instance.Play("DrunkenDemon_Displace");
            objectToDetach.DetachFromPlayer(0.1f, 0.1f);
            playerReferences.playerAnimations.PlayerAnimatorStateUpdate(curState);
        }
    }

    private void TerrainDashCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, 0.5f);
        bool check = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Terrain"))
            {
                if (colliders[i].GetComponent<SurfaceController>().Type == SurfaceManager.SurfaceType.MUD)
                {
                    check = true;
                    DashLocked = true;
                    break;
                }
            }
        }
        if (check == false) DashLocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            changeMovementData();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            changeMovementData();
        }
    }
}
