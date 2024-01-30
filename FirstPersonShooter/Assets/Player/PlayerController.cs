using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera cam;
    private Vector2 look_input = Vector2.zero;
    private float look_sped = 30f;
    private float horizontal_look_angle = 0f;
    public bool invert_x = false;
    public bool invert_y = false;
    public int invert_x_factor = 1;
    public int invert_y_factor = 1;
      
    void Start()
    {
        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        //cam invert check
        if (invert_x == true) invert_x_factor = -1;
        if (invert_y == true) invert_y_factor = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
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
}
