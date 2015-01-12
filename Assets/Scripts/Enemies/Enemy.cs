using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	public GameObject enemy;
	public List<GameObject> drops;
	
	public void dropLoot(){
		if(drops.Count > 0){
			int item = Random.Range(0, this.drops.Count);
			GameObject loot = GameObject.Instantiate(this.drops[item], this.enemy.gameObject.transform.position, this.enemy.gameObject.transform.rotation) as GameObject;
			GameObject.Destroy( loot, 5.0f );
		}
	}
}

