using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnSign : MonoBehaviour {

	public Sprite RedTurn, BlueTurn;

	// Use this for initialization
	void Start () {
		GetComponent<Image>().CrossFadeAlpha(0f, 0f, true);
		DoCrossfade(Team.RED);
	}
	
	public void DoCrossfade(Team team) {
		StartCoroutine(Crossfade(team == Team.BLUE ? BlueTurn : RedTurn));
	}

	IEnumerator Crossfade(Sprite sprite) {
		GetComponent<Image>().sprite = sprite;
		GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, true);
		yield return new WaitForSeconds(1.0f);
		GetComponent<Image>().CrossFadeAlpha(0f, 0.5f, true);
	}
}
