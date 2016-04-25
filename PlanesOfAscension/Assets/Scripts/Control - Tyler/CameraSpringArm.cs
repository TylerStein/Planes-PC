using UnityEngine;
using System.Collections;

public class CameraSpringArm : MonoBehaviour {

    public bool cameraLag = true;

    public Transform lookTarget;

    public Vector3 eulerAngleDisplacement;

    public float orbitDistance = 5;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float rotationDamping = 5;
    public float movementDamping = 5;

    public float pitchSpeed = 5;
    public float yawSpeed = 5;

    public float pitchMin;
    public float pitchMax;

    public Vector2 rotationInput;

    public GameObject playerRef;

	// Use this for initialization
	void Start () {
        playerRef = GameObject.FindGameObjectWithTag("Player");
	}

    public void setInputs(Vector2 _rotation)
    {
        rotationInput = _rotation;
    }

    void Update()
    {
        rotationInput = playerRef.GetComponent<PlayerInputHandler>().getCameraAxis();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (lookTarget)
        {
            //Make a desired position vector
            Vector3 desiredPosition = lookTarget.transform.position;

            //Get target rotation and inverse the y
            Vector3 targetInverseRotation = lookTarget.eulerAngles; 
            targetInverseRotation.y *= -1;

            //Get the displacement vector between target and desired location
            Vector3 displacement = targetInverseRotation.normalized;

            //Rotate the target rotation
            targetInverseRotation.x += rotationInput.y * Time.deltaTime;
            targetInverseRotation.y += rotationInput.x * Time.deltaTime;

            //Clamp the pitch
            targetInverseRotation.x = ClampAngle(targetInverseRotation.x, pitchMin, pitchMax);

            //Add distance to the displacement
            displacement *= orbitDistance;
            
            //Add dispalcement to real-world position
            desiredPosition += displacement;

            //Store current rotation
            Quaternion oldRot = transform.rotation;

            //Look towards player (always)
            transform.LookAt(lookTarget);

            //Apply angle displacement
            transform.Rotate(eulerAngleDisplacement, Space.Self);

            //Store the new rotation
            Quaternion desiredRotation = transform.rotation;

            //Set the rotation back
            transform.rotation = oldRot;


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
