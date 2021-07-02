using UnityEngine;

public class SoulReferences : MonoBehaviour
{
    [HideInInspector] public SoulThrowable soulThrowableObject;
    [HideInInspector] public Highlightable highlightable;
    [HideInInspector] public SoulAnimations soulAnimations;
    public SoulData soulData;
    private void Awake()
    {
        soulThrowableObject = this.gameObject.GetComponent<SoulThrowable>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
        soulAnimations = this.gameObject.GetComponent<SoulAnimations>();
    }
}
