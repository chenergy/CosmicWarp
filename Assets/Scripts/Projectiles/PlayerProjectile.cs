using UnityEngine;
using System.Collections;

public class PlayerProjectile : MonoBehaviour {
	public float speed = 1.0f;
	public float damage = 10.0f;
	public GameObject destroyParticles;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Translate( Vector3.right * this.speed * Time.deltaTime );
	}
	
	void OnTriggerEnter( Collider other ){
		if (other.tag == "Enemy"){
			EnemyBehavior script = other.GetComponent<EnemyBehavior>();
			script.health -= this.damage;
			GameObject particle = GameObject.Instantiate(this.destroyParticles, this.gameObject.transform.position, this.destroyParticles.transform.rotation) as GameObject;
			GameObject.Destroy(particle, 0.9f);
			GameObject.Destroy(this.gameObject);
		}
		if (other.tag == "Boss"){
			EnemyMothership mothership = other.GetComponent<EnemyMothership>();
			mothership.currentHealth -= this.damage;
			GameObject particle = GameObject.Instantiate(this.destroyParticles, this.gameObject.transform.position, this.destroyParticles.transform.rotation) as GameObject;
			GameObject.Destroy(particle, 0.9f);
			GameObject.Destroy(this.gameObject);
		}
	}
}
