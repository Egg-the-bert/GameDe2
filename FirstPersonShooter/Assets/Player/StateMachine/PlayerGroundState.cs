using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class PlayerGroundState : PlayerBaseState
{

    //Ground state variables
    public float maxSpeed = 6f;
    public float acceleration = 60f;
    public float stopSpeed = .5f;
    public float jumpImpulse = 10f;
    public float friction = 4f;



    public override void EnterState(PlayerStateMachine stateMachine)
    {
        
    }
    public override void ExitState(PlayerStateMachine stateMachine)
    {
        
    }
    public override void UpdateState(PlayerStateMachine stateMachine)
    {
        
    }
    public override void FixedUpdateState(PlayerStateMachine stateMachine)
    {
        stateMachine.playerVelocity = MoveGround(stateMachine.wishDir, stateMachine.playerVelocity);

        if (!stateMachine.controller.isGrounded)
        {
            stateMachine.SwitchState(this, stateMachine.airState);
        }

        if(stateMachine.jumpButtonPressed)
        {
            stateMachine.playerVelocity.y = jumpImpulse;
            stateMachine.SwitchState(this, stateMachine.airState);
        }
    }
    public override void SwitchState(PlayerStateMachine stateMachine)
    {

    }

    private Vector3 MoveGround(Vector3 wishDir, Vector3 currentVelocity)
    {
        Vector3 newVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        float speed = newVelocity.magnitude;
        if (speed <= stopSpeed)
        {
            newVelocity = Vector3.zero;
            speed = 0;
        }

        if (speed != 0)
        {
            float drop = speed * friction * Time.deltaTime;
            newVelocity *= Mathf.Max(speed - drop, 0) / speed;
        }

        newVelocity = new Vector3(newVelocity.x, currentVelocity.y, newVelocity.z);

        return Acceleration(wishDir, newVelocity, acceleration, maxSpeed);
    }



}
