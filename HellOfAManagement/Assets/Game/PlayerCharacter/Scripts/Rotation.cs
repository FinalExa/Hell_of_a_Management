using UnityEngine;

public class Rotation : MonoBehaviour
{
    public bool rotationEnabled;
    private PlayerInputs playerInputs;
    private ObjectsOnMouse objectsOnMouse;
    private Transform playerCharacterTransform;
    private Camera mainCamera;
    void Awake()
    {
        playerInputs = FindObjectOfType<PlayerInputs>();
        mainCamera = FindObjectOfType<Camera>();
        objectsOnMouse = FindObjectOfType<ObjectsOnMouse>();
        playerCharacterTransform = this.gameObject.transform;
    }
    private void Start()
    {
        rotationEnabled = true;
    }
    void Update()
    {
        if (rotationEnabled == true && playerInputs.MovementInput != Vector3.zero) Rotate();
    }

    private void Rotate()
    {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 playerVector = (playerInputs.MovementInput.x * forward) + (playerInputs.MovementInput.z * right);
        Vector3 direction = (playerVector).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }
    private float CalculateAngle(Vector3 player, Vector3 mouse)
    {
        return Mathf.Atan2(mouse.x - player.x, mouse.z - player.z) * Mathf.Rad2Deg;
    }

    public void RotateObjectToLaunch(Transform objectToLaunch, Vector3 endPosition)
    {
        objectToLaunch.localRotation = Quaternion.identity;
        float angle = CalculateAngle(objectToLaunch.position, endPosition);
        playerCharacterTransform.rotation = Quaternion.Euler(new Vector3(objectToLaunch.rotation.x, angle, playerCharacterTransform.rotation.z));
    }
    public void RotatePlayerToMousePosition()
    {
        float angle = CalculateAngle(playerCharacterTransform.position, objectsOnMouse.mousePositionInSpace);
        playerCharacterTransform.rotation = Quaternion.Euler(new Vector3(playerCharacterTransform.rotation.x, angle, playerCharacterTransform.rotation.z));
    }
}
