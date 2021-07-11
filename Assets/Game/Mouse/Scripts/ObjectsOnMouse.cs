using System.Linq;
using UnityEngine;

public class ObjectsOnMouse : MonoBehaviour
{
    public RaycastHit hit;
    public Ray ray;
    public GameObject pointedGameObject;
    [HideInInspector] public Vector3 mousePositionInSpace;
    private Camera mainCamera;
    [SerializeField] private float overlapSphereRadius;

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
        Collider[] objectsInMouseRange = Physics.OverlapSphere(mousePositionInSpace, overlapSphereRadius);
        objectsInMouseRange = objectsInMouseRange.OrderBy((d) => (d.transform.position - mousePositionInSpace).sqrMagnitude).ToArray();
        for (int i = 0; i < objectsInMouseRange.Length; i++)
        {
            IThrowable throwable = objectsInMouseRange[i].gameObject.GetComponent<IThrowable>();
            ICanBeInteracted interactable = objectsInMouseRange[i].gameObject.GetComponent<ICanBeInteracted>();
            bool check = false;
            if ((throwable != null && !throwable.IsAttachedToHand && throwable.IsInsidePlayerRange) || (interactable != null && interactable.IsInsidePlayerRange))
            {
                pointedGameObject = objectsInMouseRange[i].gameObject;
                check = true;
                break;
            }
            if (!check)
            {
                pointedGameObject = this.gameObject;
            }
        }
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

    public GameObject PassPointedObject()
    {
        return pointedGameObject.gameObject;
    }
}
