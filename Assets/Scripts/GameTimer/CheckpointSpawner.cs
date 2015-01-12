using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CheckpointTimer{
	public float spawnTime = 0.0f;
	public float speed = 1.0f;
	public float regain = 10.0f;
	public GameObject checkpoint;
	public Vector3 spawnPosition;
	
	[HideInInspector]
	public bool spawned = false;
}

public class CheckpointSpawner : MonoBehaviour {
	public float maxCheckpointLifetime = 20.0f;
	public float spawnTimeOffset = 0.0f;
	public List<CheckpointTimer> checkpoints;
	
	[HideInInspector]
	public float timer;

	
	void Start () {
		foreach (CheckpointTimer c in this.checkpoints){
			Checkpoint script = c.checkpoint.GetComponent<Checkpoint>();
			script.speed = c.speed;
			script.regain = c.regain;
		}
	}
	
	
	void Update () {
		if (GameObject.FindGameObjectWithTag("Player") != null){
			// Check each spawn timer
			foreach (CheckpointTimer c in this.checkpoints){
				if (!c.spawned){
					if (this.timer >= c.spawnTime + this.spawnTimeOffset){
						GameObject checkpoint = GameObject.Instantiate( c.checkpoint, c.spawnPosition, c.checkpoint.transform.rotation ) as GameObject;
						GameObject.Destroy(checkpoint, this.maxCheckpointLifetime );
						c.spawned = true;
					}
				}
			}
			this.timer += Time.deltaTime;
		}
	}
}
