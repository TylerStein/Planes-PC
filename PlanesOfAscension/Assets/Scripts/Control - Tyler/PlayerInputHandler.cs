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
        Vector2 rotation = new Vector2(Input.GetAxis("HorizontalCamera"), Input.GetAxis("VerticalCamera"));
        
        rotation.x = Mathf.Clamp(rotation.x, -1, 1);
        rotation.y = Mathf.Clamp(rotation.y, -1, 1);

        
        return rotation;
    }

    public bool getCameraReset()
    {
        if (Input.GetAxis("ResetCamera") > 0.1f)
        {
            return true;
        }

        return false;
    }
}
