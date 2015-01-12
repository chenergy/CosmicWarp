using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
	public float slowDownSpeed = 5.0f;
	public float timeBeforeSpeedup = 0.2f;
	public Material trail;
	public GameObject stars;
	public GameObject preWarpParticle;
	public GameObject warpSpeedParticle;
	public GameObject whileWarpingParticle;
	public GameObject returnToNormal;
	public AudioClip preWarp;
	public AudioClip postWarp;
	public AudioClip endWarp;
	
	[HideInInspector]
	public bool isWarping = false;
	
	private float timer;
	private bool preWarpCreated = false;
	private bool warpSpeedCreated = false;
	private bool trailsCreated = false;
	private bool whileWarpingCreated = false;
	private GameObject whileWarpingInstance;
	
	private bool returnToNormalCreated = true;
	
	void Update () {
		HealthLivesFuel hlf = GameObject.Find("Health").GetComponent<HealthLivesFuel>();
		
		if (hlf.currentFuel > 0.0f){
			if (Input.GetKey(KeyCode.Tab) || Input.GetKey(KeyCode.Joystick1Button1)){
				this.returnToNormalCreated = false;
				this.isWarping = true;
				
				if (this.timer < this.timeBeforeSpeedup){
					Time.timeScale = Mathf.Lerp( Time.timeScale, 0.0f, Time.deltaTime * this.slowDownSpeed);
					Camera.main.fieldOfView = Mathf.Lerp( Camera.main.fieldOfView, 30, Time.deltaTime );
					
					if (!this.preWarpCreated){
						Time.timeScale = 1.0f;
						GameObject preWarpParticle = GameObject.Instantiate(this.preWarpParticle, this.gameObject.transform.position, this.preWarpParticle.transform.rotation) as GameObject;
						GameObject.Destroy(preWarpParticle, 1.0f);
						AudioSource.PlayClipAtPoint(this.preWarp, Camera.main.transform.position + new Vector3(0,0,3));
						this.preWarpCreated = true;
						// create the trails
					}
					
					if (!this.warpSpeedCreated){
						if (this.timer > (this.timeBeforeSpeedup - 0.1f)){
							GameObject warpSpeedParticle = GameObject.Instantiate(this.warpSpeedParticle, this.gameObject.transform.position, this.warpSpeedParticle.transform.rotation) as GameObject;
							GameObject.Destroy(warpSpeedParticle, 1.0f);
							this.warpSpeedCreated = true;
						}
					}
				}
				else{
					GameObject.Find("Health").GetComponent<HealthLivesFuel>().currentFuel -= 1.0f;
				
					if (GameObject.Find("Health").GetComponent<HealthLivesFuel>().currentFuel < 0.0f){
						GameObject.Find("Health").GetComponent<HealthLivesFuel>().currentFuel = 0.0f;
					}
					
					if (Mathf.Abs(60 - Camera.main.fieldOfView) < 0.1f){
						Camera.main.fieldOfView = 60;
					}
					else{
						Camera.main.fieldOfView = Mathf.Lerp( Camera.main.fieldOfView, 60, Time.deltaTime * 4.0f );
					}
					
					if (!this.whileWarpingCreated){
						this.whileWarpingInstance = GameObject.Instantiate(this.whileWarpingParticle, Vector3.zero, this.whileWarpingParticle.transform.rotation) as GameObject;
						//AudioSource.PlayClipAtPoint(this.postWarp, this.gameObject.transform.position);
						this.whileWarpingCreated = true;
					}
					
					Time.timeScale = Mathf.Lerp( Time.timeScale, 10.0f, Time.deltaTime * this.slowDownSpeed);
					foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Enemy")){
						if (gobj.GetComponent<TrailRenderer>() == null){
							gobj.AddComponent<TrailRenderer>();
							TrailRenderer trail = gobj.GetComponent<TrailRenderer>();
							trail.material = this.trail;
							trail.time = 100.0f;
							trail.endWidth = 5.0f;
						}
						gobj.transform.Translate(Vector3.right * Time.deltaTime * 50.0f);
					}
					
					foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Drop")){
						if (gobj.GetComponent<TrailRenderer>() == null){
							gobj.AddComponent<TrailRenderer>();
							TrailRenderer trail = gobj.GetComponent<TrailRenderer>();
							trail.material = this.trail;
							trail.time = 100.0f;
							trail.endWidth = 5.0f;
						}
						gobj.transform.Translate(Vector3.right * Time.deltaTime * 50.0f);
					}
					
					this.GetComponent<HeroShip>().isInvulnerable = true;
				}
			}
			else{
				if (this.returnToNormalCreated == false){
					GameObject returnToNormal = GameObject.Instantiate(this.returnToNormal, this.gameObject.transform.position, this.returnToNormal.transform.rotation) as GameObject;
					GameObject.Destroy(returnToNormal, 1.0f);
					//AudioSource.PlayClipAtPoint(this.endWarp, this.gameObject.transform.position);
					this.returnToNormalCreated = true;
				}
				
				if (Mathf.Abs(60 - Camera.main.fieldOfView) < 0.1f){
					Camera.main.fieldOfView = 60;
				}
				else{
					Camera.main.fieldOfView = Mathf.Lerp( Camera.main.fieldOfView, 60, Time.deltaTime );
				}
				
				if (this.whileWarpingInstance != null){
					GameObject.Destroy(this.whileWarpingInstance);
					this.whileWarpingInstance = null;
				}
				if (Time.timeScale < 1.0f){
					Time.timeScale = Mathf.Lerp( Time.timeScale, 1.0f, Time.deltaTime * this.slowDownSpeed * 2.0f);
				}
				else{
					Time.timeScale = Mathf.Lerp( Time.timeScale, 1.0f, Time.deltaTime * this.slowDownSpeed);
				}
				
				this.isWarping = false;
				this.GetComponent<HeroShip>().isInvulnerable = false;
				this.timer = 0.0f;
				this.preWarpCreated = false;
				this.trailsCreated = false;
				this.warpSpeedCreated = false;
				this.whileWarpingCreated = false;
				
				// Remove all trails
				foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Enemy")){
					if (gobj.GetComponent<TrailRenderer>() != null){
						gobj.GetComponent<TrailRenderer>().enabled = false;
					}
				}
			}
		}
		else{
			if (this.returnToNormalCreated == false){
				GameObject returnToNormal = GameObject.Instantiate(this.returnToNormal, this.gameObject.transform.position, this.returnToNormal.transform.rotation) as GameObject;
				GameObject.Destroy(returnToNormal, 1.0f);
				this.returnToNormalCreated = true;
			}
			
			if (Mathf.Abs(60 - Camera.main.fieldOfView) < 0.1f){
				Camera.main.fieldOfView = 60;
			}
			else{
				Camera.main.fieldOfView = Mathf.Lerp( Camera.main.fieldOfView, 60, Time.deltaTime );
			}
			
			if (this.whileWarpingInstance != null){
				GameObject.Destroy(this.whileWarpingInstance);
				this.whileWarpingInstance = null;
			}
			if (Time.timeScale < 1.0f){
				Time.timeScale = Mathf.Lerp( Time.timeScale, 1.0f, Time.deltaTime * this.slowDownSpeed * 2.0f);
			}
			else{
				Time.timeScale = Mathf.Lerp( Time.timeScale, 1.0f, Time.deltaTime * this.slowDownSpeed);
			}
			
			this.isWarping = false;
			this.GetComponent<HeroShip>().isInvulnerable = false;
			this.timer = 0.0f;
			this.preWarpCreated = false;
			this.trailsCreated = false;
			this.warpSpeedCreated = false;
			this.whileWarpingCreated = false;
			
			// Remove all trails
			foreach (GameObject gobj in GameObject.FindGameObjectsWithTag("Enemy")){
				if (gobj.GetComponent<TrailRenderer>() != null){
					gobj.GetComponent<TrailRenderer>().enabled = false;
				}
			}
		}
		this.timer += Time.deltaTime;
	}
}
