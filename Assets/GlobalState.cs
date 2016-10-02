using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GlobalState : MonoBehaviour {
	public static GlobalState instance;

	public Team WinningPlayer { get; set; }

	// Use this for initialization
	void Start () {
		instance = this;
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene("MainMenu");
	}
	
}
