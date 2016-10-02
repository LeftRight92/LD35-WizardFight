using UnityEngine;
using System.Collections;

public static class EnumExtensions {
	public static Team Opposite(this Team t) {
		if (t == Team.BLUE) return Team.RED;
		return Team.BLUE;
	}

	/// <summary>
	/// Returns the type that would beat this type.
	/// </summary>
	public static SpriteType StrongerType(this SpriteType t) {
		if (t == SpriteType.AIR) return SpriteType.FIRE;
		if (t == SpriteType.EARTH) return SpriteType.WATER;
		if (t == SpriteType.FIRE) return SpriteType.EARTH;
		return SpriteType.AIR;
	}

	/// <summary>
	/// Returns the type that would lose to this type.
	/// </summary>
	public static SpriteType WeakerType(this SpriteType t) {
		if (t == SpriteType.AIR) return SpriteType.WATER;
		if (t == SpriteType.EARTH) return SpriteType.FIRE;
		if (t == SpriteType.FIRE) return SpriteType.AIR;
		return SpriteType.EARTH;
	}

	public static SpriteType TransformType(this SpriteType t) {
		if (t == SpriteType.AIR) return SpriteType.EARTH;
		if (t == SpriteType.EARTH) return SpriteType.AIR;
		if (t == SpriteType.FIRE) return SpriteType.WATER;
		return SpriteType.FIRE;
	}

	public static SpriteType AnnihilateType(this SpriteType t) {
		return t.TransformType();
	}
}

