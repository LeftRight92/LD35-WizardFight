using UnityEngine;
using System.Collections;
using System;

public static class Actions {
	public static Action<ISprite> Appear = new Action<ISprite>(s => s.Appear());
	public static Action<ISprite> Disappear = new Action<ISprite>(s => s.Disappear());
	public static Action<ISprite> Die = new Action<ISprite>(s => s.Die());
	public static Action<ISprite> MoveUp = new Action<ISprite>(s => s.Move(IntPair.Up));
	public static Action<ISprite> MoveDown = new Action<ISprite>(s => s.Move(IntPair.Down));
	public static Action<ISprite> MoveLeft = new Action<ISprite>(s => s.Move(IntPair.Left));
	public static Action<ISprite> MoveRight = new Action<ISprite>(s => s.Move(IntPair.Right));
	public static Action<ISprite> Transform = new Action<ISprite>(s => s.Transform());
}
