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

    public Vector3 Acceleration(Vector3 wishDir, Vector3 currentVelocity, float accel, float maxSpeed)
    {
        float projSpeed = Vector3.Dot(currentVelocity, wishDir);
        float accelSpeed = accel * Time.deltaTime;

        if (projSpeed + accelSpeed > maxSpeed)
        {
            accelSpeed = maxSpeed - projSpeed;
        }


        return currentVelocity + (wishDir * accelSpeed);
    }




}
