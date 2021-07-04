using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private MouseData mouseData;
    private ThrowableObject throwableObject;
    private ICanBeInteracted interactable;
    public Outline outline;
    public OutlineData outlineData;
    public GameObject thisGraphicsObject;
    private void Awake()
    {
        mouseData = FindObjectOfType<MouseData>();
        if (this.gameObject.GetComponent<ThrowableObject>() != null) throwableObject = this.gameObject.GetComponent<ThrowableObject>();
        if (this.gameObject.GetComponent<ICanBeInteracted>() != null) interactable = this.gameObject.GetComponent<ICanBeInteracted>();
    }
    private void Start()
    {
        if (outline != null) outline.OutlineColor = outlineData.highlightColor;
    }
    void Update()
    {
        if (outline != null) HighlightSelf();
    }

    public void HighlightSelf()
    {
        Collider collider = mouseData.GetMousePosition().collider;
        if (throwableObject != null)
        {
            if (collider != null && GameObject.ReferenceEquals(collider.gameObject, this.gameObject) && throwableObject.IsInsidePlayerRange) outline.enabled = true;
            else outline.enabled = false;
        }
        else
        {
            if (collider != null && GameObject.ReferenceEquals(collider.gameObject, this.gameObject) && interactable.IsInsidePlayerRange) outline.enabled = true;
            else outline.enabled = false;
        }
    }
}
