using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState currentState;
    public PlayerGroundState groundState = new PlayerGroundState();
    public PlayerAirState airState = new PlayerAirState();



    void Start()
    {
        currentState = groundState;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerBaseState state, PlayerBaseState newState)
    {
        state.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
