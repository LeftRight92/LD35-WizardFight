using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List<GameObject> units { get; private set; }
	[SerializeField]
	private IntPair spawnLocation;
	[SerializeField]
	private IntPair[] beginSpawnLocations;
	[SerializeField]
	private Team team;
	public int lives = 3;
	public int TurnsUntilCreate;
	public List<GameObject> lifeContainers;

	// Use this for initialization
	void Start () {
		TurnsUntilCreate = 1;
	}

	public void Begin() {
		GameController.instance.Board.Add(SpriteType.AIR, team, beginSpawnLocations[0]);
		GameController.instance.Board.Add(SpriteType.EARTH, team, beginSpawnLocations[1]);
		GameController.instance.Board.Add(SpriteType.FIRE, team, beginSpawnLocations[2]);
		GameController.instance.Board.Add(SpriteType.WATER, team, beginSpawnLocations[3]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Spawn(SpriteType type) {
		if(TurnsUntilCreate <= 0) {
			GameController.instance.Board.Add(type, team, spawnLocation);
			TurnsUntilCreate = 3;
		}
	}

	public void HitCheck() {
		ISprite sprite = GameController.instance.Board.data[spawnLocation];
		if(sprite != null && sprite.Team == team.Opposite()) {
			lives--;
			GameObject lc = lifeContainers[0];
			lifeContainers.Remove(lc);
			Destroy(lc);
			if (lives <= 0) GameController.instance.GameOver(team);
		}
	}
}
