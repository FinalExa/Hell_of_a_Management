using UnityEngine;

public class SoulStateMachine : StateMachine
{
    [HideInInspector] public SoulController soulController;
    public string currentState;
    private void Awake()
    {
        soulController = this.gameObject.GetComponent<SoulController>();
        SetState(new SoulIdle(this));
    }
    public override void Start()
    {
        return;
    }
    private void OnEnable()
    {
        _state.Start();
    }
    public override void SetState(State state)
    {
        base.SetState(state);
        currentState = state.ToString();
    }
}
