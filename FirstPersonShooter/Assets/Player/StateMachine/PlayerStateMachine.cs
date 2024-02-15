using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState currentState;
    public PlayerGroundState groundState = new PlayerGroundState();
    public PlayerAirState airState = new PlayerAirState();

    //debug text
    public TMP_Text debug_text;

    //player input
    [HideInInspector] public Vector2 move_input;

    //player vars
    [HideInInspector] public CharacterController controller;
    [HideInInspector] public Vector3 playerVelocity;
    [HideInInspector] public Vector3 wishDir;
    [HideInInspector] public bool jumpButtonPressed = false;
    public float jumpImpulse = 10f;



    void Start()
    {
        controller = GetComponent<CharacterController>();


        currentState = groundState;
        currentState.EnterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        DebugText();
    }
    void FixedUpdate()
    {
        FindWishDir();
        currentState.FixedUpdateState(this);
        MovePlayer();
    }

    public void SwitchState(PlayerBaseState state, PlayerBaseState newState)
    {
        state.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void DebugText()
    {
        debug_text.text = "Player Velocity: " + controller.velocity.ToString();
        debug_text.text += "\nWish Direction: " + wishDir.ToString();
        debug_text.text += "\nPlayer Speed: " + new Vector3(playerVelocity.x, 0, playerVelocity.z).magnitude.ToString();
        debug_text.text += "\nState: " + currentState.ToString();
    }
    public void FindWishDir()
    {
        wishDir = transform.right * move_input.x + transform.forward * move_input.y;
        wishDir = wishDir.normalized;
    }

    public void MovePlayer()
    {
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) jumpButtonPressed = true;
        if(context.phase == InputActionPhase.Canceled) jumpButtonPressed = false;
    }

    

}
