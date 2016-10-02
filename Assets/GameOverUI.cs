using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

	public Sprite red, blue;
	public Image winMessage;

	// Use this for initialization
	void Start () {
		if(GlobalState.instance.WinningPlayer == Team.RED) {
			winMessage.sprite = red;
		} else {
			winMessage.sprite = blue;
		}
	}

	public void MainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
