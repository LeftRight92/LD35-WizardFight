using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIController : MonoBehaviour {

	public static GUIController instance { get; protected set; }

	public GameObject endTurnButton;
	public Image[] spawnButtons;

	//private Image[] buttons;

	public TurnSign TurnSign;

	// Use this for initialization
	void Awake() {
		if (instance != null) Debug.LogError("Can't have more than one GUIController");
		instance = this;
		//buttons = new GameObject[] {
		//	endTurnButton,
		//	spawnButtons[0], spawnButtons[1], spawnButtons[2], spawnButtons[3]
		//};
	}

	void Start() { 
		FadeOutSpawns();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndTurn() {
		GameController.instance.ReadyAdvanceTurns = true;
	}

	public void SelectNext() {

	}

	public void SelectPrevious() {

	}

	public void SpawnSprite(string type) {
		if (type == "Air") GameController.instance.Spawn(SpriteType.AIR);
		if (type == "Earth") GameController.instance.Spawn(SpriteType.EARTH);
		if (type == "Fire") GameController.instance.Spawn(SpriteType.FIRE);
		if (type == "Water") GameController.instance.Spawn(SpriteType.WATER);
	}

	public void FadeOutSpawns() {
		foreach (Image g in spawnButtons) {
			g.CrossFadeAlpha(0f, 0.25f, false);
		}
	}

	public void FadeInSpawns() {
		foreach (Image g in spawnButtons) {
			g.CrossFadeAlpha(1f, 0.25f, false);
		}
	}

	//public void FadeOutAll() {
	//	foreach (GameObject g in buttons) {
	//		GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, false);
	//	}
	//}

	public void FadeInAll() {

	}
}
