using UnityEngine;

public class DrunkenReferences : MonoBehaviour
{
    public DrunkenData drunkenData;
    [HideInInspector] public DrunkenThrowable throwableObject;
    [HideInInspector] public Highlightable highlightable;
    [HideInInspector] public Animations animations;
    [HideInInspector] public DrunkenStateMachine drunkenStateMachine;


    private void Awake()
    {
        throwableObject = this.gameObject.GetComponent<DrunkenThrowable>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
        animations = this.gameObject.GetComponent<Animations>();
    }
}
