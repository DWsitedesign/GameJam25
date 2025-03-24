using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Transform playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 LastPosition = Vector3.zero;
    private CharacterController characterController;
    private bool canMove = true;
    private PlacementGenerator[] generators;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        generators = FindObjectsByType<PlacementGenerator>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()

    {

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = -9.81f;
        characterController.Move(moveDirection * Time.deltaTime);
        if (LastPosition!=characterController.transform.position)
        {
            LastPosition=characterController.transform.position;
            // rotationX += -Input.GetAxis("Mouse Y") * .5f;
            // transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * .5f, 0);
            // playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            playerCamera.transform.position = LastPosition;
            for (int i = 0; i < generators.Length; i++)
            {
                Debug.Log("checking");
                generators[i].CheckActive(new Vector2(Mathf.RoundToInt(LastPosition.x),Mathf.RoundToInt(LastPosition.z)));
                
            }
            
        }

    }

}