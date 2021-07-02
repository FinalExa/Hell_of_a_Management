using UnityEngine;
public abstract class StateMachine : MonoBehaviour
{
    protected State _state;
    [HideInInspector] public string stateRef;

    public virtual void SetState(State state)
    {
        _state = state;
        state.Start();
    }
    private void Start()
    {
        _state.Start();
    }
    private void Update()
    {
        _state.StateUpdate();
    }
    private void FixedUpdate()
    {
        _state.StatePhysicsUpdate();
    }
}
