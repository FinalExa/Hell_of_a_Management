using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private MouseData mouseData;
    private ThrowableObject throwableObject;
    public Outline outline;
    public ThrowableObjectData throwableObjectData;
    public GameObject thisGraphicsObject;
    private void Awake()
    {
        mouseData = FindObjectOfType<MouseData>();
        throwableObject = this.gameObject.GetComponent<ThrowableObject>();
    }
    void Update()
    {
        HighlightSelf();
    }

    public void HighlightSelf()
    {
        Collider collider = mouseData.GetMousePosition().collider;
        if (collider != null && GameObject.ReferenceEquals(collider.gameObject, this.gameObject) && throwableObject.IsInsidePlayerRange) outline.enabled = true;
        else outline.enabled = false;
    }
}
