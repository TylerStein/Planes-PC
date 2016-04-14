using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class Controls : MonoBehaviour
{
    //public float variables
    public float currentHealth;
    public float maxHealth = 100;

    public float currentStamina;
    public float maxStamina = 100;

    public float currentMS;
    public float maxMS = 100;

    public float maxCarryWeight = 20;
    public float currentCarryWeight;
    public float balance = 100;
    public float grabControl = 10;
    float grip = 100;
    public int skillSlots = 8;
    public float critChance = 0.1f;
    public float critAffectChance = 0.5f;
    public float mentalStrainRestoreRate = 10;
    public const float staminaRestoreRate = 10;

    float animSpeedModifier = 1;

    float humanity = 100;

    float bleedTIme = 5;
    float burnTime = 5;

    //public Integer variables

    //Script References - Add them here later
    Strength strength;
    Agility agility;
    Haste haste;
    Perception perception;
    Intelligence intelligence;
    Wisdom wisdom;


    public float speed = 8;
    public float jumpSpeed = 4;
    public float rotateSpeed = 15;
    float gravity = 10;

    private Vector3 moveDirection;
    private Vector3 targetDirection;

    private GameObject lookTarget;

    CharacterController cont;

    private GameObject cameraReference;

    //Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentMS = 0;

        strength = GetComponent<Strength>();
        agility = GetComponent<Agility>();
        haste = GetComponent<Haste>();
        perception = GetComponent<Perception>();
        intelligence = GetComponent<Intelligence>();
        wisdom = GetComponent<Wisdom>();

        cont = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraReference = GameObject.FindWithTag("cameraReference");
        lookTarget = GameObject.FindWithTag("LookTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = Vector3.Normalize(Vector3.Scale(cameraReference.transform.TransformDirection(Vector3.forward), new Vector3(1, 0, 1)));
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        if (cont.isGrounded)
        {
            if (targetDirection != Vector3.zero)
            {
               moveDirection = Vector3.Lerp(moveDirection, targetDirection, rotateSpeed * Time.deltaTime);
               moveDirection = moveDirection.normalized;
            }

            if (Input.GetButton("Jump"))
            {
                moveDirection += Vector3.up * jumpSpeed;
                Debug.Log("jump is pressed");
            }

        }

        if (moveDirection.x != 0 || moveDirection.z != 0 && vertical > 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
        }

        /*if (moveDirection.x != 0 || moveDirection.z != 0 && vertical < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }*/

        //test
        Debug.Log(cont.isGrounded);

        //Transform moveVector to world space
        moveDirection = transform.TransformDirection(moveDirection);

        //normalize moveVector if magnitude > 1
        if (moveDirection.magnitude > 1)
        {
            moveDirection = Vector3.Normalize(moveDirection);
        }

        //Multiply moveVector by moveSpeed
        moveDirection = (horizontal * right + vertical * -forward) * speed;

        //add gravity
        moveDirection.y -= gravity;

        //Multiply moveVector by DeltaTime
        moveDirection *= Time.deltaTime;

        //Move character in world space
        cont.Move(moveDirection);

    }
}
