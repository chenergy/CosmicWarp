using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	public InputMovement[] 	controlled;
	public CustomDragObject	joystickDragObject;
	public UISprite 		directionSprite;

	private string 		horizontalAxis;
	private string		verticalAxis;
	//private string		jumpBtn;
	//private string		normalAtkBtn;
	//private string		specialAtkBtn;
	private Vector3 	startPosition;
	private Vector3 	curPosition;

	void Start(){
		foreach (InputMovement i in this.controlled) {
			this.horizontalAxis = "Horizontal";
			this.verticalAxis 	= "Vertical";
			//this.jumpBtn 		= GameController.Platform.ToString () + "_Jump_" + i.entity.playerNum.ToString ();
			//this.normalAtkBtn 	= GameController.Platform.ToString () + "_NormalAttack_" + i.entity.playerNum.ToString ();
			//this.specialAtkBtn 	= GameController.Platform.ToString () + "_SpecialAttack_" + i.entity.playerNum.ToString ();
		}

		this.startPosition = this.joystickDragObject.transform.position;
		this.curPosition = this.startPosition;
	}

	void Update(){
		// Mouse/Touch
		this.curPosition = this.joystickDragObject.transform.position;

		foreach (InputMovement i in this.controlled) {
			Vector3 diff = Vector3.ClampMagnitude(this.curPosition - this.startPosition, 0.1f);
			this.joystickDragObject.transform.position = this.startPosition + diff;
			this.directionSprite.transform.up = diff;

			if (diff.sqrMagnitude > 0.001f) {
				if (diff.x < 0) {
					i.Move (MoveDirection.LEFT, -diff.x);
				} else if (diff.x > 0) {
					i.Move (MoveDirection.RIGHT, diff.x);
				}
			} else {
				i.StopMovement ();
			}

			if (!this.joystickDragObject.isPressed) {
				this.joystickDragObject.transform.position = this.startPosition;
			}
		}

		// Keyboard
		/*
		foreach (InputMovement i in this.controlled) {
			if (Input.GetAxis (this.horizontalAxis) > 0.01f) {
				i.Move (MoveDirection.RIGHT, Input.GetAxis (this.horizontalAxis));
			} else if (Input.GetAxis (this.horizontalAxis) < -0.01f) {
				i.Move (MoveDirection.LEFT, -Input.GetAxis (this.horizontalAxis));
			} else {
				i.StopMovement ();
			}

			if (Input.GetButtonDown (this.jumpBtn)) {
				i.Jump ();
			}
		}
		*/
	}

	public void Jump(){
		foreach (InputMovement i in this.controlled) {
			i.Jump ();
		}
	}

	public void SocketAttack(){
		foreach (InputMovement i in this.controlled) {
			i.SocketAttack ();
		}
	}
}
