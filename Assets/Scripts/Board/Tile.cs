using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	[SerializeField]
	private GameObject runePrefab;
	[SerializeField]
	private Material RedMaterial;
	[SerializeField]
	private Material BlueMaterial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveGlow() {
		transform.Find("Glow").GetComponent<Glow>().MoveGlow();

	}

	public void AttackGlow() {
		transform.Find("Glow").GetComponent<Glow>().AttackGlow();
	}

	public void TransformGlow() {
		transform.Find("Glow").GetComponent<Glow>().TransformGlow();
	}

	public void CancelGlow() {
		transform.Find("Glow").GetComponent<Glow>().StopGlow();
	}

	public void AddRunes(Team t) {
		if (transform.Find("Runes") != null) return;
		GameObject runeObj = Instantiate(
			runePrefab,
			transform.position,
			Quaternion.Euler(0, Random.Range(0, 3) * 90, 0)) as GameObject;
		runeObj.transform.parent = transform;
		runeObj.name = "Runes";
		runeObj.transform.Find("Mesh").GetComponent<MeshRenderer>().material =
			t == Team.BLUE ? BlueMaterial : RedMaterial;
	}

	public void ChangeRunes(Team t) {
		transform.Find("Runes").Find("Mesh").GetComponent<MeshRenderer>().material = 
			t == Team.BLUE ? BlueMaterial : RedMaterial;

	}

	public void RemoveRunes() {
		Destroy(transform.Find("Runes").gameObject);
	}
}
