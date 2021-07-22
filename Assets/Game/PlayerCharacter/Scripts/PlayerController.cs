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
    public enum SelectedHand
    {
        Left,
        Right
    }
    [HideInInspector] public SelectedHand selectedHand;

    private void Awake()
    {
        playerReferences = this.gameObject.GetComponent<PlayerReferences>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            changeMovementData();
            if (other.gameObject.GetComponent<SurfaceController>().Type == SurfaceManager.SurfaceType.MUD) DashLocked = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            changeMovementData();
            if (other.gameObject.GetComponent<SurfaceController>().Type == SurfaceManager.SurfaceType.MUD) DashLocked = false;
        }
    }
}
