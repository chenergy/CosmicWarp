using UnityEngine;
using System.Collections;

public class BasicEnemy : Enemy {
	public GameObject target;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag("Player") != null){
			if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Warp>().isWarping){
				if (this.enemy != null){
					EnemyBehavior script = this.enemy.GetComponent<EnemyBehavior>();
					this.enemy.transform.position += ( (this.target.transform.position - this.enemy.transform.position).normalized * script.speed * Time.deltaTime);
				}
				else{
					GameObject.Destroy(this.gameObject);
				}
			}
		}
	}
}
