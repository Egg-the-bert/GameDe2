using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_Text debug_text;

    public Camera cam;
    private Vector2 look_input = Vector2.zero;
    private float look_sped = 30f;
    private float horizontal_look_angle = 0f;
    public bool invert_x = false;
    public bool invert_y = false;
    public int invert_x_factor = 1;
    public int invert_y_factor = 1;

    private Vector2 move_input;
    private bool grounded;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 wishDir;
    public float maxSpeed = 6f;
    public float acceleration = 60f;
    public float gravity = 15f;
    public float stopSpeed = .5f;
    public float jumpImpulse = 10f;
    public float friction = 4f;
    
      
    void Start()
    {
        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //cam invert check
        if (invert_x == true) invert_x_factor = -1;
        if (invert_y == true) invert_y_factor = -1;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        debug_text.text = "Player Velocity: " + controller.velocity.ToString();
        debug_text.text += "\nWish Direction: " + wishDir.ToString();
        debug_text.text += "\nPlayer Speed: " + new Vector3(playerVelocity.x, 0 , playerVelocity.z).magnitude.ToString();
        debug_text.text += "\nGrounded: " + grounded.ToString();

        Look();
    }

    private void FixedUpdate()
    {
        wishDir = transform.right * move_input.x + transform.forward * move_input.y;
        wishDir = wishDir.normalized;

        grounded = controller.isGrounded;

        if (grounded)
        {
            playerVelocity = MoveGround(wishDir, playerVelocity);
        }
        else
        {
            playerVelocity = MoveAir(wishDir, playerVelocity);
        }
        
        playerVelocity.y -= gravity * Time.deltaTime;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }
        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
    }
    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }
    public void GetJumpInput(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void Look()
    {
        //looking left or right
        transform.Rotate(Vector3.up, look_input.x * look_sped * Time.deltaTime * invert_x_factor);

        //up and down
        float angle = look_input.y * look_sped * Time.deltaTime *  invert_y_factor;
        horizontal_look_angle -= angle;
        horizontal_look_angle = Mathf.Clamp(horizontal_look_angle, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(horizontal_look_angle, 0 , 0);
    }

    private void Jump()
    {
        if(grounded)
        {
            playerVelocity.y = jumpImpulse;
        }
    }

    private Vector3 Acceleration(Vector3 wishDir, Vector3 currentVelocity, float accel, float maxSpeed)
    {
        float projSpeed = Vector3.Dot(currentVelocity, wishDir);
        float accelSpeed = accel * Time.deltaTime;

        if (projSpeed + accelSpeed > maxSpeed)
        {
            accelSpeed = maxSpeed - projSpeed;
        }


        return currentVelocity + (wishDir * accelSpeed);
    }

    private Vector3 MoveAir(Vector3 wishDir, Vector3 currentVelocity)
    {
        return Acceleration(wishDir, currentVelocity, acceleration, maxSpeed);
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

        if(speed != 0)
        {
            float drop = speed * friction * Time.deltaTime;
            newVelocity *= Mathf.Max(speed - drop, 0)/ speed;
        }

        newVelocity = new Vector3 (newVelocity.x, currentVelocity.y, newVelocity.z);

        return Acceleration(wishDir, newVelocity, acceleration, maxSpeed);
    }
    
    










}
