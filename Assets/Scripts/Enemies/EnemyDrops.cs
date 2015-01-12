using UnityEngine;
using System.Collections;

public class EnemyDrops : MonoBehaviour {
	[HideInInspector]
	public Vector3 direction;
	
	public float speed = 2.0f;
	public float rotateX = 180f;
	public float rotateY = 180f;
	public float rotateZ = 180f;
	
	public void DropsMovement(){
		this.gameObject.transform.position += direction * this.speed * Time.deltaTime;
		//this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, direction * this.speed, Time.deltaTime);
		this.gameObject.transform.Rotate(Vector3.right * Time.deltaTime);
		this.gameObject.transform.GetChild(0).Rotate(this.rotateX* Time.deltaTime, this.rotateX* Time.deltaTime, this.rotateZ * Time.deltaTime) ;	
	}
}
