using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyTimer{
	public float spawnTime = 0.0f;
	public GameObject enemy;
	public float speed = 1.0f;
	public float damage = 10.0f;
	public Vector3 spawnPosition;
	
	[HideInInspector]
	public bool spawned = false;
}

public class EnemySpawnController : MonoBehaviour {
	public float maxEnemyLifetime = 20.0f;
	public float spawnTimeOffset = 0.0f;
	public List<EnemyTimer> enemies;
	
	[HideInInspector]
	public float timer;

	
	void Start () {
		foreach (EnemyTimer e in this.enemies){
			if (e.enemy.GetComponent<Enemy>().enemy.tag == "Enemy"){
				EnemyBehavior script = e.enemy.GetComponent<Enemy>().enemy.GetComponent<EnemyBehavior>();
				script.speed = e.speed;
				script.damage = e.damage;
			}
		}
	}
	
	void Update () {
		// Check each spawn timer
		foreach (EnemyTimer e in this.enemies){
			if (!e.spawned){
				if (this.timer >= e.spawnTime + this.spawnTimeOffset){
					GameObject enemy = GameObject.Instantiate( e.enemy, e.spawnPosition, e.enemy.transform.rotation ) as GameObject;
					if (this.maxEnemyLifetime != 0.0f){
						GameObject.Destroy(enemy, this.maxEnemyLifetime );
					}
					e.spawned = true;
				}
			}
		}
		this.timer += Time.deltaTime;
	}
}
