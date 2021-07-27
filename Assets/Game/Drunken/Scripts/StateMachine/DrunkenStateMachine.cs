using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkenStateMachine : StateMachine
{
    [HideInInspector]public DrunkenController drunkenController;

    private void Awake()
    {
        drunkenController = GetComponent<DrunkenController>();    
    }

    private void OnEnable()
    {
        SetState(new DrunkenChase(this));
    }
}
