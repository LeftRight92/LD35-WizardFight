using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	void Update () {
		if (!GameController.instance.ActionHandler.ready) return;
		if (Input.GetMouseButtonDown(0)) {
			GameObject hitObj = GetTileFromRay();
			if(hitObj != null) {
				GameController.instance.Board.Select(hitObj);
			}
		}
		if (Input.GetMouseButtonDown(1)) {
			GameObject hitObj = GetTileFromRay();
			if(hitObj != null) {
				GameController.instance.Board.Order(hitObj);
			}
		}
	}

	GameObject GetTileFromRay() {
		RaycastHit hit;
		LayerMask mask = LayerMask.GetMask(new string[] { "Tiles" });
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f, mask)) {
			return hit.transform.parent.gameObject;
		}
		return null;
	}
}
