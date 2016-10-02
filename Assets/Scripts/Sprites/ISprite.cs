using UnityEngine;
using System.Collections;

public delegate void Callback();

public interface ISprite {
	Team Team { get; set; }
	SpriteType Type { get; }
	bool Blocked { get; }
	void Appear();
	void Disappear();
	void Die();
	void Move(IntPair i);
	void RegisterActionCallback(Callback cb);
	void Transform();
	void Stun();
	void UnStun();
}
