using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlatformString { OSX, WIN };

public class GameController : MonoBehaviour{
	private static GameController instance = null;
	private PlatformString platform;

	void Awake(){
		if (instance != null) {
			Destroy (this.gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (this.gameObject);

			if (Application.platform == RuntimePlatform.OSXPlayer) {
				this.platform = PlatformString.OSX;
			} else if (Application.platform == RuntimePlatform.WindowsPlayer) {
				this.platform = PlatformString.WIN;
			}
		}
	}

	public static PlatformString Platform { get { return instance.platform; } }
}

