using UnityEngine;

public class DrunkenReferences : MonoBehaviour
{
    public DrunkenData drunkenData;
    public ThrowableObject throwableObject;

    private void Awake()
    {
        throwableObject = this.GetComponent<ThrowableObject>();
    }
}
