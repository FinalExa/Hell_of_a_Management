using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private ObjectsOnMouse mouseData;
    private ThrowableObject throwableObject;
    private ICanBeInteracted interactable;
    public Outline outline;
    public OutlineData outlineData;
    public GameObject thisGraphicsObject;
    private void Awake()
    {
        mouseData = FindObjectOfType<ObjectsOnMouse>();
        if (this.gameObject.GetComponent<ThrowableObject>() != null) throwableObject = this.gameObject.GetComponent<ThrowableObject>();
        if (this.gameObject.GetComponent<ICanBeInteracted>() != null) interactable = this.gameObject.GetComponent<ICanBeInteracted>();
    }
    private void Start()
    {
        if (outline != null)
        {
            outline.OutlineColor = outlineData.highlightColor;
            outline.OutlineWidth = outlineData.outlineWidth;
        }
    }

    private void Update()
    {
        if (outline != null) HighlightSelf();
    }

    public void HighlightSelf()
    {
        if (mouseData.pointedGameObject == this.gameObject) outline.enabled = true;
        else outline.enabled = false;
    }
}
