using UnityEngine;
using System.Collections;

public class MothershipMovement : MonoBehaviour
{
	private float timer = 0.0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.timer >= 360){
			this.timer = 0.0f;
		}
		this.transform.position = new Vector3(this.transform.position.x, Mathf.Sin(this.timer) * 3.0f, this.transform.position.z);
		this.timer += Time.deltaTime;
	}
}

