using UnityEngine;
using System.Collections;

    public class FPSController : MonoBehaviour
    {
    
        public float speed = 5f;
        public float sensitivity = 2f;
        public float jumpHeight = 3f;
        CharacterController player;
        
        public float gravity = -9.81f;
        Vector3 velocity;

        public GameObject mainCam;

        float moveFront;
        float moveLeft;

        float rotX;
        float rotY;
        public bool mouseShow = false;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        
        bool isGrounded;

        Rigidbody rb;
       
        void Start()
        {
            player = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (mouseShow == false)
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                moveFront = Input.GetAxis("Vertical") * speed;
                moveLeft = Input.GetAxis("Horizontal") * speed;

                rotX = Input.GetAxis("Mouse X") * sensitivity;
                rotY -= Input.GetAxis("Mouse Y") * sensitivity;

                rotY = Mathf.Clamp(rotY, -60f, 60f);

                Vector3 movement = new Vector3(moveLeft, 0, moveFront);
                transform.Rotate(0, rotX, 0);
                mainCam.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

                movement = transform.rotation * movement;
                movement.y -= 10f * Time.deltaTime;
                player.Move(movement * Time.deltaTime);

                velocity.y += gravity * Time.deltaTime;

                player.Move(velocity * Time.deltaTime);

                if (Input.GetKeyDown("escape") && mouseShow == false)
                {
                    Cursor.lockState = CursorLockMode.None;
                    mouseShow = true;
                }
                else if (Input.GetKeyDown("escape") && mouseShow == true)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    mouseShow = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }

                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
            
            
        }     
        
    }
