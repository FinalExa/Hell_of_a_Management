﻿public class DrunkenFlying : DrunkenState
{
    public DrunkenFlying(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {
    }

    public override void Start()
    {
        _drunkenStateMachine.drunkenController.drunkenReferences.animations.AnimatorStateUpdate(this.ToString());
    }
    public override void StateUpdate()
    {
        Transitions();
    }

    private void Transitions()
    {
        GoToChase();
    }

    private void GoToChase()
    {
        if (!_drunkenStateMachine.drunkenController.drunkenReferences.throwableObject.isNotGrounded)
            _drunkenStateMachine.SetState(new DrunkenChase(_drunkenStateMachine));
    }
}
