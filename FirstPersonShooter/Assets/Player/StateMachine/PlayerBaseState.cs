using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateMachine stateMachine);
    public abstract void ExitState(PlayerStateMachine stateMachine);
    public abstract void UpdateState(PlayerStateMachine stateMachine);
    public abstract void FixedUpdateState(PlayerStateMachine stateMachine);
    public abstract void SwitchState(PlayerStateMachine stateMachine);
   


}
