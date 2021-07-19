using UnityEngine;

public class SoulReferences : MonoBehaviour
{
    [HideInInspector] public SoulThrowable soulThrowableObject;
    [HideInInspector] public Highlightable highlightable;
    [HideInInspector] public Animations soulAnimations;
    [HideInInspector] public PlayerInRange playerInRange;
    [HideInInspector] public SoulStateMachine soulStateMachine;
    public SoulData soulData;
    private void Awake()
    {
        soulThrowableObject = this.gameObject.GetComponent<SoulThrowable>();
        highlightable = this.gameObject.GetComponent<Highlightable>();
        soulAnimations = this.gameObject.GetComponent<Animations>();
        soulStateMachine = this.gameObject.GetComponent<SoulStateMachine>();
        playerInRange = FindObjectOfType<PlayerInRange>();
    }
}
