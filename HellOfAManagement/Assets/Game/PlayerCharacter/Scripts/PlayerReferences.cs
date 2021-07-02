using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public PlayerData playerData;
    [HideInInspector] public Rotation rotation;
    [HideInInspector] public PlayerInputs playerInputs;
    [HideInInspector] public ObjectsOnMouse objectsOnMouse;
    [HideInInspector] public Rigidbody playerRb;
    [HideInInspector] public Cooldown cooldown;
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public PlayerAnimations playerAnimations;


    private void Awake()
    {
        rotation = FindObjectOfType<Rotation>();
        playerInputs = this.gameObject.GetComponent<PlayerInputs>();
        objectsOnMouse = FindObjectOfType<ObjectsOnMouse>();
        playerRb = this.gameObject.GetComponent<Rigidbody>();
        cooldown = this.gameObject.GetComponent<Cooldown>();
        mainCamera = GameObject.FindObjectOfType<Camera>();
        playerAnimations = this.gameObject.GetComponent<PlayerAnimations>();
    }
}
