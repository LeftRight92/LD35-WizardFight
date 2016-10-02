using UnityEngine;
using System.Collections;

public class HeartTorque : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddTorque(
			new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)
			), ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
