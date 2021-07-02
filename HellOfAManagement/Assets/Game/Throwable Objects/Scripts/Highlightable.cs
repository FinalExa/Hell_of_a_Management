using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private MouseData mouseData;
    private Renderer thisRenderer;
    private ThrowableObject throwableObject;
    public ThrowableObjectData throwableObjectData;
    public GameObject thisGraphicsObject;
    private void Awake()
    {
        mouseData = FindObjectOfType<MouseData>();
        thisRenderer = thisGraphicsObject.GetComponent<Renderer>();
        throwableObject = this.gameObject.GetComponent<ThrowableObject>();
    }
    void Update()
    {
        HighlightSelf();
    }

    public void HighlightSelf()
    {
        Collider collider = mouseData.GetMousePosition().collider;
        if (collider != null && GameObject.ReferenceEquals(collider.gameObject, this.gameObject) && throwableObject.IsInsidePlayerRange) thisRenderer.material.color = throwableObjectData.highlightColor;
        else thisRenderer.material.color = throwableObjectData.baseColor;
    }
}
