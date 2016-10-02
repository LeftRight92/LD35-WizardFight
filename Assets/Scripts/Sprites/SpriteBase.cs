using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class SpriteBase : MonoBehaviour, ISprite {
	public static float GROWTH_RATE = 5f;
	[SerializeField]
	public Team Team { get; set; }
	public SpriteType Type { get; protected set; }
	public bool Blocked { get; protected set; }

	private GameObject stars;
	public GameObject starPrefab;

	private Callback actionCallback;

	public void Appear() {
		StartCoroutine(Grow());
	}

	public void Die() {
		Disappear();
	}

	public void Transform() {
		GameController.instance.Board.Transform(this);
		GameController.instance.Turn.Acted(this);
		Destroy(this);
		actionCallback();
	}

	public void Disappear() {
		StartCoroutine(Shrink());
	}

	public void Move(IntPair i) {
		transform.Translate(new Vector3(
			i.a * GameController.instance.Board.scale,
			0f,
			i.b * GameController.instance.Board.scale), GameController.instance.transform);
		transform.rotation = Quaternion.Euler(
			0,
			i.a * 90 + (i.b == -1 ? 180 : 0),
			0);
		actionCallback();
	}

	public void RegisterActionCallback(Callback cb) {
		actionCallback += cb;
	}

	public void Stun() {
		stars = Instantiate(starPrefab, transform.position, Quaternion.identity) as GameObject;
		Blocked = true;
	}

	public void UnStun() {
		Destroy(stars);
		Blocked = false;
	}

	IEnumerator Grow() {
		GetComponentsInChildren<MeshRenderer>().ToList().ForEach(m => m.enabled = true);
		while (transform.localScale.x < 0.95f) {
			transform.localScale += GROWTH_RATE * Time.deltaTime * Vector3.one;
			yield return null;
		}
		transform.localScale = Vector3.one;
		actionCallback();
		yield return null;
	}

	IEnumerator Shrink() {
		while (transform.localScale.x > 0.05f) {
			transform.localScale -= GROWTH_RATE * Time.deltaTime * Vector3.one;
			yield return null;
		}
		transform.localScale = Vector3.zero;
		GetComponentsInChildren<MeshRenderer>().ToList().ForEach(m =>m.enabled = true);
		actionCallback();
		yield return null;
	}
}
