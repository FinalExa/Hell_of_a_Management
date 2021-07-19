using UnityEngine;

public class Highlightable : MonoBehaviour
{
    protected ObjectsOnMouse mouseData;
    public Outline outline;
    public OutlineData outlineData;
    public GameObject thisGraphicsObject;
    protected bool isActive;
    public virtual void Awake()
    {
        mouseData = FindObjectOfType<ObjectsOnMouse>();
    }
    public virtual void Start()
    {
        DeactivateGraphic();
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

    public virtual void HighlightSelf()
    {
        if (mouseData.pointedGameObject == this.gameObject)
        {
            if (!isActive) ActivateGraphic();
            OtherActivateBehaviour();
        }
        else if (mouseData.pointedGameObject != this.gameObject)
        {
            if (isActive) DeactivateGraphic();
            OtherDeactivateBehaviour();
        }
    }
    public virtual void ActivateGraphic()
    {
        outline.enabled = true;
        isActive = true;
    }
    public virtual void OtherActivateBehaviour()
    {
        return;
    }
    public virtual void DeactivateGraphic()
    {
        outline.enabled = false;
        isActive = false;
    }
    public virtual void OtherDeactivateBehaviour()
    {
        return;
    }

}
