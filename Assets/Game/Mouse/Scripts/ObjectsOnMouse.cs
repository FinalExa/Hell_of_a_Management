using UnityEngine;

public class ObjectsOnMouse : MouseData
{
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
