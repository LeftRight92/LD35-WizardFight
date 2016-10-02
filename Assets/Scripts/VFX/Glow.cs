using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class Glow : MonoBehaviour {

	[SerializeField]
	private Color attackGlow;
	[SerializeField]
	private Color moveGlow;
	[SerializeField]
	private Color transformGlow;
	//[SerializeField]
	//private float minAlpha;
	//[SerializeField]
	//private float maxAlpha;

	//private bool glowing = false;
	//private bool climbing = true;
	//private Color currentGlowMax, currentGlowMin;
	private MeshRenderer r;

	//private Color attackGlowMin, attackGlowMax, moveGlowMin, moveGlowMax;
	
	void Start() {
		r = GetComponent<MeshRenderer>();
		//attackGlowMin = new Color(attackGlow.r, attackGlow.g, attackGlow.b, minAlpha);
		//attackGlowMax = new Color(attackGlow.r, attackGlow.g, attackGlow.b, maxAlpha);
		//moveGlowMin = new Color(moveGlow.r, moveGlow.g, moveGlow.b, minAlpha);
		//moveGlowMax = new Color(moveGlow.r, moveGlow.g, moveGlow.b, maxAlpha);
		StopGlow();
	}

	// Update is called once per frame
	//void Update () {
	//	if (glowing) {
	//		r.material.color = Color.Lerp(
	//			r.material.color,
	//			climbing ? currentGlowMax : currentGlowMin,
	//			0f * Time.deltaTime
	//			);
	//		if ((climbing && r.material.color.a > 0.95f * maxAlpha) ||
	//			(!climbing && r.material.color.a < 1.05f * minAlpha)) {
	//			climbing = !climbing;
	//			Debug.Log("Switch");
	//		}
	//		Debug.Log(r.material.color.a);
	//	}
	//}

	public void AttackGlow() {
		gameObject.SetActive(true);
		//glowing = true;
		//currentGlowMin = attackGlowMin;
		//currentGlowMax = attackGlowMax;
		//r.material.color = Color.Lerp(attackGlowMin, attackGlowMax, Random.value);
		r.material.color = attackGlow;
	}

	public void MoveGlow() {
		gameObject.SetActive(true);
		//glowing = true;
		//currentGlowMin = moveGlowMin;
		//currentGlowMax = moveGlowMax;
		//r.material.color = Color.Lerp(moveGlowMin, moveGlowMax, Random.value);
		r.material.color = moveGlow;
	}

	public void TransformGlow() {
		gameObject.SetActive(true);
		r.material.color = transformGlow;
	}

	public void StopGlow() {
		//glowing = false;
		gameObject.SetActive(false);
	}
}
