using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class CameraMovement : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 5F;
    public float sensitivityY = 5F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public static GameObject player;

    private bool cameraAdjusting = false;



    float rotationY = 0F;

    // Use this for initialization
    void Start () {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        player = GameObject.FindWithTag("ReferenceTarget");
    }
	
	// Update is called once per frame
	void Update () {
	    if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("RightX") * sensitivityX;

            rotationY += Input.GetAxis("RightY") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }

        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("RightX") * sensitivityX, 0);
        }

        else
        {
            rotationY += Input.GetAxis("RightY") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(rotationY, transform.localEulerAngles.y, 0);
        }

        if (Input.GetButtonDown("RecenterCamera") )
        {

            StartCoroutine(LerpVector(transform.position, player.transform.position, 0.2f));
            StartCoroutine(LerpQuaternion(transform.rotation, player.transform.rotation, 0.2f));
        }
	}

    IEnumerator LerpVector(Vector3 startPos, Vector3 endPos, float time)
    {
        float startTime = Time.time;
        while (Time.time < startTime + time)
        {
            transform.position = Vector3.Lerp(startPos, player.transform.position, (Time.time - startTime) / time);
            yield return null;
        }
        transform.position = endPos;
    }

    IEnumerator LerpQuaternion(Quaternion startPos, Quaternion endPos, float time)
    {
        float startTime = Time.time;
        while (Time.time < startTime + time)
        {
            transform.rotation = Quaternion.Lerp(startPos, player.transform.rotation, (Time.time - startTime) / time);
            yield return null;
        }
        transform.rotation = endPos;
    }
}
