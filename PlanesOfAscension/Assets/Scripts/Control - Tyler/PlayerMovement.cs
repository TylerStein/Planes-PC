using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    PlayerInputHandler InputHandler;
    CharacterController controller;

    public float ForwardMoveScale = 2.0f;
    public float HorizontalMoveScale = 2.0f;

    Vector2 movementInput;

	// Use this for initialization
	void Start () {
        InputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponentInChildren<CharacterController>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        movementInput = InputHandler.getMovementAxis();

        if (controller.velocity != Vector3.zero)
        {
            Vector3 vel = controller.velocity;
            vel.y = 0;
            controller.transform.forward = vel;
        }

        float forwardMovement = movementInput.y * ForwardMoveScale;
        float rightMovement = movementInput.x * HorizontalMoveScale;

        Vector3 relativeForward = Camera.main.transform.forward;
        Vector3 relativeRight = Camera.main.transform.right;

        Vector3 movement = new Vector3();

        movement += relativeForward * forwardMovement * ForwardMoveScale;
        movement += relativeRight * rightMovement * HorizontalMoveScale;

        movement += Physics.gravity;

        controller.Move(movement * Time.deltaTime);
	}
}
