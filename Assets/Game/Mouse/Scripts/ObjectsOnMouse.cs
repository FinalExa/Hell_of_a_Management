using UnityEngine;

public class ObjectsOnMouse : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    [HideInInspector] public Vector3 mousePositionInSpace;
    private Camera mainCamera;

    public void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void FixedUpdate()
    {
        MouseRaycast();
    }
    public void MouseRaycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        mousePositionInSpace = hit.point;
    }
    public RaycastHit GetMousePosition()
    {
        return hit;
    }
    public bool CheckForThrowableObject(Collider hit)
    {
        if (hit != null && hit.GetComponent<IThrowable>() != null)
        {
            return true;
        }
        else return false;
    }

    public bool CheckForInteractableObject(Collider hit)
    {
        if (hit != null && hit.GetComponent<ICanBeInteracted>() != null)
        {
            return true;
        }
        else return false;
    }

    public GameObject PassThrowableObject()
    {
        return hit.collider.gameObject;
    }
}
