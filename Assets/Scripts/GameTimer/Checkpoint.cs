using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	public float regain;
	public float speed;
	public GameObject regainParticle;
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Player"){
			HeroShip script = other.GetComponent<HeroShip>();
			
			if (!script.isInvulnerable){
				/*
				script.health -= this.damage;
				*/
				GameTimer timer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
				timer.secondsLeft += this.regain;
				
				GameObject particle = GameObject.Instantiate( this.regainParticle, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity ) as GameObject;
				GameObject.Destroy(particle, 1.0f );
				
				this.gameObject.collider.enabled = false;
			}
		}
	}
	
	void Update(){
		this.gameObject.transform.Translate( Vector3.left * this.speed * Time.deltaTime );
	}
}