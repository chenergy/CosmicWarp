using UnityEngine;
using System.Collections;

public class HeroShip : MonoBehaviour {
	public float fireRate = 0.25f;
	public float bulletLifetime = 2.0f;
	public float bulletSpeed = 1.0f;
	public float moveSpeed = 10.0f;
	public string weapon = "";
	public GameObject bullet;
	public GameObject deathParts;
	public float minX = -10.0f;
	public float minY = -8.0f;
	public float maxX = 10.0f;
	public float maxY = 8.0f;
	public AudioClip shoot;
	
	[HideInInspector]
	public bool isInvulnerable = false;
	[HideInInspector]
	public float health = 100.0f;
	
	[HideInInspector]
	public float fuel = 100.0f;
	
	[HideInInspector]
	public float fireTimer = 0.0f;
	
	[HideInInspector]
	public Collider collider;
	
	private Vector3 direction;
	// Use this for initialization
	void Start () {
		this.direction = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.GetComponent<Warp>().isWarping){
			//Debug.Log(this.health);
			// Up
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0){
				//this.direction.y = this.moveSpeed * 0.01f;
				//this.gameObject.transform.position = this.gameObject.transform.position + Vector3.Lerp( this.direction, Vector3.zero, Time.deltaTime );
				this.gameObject.transform.position += Vector3.up * Time.deltaTime * this.moveSpeed;
				this.gameObject.transform.rotation = Quaternion.Slerp( this.gameObject.transform.rotation, Quaternion.Euler( new Vector3( 60, 0, 0 ) ), Time.deltaTime * 3.0f );
			}
			// Down
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0){
				//this.direction.y = -this.moveSpeed * 0.01f;
				//this.gameObject.transform.position = this.gameObject.transform.position + Vector3.Lerp( this.direction, Vector3.zero, Time.deltaTime );
				this.gameObject.transform.position += Vector3.down * Time.deltaTime * this.moveSpeed;
				this.gameObject.transform.rotation = Quaternion.Slerp( this.gameObject.transform.rotation, Quaternion.Euler( new Vector3( -60, 0, 0 ) ), Time.deltaTime * 3.0f );
			}
			// Right
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0){
				//this.direction.x = this.moveSpeed * 0.01f;
				//this.gameObject.transform.position = this.gameObject.transform.position + Vector3.Lerp( this.direction, Vector3.zero, Time.deltaTime );
				this.gameObject.transform.position += Vector3.right * Time.deltaTime * this.moveSpeed;
			}
			// Left
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0){
				//this.direction.x = -this.moveSpeed * 0.01f;
				//this.gameObject.transform.position = this.gameObject.transform.position + Vector3.Lerp( this.direction, Vector3.zero, Time.deltaTime );
				this.gameObject.transform.position += Vector3.left * Time.deltaTime * this.moveSpeed;
			}
			// Fire a bullet
			if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button0)){
				this.fireTimer += Time.deltaTime;
				if (this.fireTimer >= this.fireRate){
					this.fireTimer = 0.0f;
				}
				// fire
				if (this.fireTimer == 0.0f){
					GameObject bullet = GameObject.Instantiate( this.bullet, this.gameObject.transform.position, this.bullet.transform.rotation) as GameObject;
					PlayerProjectile bulletScript = bullet.GetComponent<PlayerProjectile>();
					bulletScript.speed = this.bulletSpeed * this.bulletSpeed;
					
					GameObject.Destroy(bullet, this.bulletLifetime);
					AudioSource.PlayClipAtPoint(shoot, transform.position);
					Debug.Log("firing");
				}
			}
			else{
				this.fireTimer = this.fireRate;
			}
			this.gameObject.transform.rotation = Quaternion.Slerp( this.gameObject.transform.rotation, Quaternion.identity, Time.deltaTime * 2.0f );
			
			this.gameObject.transform.position = new Vector3( Mathf.Clamp( this.gameObject.transform.position.x, this.minX, this.maxX),
				Mathf.Clamp( this.gameObject.transform.position.y, this.minY, this.maxY),
				0);
		}
	}
	
	public void SelfDestruct(){
		
	}
}
