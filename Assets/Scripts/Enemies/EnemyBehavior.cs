using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
	public float speed = 1.0f;
	public float damage = 10.0f;
	public float health = 10.0f;
	public GameObject deathParts;
	public GameObject deathParticles;
	public AudioClip explodeE;
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Player"){
			HeroShip script = other.GetComponent<HeroShip>();
			
			if (!script.isInvulnerable){
				/*
				script.health -= this.damage;
				*/
				GameTimer timer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
				timer.secondsLeft -= this.damage;
				GameObject deathParts = GameObject.Instantiate( this.deathParts, this.transform.position, this.deathParts.transform.rotation) as GameObject;
				GameObject.Destroy( deathParts, 1.0f );
				GameObject deathParticles = GameObject.Instantiate(this.deathParticles, this.transform.position, this.deathParticles.transform.rotation) as GameObject;
				GameObject.Destroy ( deathParticles, 0.9f );
				Destroy(this.gameObject);
			}
		}
	}
	
	void Update(){
		if (this.health <= 0.0f){
			GameObject deathParts = GameObject.Instantiate( this.deathParts, this.transform.position, this.deathParts.transform.rotation) as GameObject;
			GameObject.Destroy( deathParts, 1.0f );
			GameObject deathParticles = GameObject.Instantiate(this.deathParticles, this.transform.position, this.deathParticles.transform.rotation) as GameObject;
			GameObject.Destroy ( deathParticles, 0.9f );
			this.transform.parent.gameObject.GetComponent<Enemy>().dropLoot();
			AudioSource.PlayClipAtPoint(explodeE, transform.position);
			Destroy(this.gameObject);
		}
	}
}