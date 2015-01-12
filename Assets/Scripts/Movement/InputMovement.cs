using UnityEngine;
using System.Collections;

public enum MoveDirection { LEFT, RIGHT, UP, DOWN };

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class InputMovement : MonoBehaviour{
	public HeroShip 	entity;
	public float		moveSpeed 		= 1.0f;
	public float 		maxMoveSpeed 	= 1.0f;
	public float 		jumpStrength 	= 1.0f;
	public float 		floatiness 		= 10.0f;
	//public float		attackDuration 	= 1.0f;

	private CharacterController controller;
	private Animator 			animator;
	private Vector3 			movement;
	private bool				isMoving 	= false;
	private bool				canJump 	= true;
	private bool				canAttack 	= true;

	void Start(){
		this.animator = this.GetComponent<Animator> ();
		this.controller = this.GetComponent<CharacterController> ();
	}

	void Update(){
		this.movement.y += Physics.gravity.y * Time.deltaTime * (1 / floatiness);
		this.controller.Move (this.movement);

		this.movement.x = Mathf.Lerp (this.movement.x, 0.0f, Time.deltaTime * 20);

		if (!this.controller.isGrounded) {
			this.animator.SetBool ("isGrounded", false);
			this.animator.speed = 1.0f;
			this.canJump = false;
		} else {
			this.animator.SetBool ("isGrounded", true);
			this.canJump = true;
		}
	}

	public void Move (MoveDirection direction, float amount)
	{
		switch (direction) {
		case MoveDirection.RIGHT:
			this.animator.SetBool ("isMoving", true);
			this.animator.speed = Mathf.Abs (amount * 10);
			this.movement += Vector3.ClampMagnitude (Vector3.right * amount * this.moveSpeed, this.maxMoveSpeed) * Time.deltaTime;
			this.transform.LookAt (this.controller.transform.position + Vector3.forward);
			break;
		case MoveDirection.LEFT:
			this.animator.SetBool ("isMoving", true);
			this.animator.speed = Mathf.Abs (amount * 10);
			this.movement += Vector3.ClampMagnitude(Vector3.left * amount * this.moveSpeed, this.maxMoveSpeed) * Time.deltaTime;
			this.transform.LookAt (this.controller.transform.position + Vector3.back);
			break;
		default:
			break;
		}
	}

	public void StopMovement(){
		this.animator.SetBool ("isMoving", false);
		this.animator.speed = 1.0f;
	}

	public void Jump (){
		if (this.canJump)
			this.movement.y = this.jumpStrength / 10.0f;
	}

	public void SocketAttack(){
		if (this.canAttack) {
			this.animator.SetBool ("isAttacking", true);
			this.animator.speed = 1.0f;
			StartCoroutine (DelayAttack ());
		}
	}

	IEnumerator DelayAttack(){
		while ( this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.95f ) {
			yield return null;
		}

		this.canAttack = true;
		this.animator.SetBool ("isAttacking", false);
	}
}

