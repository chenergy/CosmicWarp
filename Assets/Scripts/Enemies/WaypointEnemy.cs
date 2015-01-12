using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointEnemy : Enemy
{
	public float delay = 1.0f;
	public List<GameObject> waypoints;
	
	private GameObject currentTarget;
	private int currentWaypoint = 0;
	private float delayTimer = 0.0f;
	// Use this for initialization
	void Start ()
	{
		this.currentTarget = this.waypoints[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check if warp is on
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
				}
				else{
					GameObject.Destroy(this.gameObject);
				}
			}
		}
	}
}

