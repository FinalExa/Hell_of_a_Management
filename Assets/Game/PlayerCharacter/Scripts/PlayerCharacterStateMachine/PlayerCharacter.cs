using System;
using UnityEngine;
public class PlayerCharacter : StateMachine
{
    [HideInInspector] public PlayerController playerController;
    public static Action playerStateChanged;

    public override void SetState(State state)
    {
        base.SetState(state);
        //playerStateChanged();
    }
    private void Awake()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        SetState(new Idle(this));
    }
    private void OnCollisionStay(Collision collision)
    {
        _state.Collisions(collision);
    }
}
