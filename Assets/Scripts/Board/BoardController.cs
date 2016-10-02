using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BoardController : MonoBehaviour {

	public BoardData data { get; protected set; }

	private static readonly int MOVE_DISTANCE = 2;

	public GameObject fireSprite;
	public GameObject waterSprite;
	public GameObject earthSprite;
	public GameObject airSprite;
	public GameObject stone;

	public Vector3 offset;
	public float scale = 2.0f;
	
	private Dictionary<IntPair, GameObject> tiles;
	private IntPair? selected;
	private Dictionary<IntPair, ActionType> actableTiles;
	private GameObject orderObj;

	public void Begin () {
		data = new BoardData(9, 7);
		tiles = new Dictionary<IntPair, GameObject>();
		foreach (IntPair i in data.pieces.Keys) {
			GameObject tile = Instantiate(stone, translate(i), Quaternion.identity) as GameObject;
			tile.transform.parent = transform;
			tile.name = "Tile(" + i.a + ", " + i.b + ")";
			tiles.Add(i, tile);
		}
	}

	public void Select(GameObject obj) {
		IntPair newSelect = tiles.First(t => t.Value == obj).Key;
		if (!GameController.instance.Turn.ToAct.Contains(data[newSelect])) return;
		selected = newSelect;
		tiles.Values.ToList().ForEach(t => t.GetComponent<Tile>().CancelGlow());
		actableTiles = new Dictionary<IntPair, ActionType>();
		if(data[selected.Value] != null) {
			if (data[selected.Value].Team != GameController.instance.Turn.Team) {
				selected = null;
				actableTiles = null;
			} else {
				actableTiles.Add(selected.Value, ActionType.TRANSFORM);
				tiles[selected.Value].GetComponent<Tile>().TransformGlow();
				foreach (IntPair i in data.GetLocationsInRange(selected.Value, MOVE_DISTANCE)) {
					tiles[i].GetComponent<Tile>().MoveGlow();
					actableTiles.Add(i, ActionType.MOVE);
				}
				foreach (IntPair i in data.GetHostileLocationsInRange(data[selected.Value], MOVE_DISTANCE)) {
					tiles[i].GetComponent<Tile>().AttackGlow();
					actableTiles.Add(i, ActionType.ATTACK);
				}
			}
		} else {
			selected = null;
			actableTiles = null;
		}
	}

	public void Deselect() {
		selected = null;
		actableTiles = null;
		tiles.Values.ToList().ForEach(t => t.GetComponent<Tile>().CancelGlow());
	}

	public void Add(SpriteType type, Team team, IntPair location) {
		GameObject proto;
		if (type == SpriteType.AIR) proto = airSprite;
		else if (type == SpriteType.EARTH) proto = earthSprite;
		else if (type == SpriteType.FIRE) proto = fireSprite;
		else if (type == SpriteType.WATER) proto = waterSprite;
		else throw new System.Exception();
		if (data[location] != null) return;
		GameObject sprite = Instantiate(proto, translate(location), Quaternion.identity) as GameObject;
		sprite.GetComponent<ISprite>().Team = team;
		sprite.GetComponent<ISprite>().RegisterActionCallback(GameController.instance.ActionHandler.Callback);
		tiles[location].GetComponent<Tile>().AddRunes(team); //TODO: Set as appropriate
		data.Add(sprite.GetComponent<ISprite>(), location);
		GUIController.instance.FadeOutSpawns();
		if(selected.HasValue)
			Select(tiles[selected.Value]);
	}

	public void Transform(ISprite sprite) {
		SpriteType type = sprite.Type.TransformType();
		GameObject proto;
		if (type == SpriteType.AIR) proto = airSprite;
		else if (type == SpriteType.EARTH) proto = earthSprite;
		else if (type == SpriteType.FIRE) proto = fireSprite;
		else if (type == SpriteType.WATER) proto = waterSprite;
		else throw new System.Exception();
		IntPair location = data[sprite];
		GameObject newSprite = Instantiate(proto, translate(location), Quaternion.identity) as GameObject;
		newSprite.GetComponent<ISprite>().Team = sprite.Team;
		newSprite.GetComponent<ISprite>().RegisterActionCallback(GameController.instance.ActionHandler.Callback);

		data.Remove(sprite);
		data.Add(newSprite.GetComponent<ISprite>(), location);
		newSprite.transform.localScale = Vector3.zero;
		GameController.instance.ActionHandler.TransformEnd(newSprite.GetComponent<ISprite>());
	}

	public void Order(GameObject obj) {
		if (!selected.HasValue) return;
		IntPair location = tiles.First(t => t.Value == obj).Key;
		if (!actableTiles.ContainsKey(location)) return;
		ActionType order = actableTiles[location];
		//Debug.Log(order);
		orderObj = obj;
		if (order == ActionType.TRANSFORM) GameController.instance.ActionHandler.Transform(data[selected.Value]);
		else if (order == ActionType.MOVE) {
			Pathfind pf = new Pathfind(selected.Value, location);
			List<IntPair> directions = pf.GetDirections();
			foreach (IntPair i in directions)
				GameController.instance.ActionHandler.Move(data[selected.Value], i);
		} else if (order == ActionType.ATTACK) {
			Pathfind pf = new Pathfind(selected.Value, location);
			List<IntPair> directions = pf.GetDirections();
			for (int i = 0; i < directions.Count - 1; i++) {
				GameController.instance.ActionHandler.Move(data[selected.Value], directions[i]);
			}
			SpriteType selectedType = data[selected.Value].Type;
			SpriteType targetType = data[location].Type;
			//Debug.Log(selectedType + " --> " + targetType);
			if (selectedType.WeakerType() == targetType) {
				Debug.Log("Take");
				GameController.instance.ActionHandler.Take(data[selected.Value], directions.Last(), data[location]);
			} else if (selectedType.StrongerType() == targetType) {
				Debug.Log("Die");
				GameController.instance.ActionHandler.Die(data[selected.Value]);
			} else if (selectedType.AnnihilateType() == targetType) {
				Debug.Log("Annihilate");
				GameController.instance.ActionHandler.Annihilate(data[selected.Value], data[location]);
			} else
				Debug.Log("Block");
			//TODO: Add code for blocking
		}
	}

	public void PostActionCallback() {
		if (!selected.HasValue) return;
		//Debug.Log("PA Callback");
		IntPair location = tiles.FirstOrDefault(t => t.Value == orderObj).Key;
		ActionType order = actableTiles[location];
		if (order == ActionType.TRANSFORM) {
			;
		} else if (order == ActionType.MOVE) {
			tiles[selected.Value].GetComponent<Tile>().RemoveRunes();
			tiles[location].GetComponent<Tile>().AddRunes(data[selected.Value].Team);
			data.Move(selected.Value, location);
		} else if (order == ActionType.ATTACK) {
			SpriteType selectedType = data[selected.Value].Type;
			SpriteType targetType = data[location].Type;

			if (selectedType.WeakerType() == targetType) {
				data.RemoveAt(location);
				tiles[selected.Value].GetComponent<Tile>().RemoveRunes();
				tiles[location].GetComponent<Tile>().ChangeRunes(data[selected.Value].Team);
				data.Move(selected.Value, location);

			} else if (selectedType.StrongerType() == targetType) {
				data.RemoveAt(selected.Value);
				tiles[selected.Value].GetComponent<Tile>().RemoveRunes();

			} else if (selectedType.AnnihilateType() == targetType) {
				tiles[selected.Value].GetComponent<Tile>().RemoveRunes();
				tiles[location].GetComponent<Tile>().RemoveRunes();
				data.RemoveAt(selected.Value);
				data.RemoveAt(location);
			} else {
				tiles[selected.Value].GetComponent<Tile>().RemoveRunes();
				//HORRIFIC CODE ALERT
				IntPair movedPos = translate(((MonoBehaviour) data[selected.Value]).transform.position);
				tiles[movedPos].GetComponent<Tile>().AddRunes(data[selected.Value].Team);
				data.Move(selected.Value, movedPos);
				data[location].Stun();
				location = movedPos;
			}
		}
		GameController.instance.Turn.Acted(data[location]);
		Deselect();
		//if (GameController.instance.ReadyAdvanceTurns) return;
		//Select(tiles[data[GameController.instance.Turn.GetAny()]]);
	}

	private Vector3 translate(IntPair intP) {
		return new Vector3(
			(intP.a * scale) - offset.x,
			offset.y,
			(intP.b * scale) - offset.z
			);
	}

	private IntPair translate(Vector3 v) {
		return new IntPair(
			Mathf.RoundToInt((v.x + offset.x)/ scale),
			Mathf.RoundToInt((v.z + offset.z)/ scale)
			);
	}
}
