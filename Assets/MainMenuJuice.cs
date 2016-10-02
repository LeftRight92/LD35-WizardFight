using UnityEngine;
using System.Collections;

public class MainMenuJuice : MonoBehaviour {

	public GameObject[] prefabs;

	// Use this for initialization
	void Start() {
		Instantiate(prefabs[Random.Range(0, 4)], transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 1, 0), 10 * Time.deltaTime);
	}
}
