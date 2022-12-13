using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] public CharacterController controller;

    public float speed = 12f;
    public float runSpeed = 15f;

    public float normalHeight;
    public float crouchHeight;

    public float jumpHeight = 3f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
        
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            Debug.Log("Springer! utan controll nedtryckt");
        }

        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            controller.height = crouchHeight; 
            Debug.Log("Croush");
            controller.Move(move * 6 * Time.deltaTime);
        }

        else
        {
            Debug.Log("Går vanligt");
            controller.Move(move * speed * Time.deltaTime);
            controller.height = normalHeight;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Debug.Log(velocity.y);
    }
}
