using UnityEngine;
using TMPro;
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public Transform playerCamera;
    public Slider statusSlider;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float rotationSpeed = 500f;
    public bool localControl = false;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 LastPosition = Vector3.zero;
    private CharacterController characterController;
    private PlacementGenerator generators;

    private TextMeshProUGUI userNote;
    private bool isInTrigger = false;
    private bool longHold = false;
    private float holdDuration = 2f;
    private float holdTimer;
    private Action triggerCallback;
    private bool holdingItem = false;
    private Transform holdingTransform;

    public int numberOfLogs = 0;
    float pickedUpTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        userNote = GameObject.FindWithTag("UserNotifications").GetComponent<TextMeshProUGUI>();
        generators = FindAnyObjectByType<PlacementGenerator>();
    }

    // Update is called once per frame
    void Update()

    {
        Vector3 forward;
        Vector3 right;
        if (localControl)
        {

            forward = transform.TransformDirection(Vector3.forward);
            right = transform.TransformDirection(Vector3.right);

        }
        else
        {
            forward = Vector3.forward;
            right = Vector3.right;
        }
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = -9.81f;
        characterController.Move(moveDirection * Time.deltaTime);
        if (LastPosition != characterController.transform.position)
        {
            moveDirection.y = 0;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
            characterController.transform.rotation = Quaternion.RotateTowards(characterController.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);



            LastPosition = characterController.transform.position;
            // rotationX += -Input.GetAxis("Mouse Y") * .5f;
            // transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * .5f, 0);
            // playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
            playerCamera.transform.position = LastPosition;
            if (generators)
            {
                generators.CheckActive(new Vector2(Mathf.RoundToInt(LastPosition.x), Mathf.RoundToInt(LastPosition.z)));
            }

        }

        if (isInTrigger && !longHold && Input.GetKeyDown(KeyCode.E))
        {
            triggerCallback();
        }
        else if (isInTrigger && longHold && Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;
            statusSlider.value = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                triggerCallback();
                holdTimer = 0;
            }
        }
        else if (holdingItem && Time.time - pickedUpTime >= .3f && Input.GetKey(KeyCode.E))
        {
            DropObj();
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CharacterInteractable"))
        {
            isInTrigger = true;
            userNote.enabled = true;
            longHold = false;
            triggerCallback = other.gameObject.GetComponent<interactControl>().callBack();
            userNote.text = other.gameObject.GetComponent<interactControl>().messageText();

        }
        else if (other.gameObject.CompareTag("CharacterPickup"))
        {

            other.gameObject.GetComponent<interactControl>().callBack()();
            generators.RemoveObject(other.gameObject);
        }
        else if (other.gameObject.CompareTag("CharacterInteractableLong"))
        {
            isInTrigger = true;
            userNote.enabled = true;
            longHold = true;
            statusSlider.gameObject.SetActive(true);
            triggerCallback = other.gameObject.GetComponent<interactControl>().callBack();
            userNote.text = other.gameObject.GetComponent<interactControl>().messageText();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CharacterInteractable"))
        {
            holdTimer = 0;
            userNote.enabled = false;
            isInTrigger = false;

        }
        else if (other.gameObject.CompareTag("CharacterInteractableLong"))
        {
            holdTimer = 0;
            userNote.enabled = false;
            isInTrigger = false;
            longHold = false;
            statusSlider.gameObject.SetActive(false);
        }
    }

    internal void PickupObj(GameObject item)
    {
        if (!holdingItem)
        {
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector3(0, 0, 2f);
            if (item.transform.rotation.eulerAngles.y < 50f || item.transform.rotation.eulerAngles.y > 130f)
            {
                item.transform.rotation = Quaternion.Euler(item.transform.rotation.eulerAngles.x, 90f, item.transform.rotation.eulerAngles.z);

            }
            holdingTransform = item.transform;
            holdingItem = true;
            pickedUpTime = Time.time;
        }
    }
    void DropObj()
    {
        if (holdingItem)
        {
            holdingTransform.SetParent(null);
            holdingItem = false;
        }
    }

    internal void depositLog()
    {
        if (holdingItem && holdingTransform.gameObject.name == "Log, Hollow")
        {
            holdingTransform.SetParent(null);
            holdingItem = false;
            numberOfLogs++;
            Destroy(holdingTransform.gameObject);
        }
    }
}