using UnityEngine;

public class Highlightable : MonoBehaviour
{
    private ObjectsOnMouse mouseData;
    public Outline outline;
    public OutlineData outlineData;
    public GameObject thisGraphicsObject;
    private void Awake()
    {
        mouseData = FindObjectOfType<ObjectsOnMouse>();
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
        if (mouseData.pointedGameObject == this.gameObject) ActivateGraphic();
        else DeactivateGraphic();
    }
    public virtual void ActivateGraphic()
    {
        outline.enabled = true;
    }

    public virtual void DeactivateGraphic()
    {
        outline.enabled = false;
    }
}
