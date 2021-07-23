﻿using UnityEngine;
using System;
public class DrunkenDeny : DrunkenState
{
    public float timer;
    public static Action removeItemFromHand;

    public DrunkenDeny(DrunkenStateMachine drunkenStateMachine) : base(drunkenStateMachine)
    {

    }

    public override void Start()
    {
        timer = _drunkenStateMachine.drunkenController.drunkenReferences.drunkenData.timerDeny;
        _drunkenStateMachine.drunkenController.navMeshAgent.enabled = false;
    }

    private void Deny()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            removeItemFromHand();
            timer = _drunkenStateMachine.drunkenController.drunkenReferences.drunkenData.timerDeny;
        }
    }

    public override void StateUpdate()
    {
        Deny();
        Transitions();
    }

    private void Transitions()
    {
        GoToGrabbed();
        GoToChase();
    }

    private void GoToGrabbed()
    {
        if (_drunkenStateMachine.drunkenController.drunkenReferences.throwableObject.IsAttachedToHand)
        {
            _drunkenStateMachine.SetState(new DrunkenGrabbed(_drunkenStateMachine));
        }
    }

    private void GoToChase()
    {
        if (!_drunkenStateMachine.drunkenController.playerInRange)
            _drunkenStateMachine.SetState(new DrunkenChase(_drunkenStateMachine));
    }
}
