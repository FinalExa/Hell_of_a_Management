using UnityEngine;

public abstract class MouseData : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    [HideInInspector] public Vector3 mousePositionInSpace;
    private Camera mainCamera;

    public virtual void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public virtual void FixedUpdate()
    {
        MouseRaycast();
    }
    public virtual void MouseRaycast()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        mousePositionInSpace = hit.point;
    }
    public virtual RaycastHit GetMousePosition()
    {
        return hit;
    }
}
