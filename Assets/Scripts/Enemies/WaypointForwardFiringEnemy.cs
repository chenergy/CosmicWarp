using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointForwardFiringEnemy : Enemy
{
	public float delay = 1.0f;
	public List<GameObject> waypoints;
	
	private GameObject currentTarget;
	private int currentWaypoint = 0;
	private float delayTimer = 0.0f;
	private float bulletDelayTimer = 0.0f;
	
	// Bullet Attributes
	public GameObject bullet;
	public float bulletLifetime = 1.0f;
	public float bulletSpeed = 1.0f;
	public float bulletDamage = 10.0f;
	[HideInInspector]
	public GameObject firingTarget;
	public float firingDelay = 1.0f;
	
	// Use this for initialization
	void Start ()
	{
		this.currentTarget = this.waypoints[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindGameObjectWithTag("Player") != null){
			if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Warp>().isWarping){
				if (this.currentWaypoint < this.waypoints.Count - 1){
					if (this.delayTimer >= this.delay){
						this.currentWaypoint++;
						this.currentTarget = this.waypoints[this.currentWaypoint];
						this.delayTimer = 0.0f;
					}
				}
				if (this.enemy != null){
					EnemyBehavior script = this.enemy.GetComponent<EnemyBehavior>();
					this.enemy.transform.position += ( (this.currentTarget.transform.position - this.enemy.transform.position).normalized * script.speed * Time.deltaTime);
					
					// Start delay if you are at the current waypoint
					if ((this.enemy.transform.position - this.currentTarget.transform.position).magnitude < 0.1f){
						this.enemy.transform.position = this.currentTarget.transform.position;
						this.delayTimer += Time.deltaTime;
					}
					
					// Bullet handling
					if (this.bulletDelayTimer >= this.firingDelay){
						GameObject bullet = GameObject.Instantiate( this.bullet, this.enemy.gameObject.transform.position, this.bullet.transform.rotation) as GameObject;
						
						EnemyProjectile pscript = bullet.GetComponent<EnemyProjectile>();
						pscript.direction = Vector3.left;
						pscript.speed = this.bulletSpeed;
						pscript.damage = this.bulletDamage;
						
						GameObject.Destroy(bullet, this.bulletLifetime);
						Debug.Log("firing");
						this.bulletDelayTimer = 0.0f;
					}
					else{
						this.bulletDelayTimer += Time.deltaTime;
					}
				}
				else{
					GameObject.Destroy(this.gameObject);
				}
			}
		}
	}
}

