using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector2 getMovementAxis()
    {
        Vector2 movement = new Vector2(Input.GetAxis("HorizontalMovement"), Input.GetAxis("VerticalMovement"));
        return movement;
    }

    public Vector2 getCameraAxis()
    {
        Vector2 rotation = new Vector2(Input.GetAxis("VerticalCamera"), Input.GetAxis("HorizontalCamera"));
        return rotation;
    }
}
