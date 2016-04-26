using UnityEngine;
using System.Collections;

public class CameraSpringArm : MonoBehaviour {

    public bool cameraLag = true;

    public Transform lookTarget;
    public Transform rotTarget;

    public Vector3 eulerAngleDisplacement;


    public float orbitDistance = 5;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float rotationDamping = 5;
    public float movementDamping = 5;
    public float catchUpDampening = 8;

    public float pitchSpeed = 5;
    public float yawSpeed = 5;

    public float pitchMin;
    public float pitchMax;

    public Vector2 rotationInput;
    public Vector2 movementInput;

    public GameObject playerRef;

    private PlayerInputHandler InputHandler;

    private float collisionFix = 5.0f;

    private bool bNeedsReset = false;

	// Use this for initialization
	void Start () {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        rotTarget.rotation = playerRef.transform.rotation;
        InputHandler = playerRef.GetComponent<PlayerInputHandler>();
        bNeedsReset = false;
	}

    public void setInputs(Vector2 _rotation)
    {
        rotationInput = _rotation;
    }

    void Update()
    {
        rotationInput = InputHandler.getCameraAxis();
        movementInput = InputHandler.getMovementAxis();

        if(InputHandler.getCameraReset()){
            bNeedsReset = true;
        }

        rotTarget.position = lookTarget.transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (lookTarget && rotTarget)
        {
            //==MAKE ROTATION REFERENCE PLAYER POSITION==
            Quaternion desiredRotation = transform.rotation;
            Vector3 desiredPosition = transform.position;

            Quaternion oldRot = transform.rotation;
            transform.LookAt(lookTarget);
            desiredRotation = transform.rotation;
            transform.rotation = oldRot;
            

            if(bNeedsReset){
                rotTarget.rotation.Set(rotTarget.rotation.x, rotTarget.rotation.y, 0, 0);

                desiredPosition = transform.position;
                rotTarget.forward = lookTarget.forward;
                desiredPosition = lookTarget.position + (rotTarget.forward * -1) * orbitDistance;

                float nearness = (transform.position - desiredPosition).magnitude;

                if(nearness < 0.5f){
                    bNeedsReset = false;
                }
            }else{
                //==ROTATE THE REFERENCE BASED ON INPUT==
                rotTarget.Rotate(Vector3.up, -rotationInput.x * yawSpeed, Space.World);
                rotTarget.Rotate(Vector3.right, rotationInput.y * pitchSpeed, Space.World);


                rotTarget.rotation.Set(rotTarget.rotation.x, rotTarget.rotation.y, 0, 0);



                float distance = (rotTarget.position - transform.position).magnitude;

                desiredPosition = lookTarget.position + (rotTarget.forward * -1) * distance;

                if (distance > distanceMax)
                {
                    float diff = (distance - distanceMax);
                    desiredPosition += transform.forward * diff;
                }
                else if (distance < distanceMin)
                {
                    float diff = (distance - distanceMin);
                    desiredPosition += transform.forward * diff;
                }
            }


            //==DO CAMERA LAG==
            if (cameraLag)
            {
                //Do lerp adjustment
                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * movementDamping);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationDamping);
            }
            else
            {
                //Do sudden adjustment
                transform.position = desiredPosition;
                transform.rotation = desiredRotation;
            }
        }
	}

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
