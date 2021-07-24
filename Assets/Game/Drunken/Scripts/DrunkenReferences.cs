using UnityEngine;

public class DrunkenReferences : MonoBehaviour
{
    public DrunkenData drunkenData;
    [HideInInspector] public ThrowableObject throwableObject;
    [HideInInspector] public Highlightable highlightable;
    [HideInInspector] public Animations animations;


    private void Awake()
    {
        throwableObject = this.gameObject.GetComponent<ThrowableObject>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
        animations = this.gameObject.GetComponent<Animations>();
    }
}
