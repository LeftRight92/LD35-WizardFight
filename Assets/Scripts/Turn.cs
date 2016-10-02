using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Turn {

	public Team Team { get; protected set; }
	public List<ISprite> ToAct { get; protected set; }

	public Turn(Team team) {
		ToAct = GameController.instance.Board.data.pieces.Values.Where(
			p => p != null &&
			p.Team == team &&
			!p.Blocked
			).ToList();
		//Debug.Log("ToAct Pieces: " + ToAct.Count);
		Team = team;
	}

	public void Acted(ISprite sprite) {
		if (!ToAct.Contains(sprite)) return;
		ToAct.Remove(sprite);
		if (ToAct.Count == 0) GameController.instance.ReadyAdvanceTurns = true;
	}

	public ISprite GetAny() {
		return ToAct.First();
	}
}
