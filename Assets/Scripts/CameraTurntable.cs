using UnityEngine;
using System.Collections;

public class CameraTurntable : MonoBehaviour {
	Vector3 lastMousePosition;
	[SerializeField]
	bool flipVertical = false;
	[SerializeField]
	float verticalScaling = 1f;
	[SerializeField]
	bool flipHorizontal = false;
	[SerializeField]
	float horizontalScaling = 1f;
	[SerializeField]
	float verticalAngleMin = 10f;
	[SerializeField]
	float verticalAngleMax = 80f;
	// Use this for initialization
	void Start () {
		lastMousePosition = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentMousePosition = Input.mousePosition;

		if (Input.GetMouseButton(2)) {
			Vector3 diff = (lastMousePosition - currentMousePosition);
			transform.rotation = Quaternion.Euler(
				Mathf.Clamp(
					transform.rotation.eulerAngles.x + (diff.y * verticalScaling * (flipVertical ? -1 : 1)),
					verticalAngleMin,
					verticalAngleMax),
				transform.rotation.eulerAngles.y + (diff.x * horizontalScaling * (flipHorizontal ? -1 : 1)),
				0);
		}

		lastMousePosition = currentMousePosition;
	}
}
