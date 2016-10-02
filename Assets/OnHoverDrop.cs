using UnityEngine;
using System.Collections;

public class OnHoverDrop : MonoBehaviour {

	public void Enter() {
		GetComponent<RectTransform>().position = new Vector3(
			GetComponent<RectTransform>().position.x,
			GetComponent<RectTransform>().position.y - 0.05f,
			GetComponent<RectTransform>().position.z
			);
	}

	public void Exit() {
		GetComponent<RectTransform>().position = new Vector3(
			GetComponent<RectTransform>().position.x,
			GetComponent<RectTransform>().position.y + 0.05f,
			GetComponent<RectTransform>().position.z
			);
	}
}
