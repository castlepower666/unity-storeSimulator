using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference lookAction;
    public CharacterController charCon;
    public float moveSpeed;
    public float jumpForce;
    private float ySpeed;
    private float horiRot;
    private float vertRot;
    public float lookSpeed;
    public Transform theCam;
    public float maxLookAngle;
    public float minLookAngle;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        horiRot += lookInput.x * Time.deltaTime * lookSpeed;
        transform.rotation = Quaternion.Euler(0f, horiRot, 0f);

        vertRot -= lookInput.y * Time.deltaTime * lookSpeed;
        vertRot = Mathf.Clamp(vertRot, minLookAngle, maxLookAngle);
        theCam.localRotation = Quaternion.Euler(vertRot, 0f, 0f);


        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        //Vector3 moveAmount = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        Vector3 moveAmount = transform.forward * moveInput.y + transform.right * moveInput.x;
        if (moveAmount.sqrMagnitude > 1) moveAmount.Normalize();

        moveAmount *= moveSpeed;

        if (charCon.isGrounded)
        {
            ySpeed = 0;
            if (jumpAction.action.WasPressedThisFrame())
            {
                ySpeed = jumpForce;
            }
        }



        ySpeed += Physics.gravity.y * Time.deltaTime;
        moveAmount.y = ySpeed;

        charCon.Move(moveAmount * Time.deltaTime);
    }
}
