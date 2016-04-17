using UnityEngine;
using System.Collections;

public class SemiTransparent : MonoBehaviour {

	[SerializeField]
	[Range(0,1)]
	float alpha = 0.5f;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
