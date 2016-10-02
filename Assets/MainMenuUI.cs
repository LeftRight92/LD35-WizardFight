using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

	public void Play() {
		SceneManager.LoadScene("Game");
	}

	public void Instructions() {
		SceneManager.LoadScene("Instructions");
	}
}
