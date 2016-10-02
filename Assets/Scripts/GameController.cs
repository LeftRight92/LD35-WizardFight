using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance;

	public BoardController Board;
	public Player RedPlayer;
	public Player BluePlayer;
	public ActionHandler ActionHandler;
	public Turn Turn { get; protected set; }
	private int turnNumber = 1;

	public bool ReadyAdvanceTurns = false;

	// Use this for initialization
	void Awake() {
		if (instance != null) Debug.LogError("Cannot have multiple game controllers");
		instance = this;
	}
	
	void Start() {
		Board.Begin();
		ActionHandler = new ActionHandler(Board.PostActionCallback);
		RedPlayer.Begin();
		BluePlayer.Begin();
		Turn = new Turn(Team.RED);
	}
	
	// Update is called once per frame
	void Update () {
		if (ReadyAdvanceTurns && ActionHandler.ready) AdvanceTurns();
	}

	public void AdvanceTurns() {
		Board.data.pieces.Values.Where(
			p => p != null &&
			p.Team == Turn.Team &&
			p.Blocked
			).ToList().ForEach(p => p.UnStun());
		Board.Deselect();
		if(Turn.Team == Team.RED) {
			if (RedPlayer.TurnsUntilCreate > 0) RedPlayer.TurnsUntilCreate--;
			RedPlayer.HitCheck();
			if (BluePlayer.TurnsUntilCreate <= 0)
				GUIController.instance.FadeInSpawns();
			else
				GUIController.instance.FadeOutSpawns();
		} else {
			if (BluePlayer.TurnsUntilCreate > 0) BluePlayer.TurnsUntilCreate--;
			BluePlayer.HitCheck();
			if (RedPlayer.TurnsUntilCreate <= 0)
				GUIController.instance.FadeInSpawns();
			else
				GUIController.instance.FadeOutSpawns();
		}
		turnNumber++;
		GUIController.instance.TurnSign.DoCrossfade(Turn.Team.Opposite());
		Turn = new Turn(Turn.Team.Opposite());
		ReadyAdvanceTurns = false;
	}

	public void Spawn(SpriteType type) {
		if(Turn.Team == Team.RED) {
			RedPlayer.Spawn(type);
		} else {
			BluePlayer.Spawn(type);
		}
	}

	public void GameOver(Team losingPlayer) {
		GlobalState.instance.WinningPlayer = losingPlayer.Opposite();
		StartCoroutine(WaitGameOver());
	}

	IEnumerator WaitGameOver() {
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("GameOver");
	}
}
