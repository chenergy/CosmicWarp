using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Attributes{
	public float 		xOffset 	= 0.0f;
	public float 		yOffset 	= 0.0f;
	public float 		xScale 		= 1.0f;
	public float 		yScale 		= 1.0f;
	public GUIStyle 	frontStyle;
	public GUIStyle 	backStyle;
	public GUIStyle		textStyle;
}

public class GameTimer : MonoBehaviour {
	public float 		secondsLeft = 300.0f;
	//public Attributes	guiAttributes;
	public GameObject	player;
	//public GameTimer 	gameTimer;

	public Text 		timerText;
	public Slider		timerSlider;
	public Button		restartButton;


	private bool 		destroyed 	= false;
	private float 		maxTimer	= 300.0f;
	private float 		bossBarLength;
	private float 		timer;

	//private GUIText		timerText;
	// Use this for initialization
	void Start () {
		//this.gameTimer = GameObject.Find("GameTimer").GetComponent<GameTimer>();
		//this.timerText = this.gameObject.GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null){
			//Debug.Log(this.secondsLeft);
			Debug.Log(player.GetComponent<Warp>().isWarping);
			if (!player.GetComponent<Warp>().isWarping){
				Debug.Log("still doing this");
				this.secondsLeft -= Time.deltaTime;
				if (this.secondsLeft < 0.0f){
					if (!this.destroyed){
						GameObject deathparts = GameObject.Instantiate(player.GetComponent<HeroShip>().deathParts, player.transform.position, Quaternion.identity) as GameObject;
						GameObject.Destroy(player);
						GameObject.Destroy(deathparts, 5.0f);
						this.destroyed = true;
						this.secondsLeft = 0.0f;
					}
				}
			}
		}

		this.UpdateTime ();
	}


	void UpdateTime(){
		int timeInt = (int)this.secondsLeft;
		string minutes = (Mathf.FloorToInt(this.secondsLeft/60.0f)).ToString();
		string seconds = (timeInt % 60).ToString();
		string milliseconds = (this.secondsLeft - timeInt).ToString();
		milliseconds = milliseconds.Substring(Mathf.Min(milliseconds.Length-1, 2), Mathf.Min(milliseconds.Length, 2));

		if (seconds.Length == 1){
			seconds = "0" + seconds;
		}
		if (milliseconds.Length == 1){
			milliseconds = "0" + milliseconds;
		}

		// Set Time remaining
		if (this.secondsLeft > 0.0f){
			//GUI.Box(new Rect(this.guiAttributes.xOffset, this.guiAttributes.yOffset, 294 * this.guiAttributes.xScale, 39 * this.guiAttributes.yScale), minutes + ":" + seconds + ":" + milliseconds, this.guiAttributes.textStyle );
			this.timerText.text = minutes + ":" + seconds + ":" + milliseconds;
			this.timerSlider.normalizedValue = this.secondsLeft / this.maxTimer;
			//GUI.Box(new Rect(10, Screen.height - 40, 60, 25), "Fuel: " + currentFuel);
			//GUI.Box(new Rect(70, Screen.height - 40, currentFuel, 25), "");
			//GUI.Box(new Rect(Screen.width* 3/5, Screen.height - 40, bossBarLength, 25), "MotherShip");
		}
		else{
			/*if (GUI.Button( new Rect(Screen.width/2.0f - Screen.width/20.0f, Screen.height/2.0f - Screen.width/20.0f, Screen.width/10.0f, Screen.height/10.0f), "Restart")){
				Application.LoadLevel("scene");
			}*/
			restartButton.gameObject.SetActive (true);
		}
	}


	public void ButtonRestart (){
		Application.LoadLevel ("level1");
	}


	/*void OnGUI(){
		int timeInt = (int)this.secondsLeft;
		string minutes = (Mathf.FloorToInt(this.secondsLeft/60.0f)).ToString();
		string seconds = (timeInt % 60).ToString();
		string milliseconds = (this.secondsLeft - timeInt).ToString();
		milliseconds = milliseconds.Substring(Mathf.Min(milliseconds.Length-1, 2), Mathf.Min(milliseconds.Length, 2));
		
		if (seconds.Length == 1){
			seconds = "0" + seconds;
		}
		if (milliseconds.Length == 1){
			milliseconds = "0" + milliseconds;
		}
		
		// Set Time remaining
		if (this.secondsLeft >= 0.0f){
			//GUI.Box(new Rect(this.guiAttributes.xOffset, this.guiAttributes.yOffset, 294 * this.guiAttributes.xScale, 39 * this.guiAttributes.yScale), minutes + ":" + seconds + ":" + milliseconds, this.guiAttributes.textStyle );
			this.timerText.text = minutes + ":" + seconds + ":" + milliseconds;
			//GUI.Box(new Rect(10, Screen.height - 40, 60, 25), "Fuel: " + currentFuel);
			//GUI.Box(new Rect(70, Screen.height - 40, currentFuel, 25), "");
			//GUI.Box(new Rect(Screen.width* 3/5, Screen.height - 40, bossBarLength, 25), "MotherShip");
		}
		else{
			if (GUI.Button( new Rect(Screen.width/2.0f - Screen.width/20.0f, Screen.height/2.0f - Screen.width/20.0f, Screen.width/10.0f, Screen.height/10.0f), "Restart")){
				Application.LoadLevel("scene");
			}
		}
		
		// Boss UI

		//if (GameObject.FindGameObjectWithTag("Boss") != null){
		//	float currentHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyMothership>().currentHealth;
		//	float maxHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<EnemyMothership>().totalHealth;
		//	this.bossBarLength = (Screen.width / 6) * (currentHealth/maxHealth);
		//}
	}*/
	
	public void addTime(float amount){
		if(secondsLeft > 0){
			secondsLeft += amount;
		}
	}
}
