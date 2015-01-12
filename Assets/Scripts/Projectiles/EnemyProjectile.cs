using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
	[HideInInspector]
	public Vector3 direction;
	public float speed;
	public float damage;
	public GameObject destroyParticles;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.gameObject.transform.position += direction * this.speed * Time.deltaTime;
	}
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Player"){
			HeroShip script = other.GetComponent<HeroShip>();
			
			if (!script.isInvulnerable){
				GameTimer timer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
				timer.secondsLeft -= this.damage;
				/*
				script.health -= this.damage;
				*/
				GameObject particle = GameObject.Instantiate(this.destroyParticles, this.gameObject.transform.position, this.destroyParticles.transform.rotation) as GameObject;
				GameObject.Destroy(particle, 0.9f);
				Destroy(this.gameObject);
			}
		}
	}
}

