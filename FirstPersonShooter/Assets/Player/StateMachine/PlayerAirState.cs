using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public float maxSpeed = 8f;
    public float acceleration = 60f;
    public float gravity = 15f;

    public override void EnterState(PlayerStateMachine stateMachine)
    {
        
    }
    public override void ExitState(PlayerStateMachine stateMachine)
    {
        stateMachine.playerVelocity.y = -2;
    }
    public override void UpdateState(PlayerStateMachine stateMachine)
    {

    }
    public override void FixedUpdateState(PlayerStateMachine stateMachine)
    {
        stateMachine.playerVelocity.y -= gravity * Time.deltaTime;
        
        stateMachine.playerVelocity = MoveAir(stateMachine.wishDir, stateMachine.playerVelocity);


        if(stateMachine.controller.isGrounded)
        {
            stateMachine.SwitchState(this, stateMachine.groundState);
        }
    }
    public override void SwitchState(PlayerStateMachine stateMachine)
    {

    }

    private Vector3 MoveAir(Vector3 wishDir, Vector3 currentVelocity)
    {
        return Acceleration(wishDir, currentVelocity, acceleration, maxSpeed);
    }
}
