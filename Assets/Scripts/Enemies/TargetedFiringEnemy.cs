using UnityEngine;
using System.Collections;

public class TargetedFiringEnemy : Enemy
{
	public GameObject bullet;
	public float bulletLifetime = 1.0f;
	public float bulletSpeed = 1.0f;
	public float bulletDamage = 10.0f;
	
	[HideInInspector]
	public GameObject firingTarget;
	
	public float firingDelay = 1.0f;
	
	private float delayTimer = 0.0f;
	// Use this for initialization
	void Start () {
		this.firingTarget = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Check if warp is on
		if (GameObject.FindGameObjectWithTag("Player") != null){
			if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Warp>().isWarping){
				if (this.enemy != null){
					EnemyBehavior script = this.enemy.GetComponent<EnemyBehavior>();
					this.enemy.transform.position += Vector3.left * script.speed * Time.deltaTime;
					
					// Start delay if you are at the current waypoint
					if (this.delayTimer >= this.firingDelay){
						if (this.firingTarget != null){
							GameObject bullet = GameObject.Instantiate( this.bullet, this.enemy.gameObject.transform.position, this.bullet.transform.rotation) as GameObject;
							
							EnemyProjectile pscript = bullet.GetComponent<EnemyProjectile>();
							pscript.direction = (firingTarget.transform.position - this.transform.position).normalized;
							pscript.speed = this.bulletSpeed;
							pscript.damage = this.bulletDamage;
							
							GameObject.Destroy(bullet, this.bulletLifetime);
							Debug.Log("firing");
							this.delayTimer = 0.0f;
						}
					}
					else{
						this.delayTimer += Time.deltaTime;
					}
				}
				else{
					GameObject.Destroy(this.gameObject);
				}
			}
		}
	}
}

