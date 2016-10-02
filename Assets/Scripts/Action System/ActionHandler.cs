using UnityEngine;
using System.Collections.Generic;
using System;

public class ActionHandler {

	private Queue<KeyValuePair<Action<ISprite>, ISprite>> queue;
	public bool ready { get; private set; }
	public Callback postActionCallback;

	public ActionHandler(Callback postActionCallback) {
		queue = new Queue<KeyValuePair<Action<ISprite>, ISprite>>();
		ready = true;
		this.postActionCallback = postActionCallback;
	}
	
	private void Update() {
		if (ready && queue.Count > 0) {
			//Debug.Log("Dispatch: " + queue.Count);
			KeyValuePair<Action<ISprite>, ISprite> action = queue.Dequeue();
			action.Key(action.Value);
			ready = false;
		}
		//else {
		//	Debug.Log("No Update: " + ready + " " + queue.Count);
		//}
	}

	public void Callback() {
		ready = true;
		//Debug.Log("Callback");
		if (queue.Count == 0) postActionCallback();
		Update();
	}

	public void Move(ISprite sprite, IntPair direction) {
		Enqueue(Actions.Disappear, sprite);
		if (direction == IntPair.Up) Enqueue(Actions.MoveUp, sprite);
		else if (direction == IntPair.Down) Enqueue(Actions.MoveDown, sprite);
		else if (direction == IntPair.Left) Enqueue(Actions.MoveLeft, sprite);
		else if (direction == IntPair.Right) Enqueue(Actions.MoveRight, sprite);
		else Debug.LogError("Move Direction must be atomic");
		Enqueue(Actions.Appear, sprite);
		Update();
	}
	
	public void Take(ISprite sprite, IntPair direction, ISprite enemy) {
		Enqueue(Actions.Die, enemy);
		Move(sprite, direction);
		Update();
	}

	public void Die(ISprite sprite) {
		Enqueue(Actions.Die, sprite);
		Update();
	}

	public void Annihilate(ISprite sprite, ISprite enemy) {
		Enqueue(Actions.Die, enemy);
		Enqueue(Actions.Die, sprite);
		Update();
	}

	public void Transform(ISprite sprite) {
		Enqueue(Actions.Disappear, sprite);
		Enqueue(Actions.Transform, sprite);
		Update();
	}

	public void TransformEnd(ISprite sprite) {
		Enqueue(Actions.Appear, sprite);
		Update();
	}

	public void Enqueue(Action<ISprite> act, ISprite spr) {
		queue.Enqueue(new KeyValuePair<Action<ISprite>, ISprite>(act, spr));
	}
}
