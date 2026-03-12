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
    public Camera theCam;
    public float maxLookAngle;
    public float minLookAngle;
    public LayerMask whatIsStock;
    public LayerMask whatIsShelf;
    public float interactionRange;
    private StockObject heldPickup;
    public Transform holdPoint;
    public float throwForce;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIController.Instance != null)
        {
            if (UIController.Instance.updatePricePanel.activeSelf)
            {
                return;
            }
        }

        //控制玩家旋转和视角
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        horiRot += lookInput.x * Time.deltaTime * lookSpeed;
        transform.rotation = Quaternion.Euler(0f, horiRot, 0f);

        vertRot -= lookInput.y * Time.deltaTime * lookSpeed;
        vertRot = Mathf.Clamp(vertRot, minLookAngle, maxLookAngle);
        theCam.transform.localRotation = Quaternion.Euler(vertRot, 0f, 0f);


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

        //交互
        Ray ray = theCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (heldPickup == null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, whatIsStock))
                {
                    heldPickup = hit.collider.GetComponent<StockObject>();
                    heldPickup.transform.SetParent(holdPoint);
                    heldPickup.Pickup();
                }
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, whatIsShelf))
                {
                    heldPickup = hit.collider.GetComponent<ShelfSpaceController>().GetStockObject();
                    if (heldPickup != null)
                    {
                        heldPickup.transform.SetParent(holdPoint);
                        heldPickup.Pickup();
                    }
                }
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, whatIsShelf))
                {
                    hit.collider.GetComponent<ShelfSpaceController>().StartUpdatePrice();
                }
            }
        }
        else
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, whatIsShelf))
                {
                    // heldPickup.MakePlaed();
                    // heldPickup.transform.SetParent(hit.transform);
                    // heldPickup = null;

                    hit.collider.GetComponent<ShelfSpaceController>().PlaceStock(heldPickup);

                    if (heldPickup.isPlaced)
                    {
                        heldPickup = null;
                    }
                }
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                heldPickup.Release();
                heldPickup.rb.AddForce(theCam.transform.forward * throwForce, ForceMode.Impulse);
                heldPickup.transform.SetParent(null);
                heldPickup = null;
            }
        }
    }
}
