using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public CurrentKeybinds currentKeybindsScript;

    public Camera playerCamera;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    float rotationX = 0;


    CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;

    [HideInInspector] public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * (Input.GetKey(currentKeybindsScript.playerForwardKey) ? 1 : Input.GetKey(currentKeybindsScript.playerBackwardKey) ? -1 : 0) : 0;

        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * (Input.GetKey(currentKeybindsScript.playerRightKey) ? 1 : Input.GetKey(currentKeybindsScript.playerLeftKey) ? -1 : 0) : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if ((Input.GetKey(currentKeybindsScript.playerJumpKey)) && (canMove) && (characterController.isGrounded))
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}