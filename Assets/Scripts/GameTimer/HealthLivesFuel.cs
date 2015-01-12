using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthLivesFuel : MonoBehaviour {
	//public GUIStyle		healthBar;
	//public GUIStyle		backBar;
	public float 		currentFuel = 0.0f;
	public float 		maxFuel 	= 100.0f;

	public Slider 		fuelSlider;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*void OnGUI(){
		GUI.Box(new Rect(10, Screen.height - 40, 60, 25), "Fuel: " + currentFuel);
		GUI.Box(new Rect(70, Screen.height - 40, currentFuel, 25), "");
	}*/
	
	public void addFuel(float amount){
		if(currentFuel < maxFuel){			
			currentFuel += amount;
		}

		this.fuelSlider.normalizedValue = (this.currentFuel / this.maxFuel);
	}
}
