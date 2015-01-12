using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour
{
	/*
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Enemy")){
			if (this.gameObject.transform.position.x < -10){
				Debug.Break();
				GameObject.Destroy(gobj);
			}
		}
	}
	*/
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy"){
			GameObject.Destroy(other.transform.parent.gameObject);
		}
	}
}

